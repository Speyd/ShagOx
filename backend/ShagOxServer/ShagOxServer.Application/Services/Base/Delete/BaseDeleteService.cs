using Microsoft.Extensions.Logging;
using ShagOxServer.Application.DTOs.Base.Responses;
using ShagOxServer.Application.Interfaces.Persistences;
using ShagOxServer.Application.Interfaces.Repositories.Base;
using ShagOxServer.Application.Interfaces.Services.Base.Delete;
using ShagOxServer.Application.Resources.EntityErrors;
using ShagOxServer.Application.Resources.EntityNames.Extensions;
using ShagOxServer.Application.Services.Base.Validator;
using ShagOxServer.Domain.Base;
using ShagOxServer.Domain.Base.Enum;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Services.Base.Delete;
public abstract class BaseDeleteService<TEntity>
    : ISoftDeleteService<TEntity, DeleteResponse>
    where TEntity : BaseEntity
{
    protected readonly IRepository<TEntity> _repository;
    protected readonly BaseValidator<TEntity> _queryRepository;
    private readonly ILogger<BaseDeleteService<TEntity>> _logger;
    private readonly IUnitOfWork _unitOfWork;


    public BaseDeleteService(
        IRepository<TEntity> repository,
        BaseValidator<TEntity> queryRepository,
        IUnitOfWork unitOfWork,
        ILogger<BaseDeleteService<TEntity>> logger)
    {
        _repository = repository;
        _queryRepository = queryRepository;
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public abstract Task InvalidateCache(
        TEntity entity);

    public virtual async Task<Result<DeleteResponse>> SoftDeleteAsync(
        long id,
        CancellationToken cancellationToken = default)
    {
        var entity = await _queryRepository
            .GetByIdIncludeAsync(id, cancellationToken);

        if (!entity.IsSuccess)
            return Result<DeleteResponse>.Fail(entity.Error);

        if (entity.Value is null)
        {
            return Result<DeleteResponse>.NotFound(
                EntityNameExtensions.GetLocalizedName<TEntity>());
        }

        return await SoftDeleteInternalAsync(entity.Value, 
            cancellationToken);
    }

    public virtual async Task<Result<DeleteResponse>> SoftDeleteInternalAsync(
        TEntity entity,
        CancellationToken cancellationToken = default)
    {
        await _unitOfWork.BeginTransactionAsync();

        try
        {
            var body = await SoftBodyDeleteAsync(entity, 
                    cancellationToken);

            if (!body.IsSuccess)
            {
                await _unitOfWork.RollbackAsync();
                return Result<DeleteResponse>.Fail(body.Error);
            }

            _repository.Update(entity);

            await _unitOfWork.CommitAsync();
        }
        catch (Exception ex)
        {
            await _unitOfWork.RollbackAsync();

            _logger.LogError(
                ex,
                "Failed to delete {EntityName}. Id: {Id}",
                typeof(TEntity).Name,
                entity.Id);

            return Result<DeleteResponse>.Fail(
                EntityErrorResources.LineItemAttributeDeleteFailed);
        }

        await InvalidateCache(entity);

        return Result<DeleteResponse>.Success(
            new DeleteResponse(entity.Id, DateTime.UtcNow));
    }

    public virtual async Task<Result<DeleteResponse>> SoftDeleteRecordAsync(
       TEntity entity,
        CancellationToken cancellationToken = default)
    {
        try
        {
            var body = await SoftBodyDeleteAsync(entity, 
                cancellationToken);

            _repository.Update(entity);

            if (!body.IsSuccess)
                return Result<DeleteResponse>.Fail(body.Error);

            await _unitOfWork.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(
                ex,
                "Failed to delete {EntityName}. Id: {Id}",
                typeof(TEntity).Name,
                entity.Id);

            return Result<DeleteResponse>.Fail(
                EntityErrorResources.LineItemAttributeDeleteFailed);
        }

        await InvalidateCache(entity);

        return Result<DeleteResponse>.Success(
            new DeleteResponse(entity.Id, DateTime.UtcNow));
    }

    public virtual async Task<Result<bool>> SoftBodyDeleteAsync(
        TEntity entity,
        CancellationToken cancellationToken = default)
    {
        var tracked = await _queryRepository
            .GetByIdAttachAsync(entity.Id, cancellationToken);

        if (!tracked.IsSuccess)
            return Result<bool>.Fail(tracked.Error);

        var deletedAt = DateTime.UtcNow;

        foreach (var target in new[] { entity, tracked.Value! })
        {
            target.EntityStatus = EntityStatus.Deleted;
            target.DeletedAt = deletedAt;
        }

        return Result<bool>.Success(true);
    }
}
