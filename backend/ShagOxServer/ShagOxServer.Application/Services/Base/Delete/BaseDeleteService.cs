using Microsoft.Extensions.Logging;
using ShagOxServer.Application.DTOs.Base.Responses;
using ShagOxServer.Application.Interfaces.Persistences;
using ShagOxServer.Application.Interfaces.Services.Base.Delete;
using ShagOxServer.Application.Resources.EntityErrors;
using ShagOxServer.Application.Resources.EntityNames.Extensions;
using ShagOxServer.Domain.Base;
using ShagOxServer.Domain.Base.Enum;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Services.Base.Delete;
public abstract class BaseDeleteService<TEntity>
    : ISoftDeleteService<DeleteResponse>
    where TEntity : BaseEntity
{
    protected readonly BaseValidator<TEntity> _queryRepository;
    private readonly ILogger<BaseDeleteService<TEntity>> _logger;
    private readonly IUnitOfWork _unitOfWork;


    public BaseDeleteService(
        BaseValidator<TEntity> queryRepository,
        IUnitOfWork unitOfWork,
        ILogger<BaseDeleteService<TEntity>> logger
    )
    {
        _queryRepository = queryRepository;
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public abstract Task InvalidateCache(
        TEntity entity);

    public virtual async Task<Result<DeleteResponse>> SoftDeleteAsync(
        long id)
    {
        var entity = await _queryRepository
            .GetByIdIncludeAsync(id);

        if (!entity.IsSuccess)
            return Result<DeleteResponse>.Fail(entity.Error);

        if (entity.Value is null)
        {
            return Result<DeleteResponse>.NotFound(
                EntityNameExtensions.GetLocalizedName<TEntity>());
        }

        await _unitOfWork.BeginTransactionAsync();

        try
        {
            var body = await SoftBodyDeleteAsync(entity.Value);
            if (!body.IsSuccess)
                return Result<DeleteResponse>.Fail(body.Error);

            await _unitOfWork.CommitAsync();
        }
        catch (Exception ex)
        {
            await _unitOfWork.RollbackAsync();

            _logger.LogError(
                ex,
                $"Failed to delete {typeof(TEntity).Name}. " + 
                "Id: {Id}",
                id);

            return Result<DeleteResponse>.Fail(
                EntityErrorResources.BasketAttributeDeleteFailed);
        }

        await InvalidateCache(entity.Value!);

        return Result<DeleteResponse>.Success(
           new DeleteResponse(
               entity.Value!.Id,
               DateTime.UtcNow
           )
       );
    }

    public virtual async Task<Result<bool>> SoftBodyDeleteAsync(
        TEntity entity)
    {
        entity.EntityStatus = EntityStatus.Deleted;
        entity.DeletedAt = DateTime.UtcNow;

        return Result<bool>.Success(true);
    }
}
