using Microsoft.Extensions.Logging;
using ShagOxServer.Application.DTOs.Base.Requests;
using ShagOxServer.Application.DTOs.Base.Responses;
using ShagOxServer.Application.Interfaces.Persistences;
using ShagOxServer.Application.Interfaces.Services.Base.Delete;
using ShagOxServer.Application.Resources.EntityErrors;
using ShagOxServer.Application.Services.Base.Validator;
using ShagOxServer.Domain.Base;
using ShagOxServer.Domain.Base.Enum;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Services.Base.Update;
public abstract class BaseUpdateService<TEntity>
    where TEntity : BaseEntity
{
    private readonly ISoftDeleteService<TEntity, DeleteResponse> _deleteService;
    private readonly BaseValidator<TEntity> _validator;

    private readonly ILogger<BaseUpdateService<TEntity>> _logger;

    private readonly IUnitOfWork _unitOfWork;


    public BaseUpdateService(
        ISoftDeleteService<TEntity, DeleteResponse> deleteService,
        BaseValidator<TEntity> validator,
        ILogger<BaseUpdateService<TEntity>> logger,
        IUnitOfWork unitOfWork)
    {
        _deleteService = deleteService;
        _validator = validator;
        _logger = logger;
        _unitOfWork = unitOfWork;
    }


    protected virtual async Task<Result<bool>> ValidateUpdateConflictsAsync(
        TEntity entity,
        CancellationToken cancellationToken = default)
    {
        return Result<bool>.Success(true);
    }

    public virtual async Task<Result<bool>> ChangeStatusAsync(
        long entityId,
        ChangeStatusRequest request,
        CancellationToken cancellationToken = default)
    {
        var entityResult = await _validator
            .GetByIdAttachAsync(entityId,
                cancellationToken, request.FindStatus);

        if (!entityResult.IsSuccess || entityResult.Value is null)
            return Result<bool>.Fail(entityResult.Error);


        var entity = entityResult.Value;

        if (request.ChangeStatus == entity.EntityStatus)
            return Result<bool>.Success(true);

        await _unitOfWork.BeginTransactionAsync();

        try 
        { 
            var result = request.ChangeStatus switch
            {
                EntityStatus.Deleted => await DeleteStatus(
                    entityId, request.FindStatus, cancellationToken),
                EntityStatus.Active => await ActiveStatus(entity),
                EntityStatus.Hidden => await HiddenStatus(entity),
                _ => Result<bool>.Fail(EntityErrorResources.InvalidStatusValue)
            };

            if (!result.IsSuccess)
                await _unitOfWork.RollbackAsync();

            return result;
        }
        catch (Exception ex)
        {
            await _unitOfWork.RollbackAsync();

            _logger.LogError(
               ex,
               "An unexpected error occurred while changing the status of " +
               "{EntityName} with ID {EntityId} to {Status}",
               typeof(TEntity).Name,
               entityId,
               request.ChangeStatus);

            return Result<bool>.Fail(
                EntityErrorResources.StatusChangeFailed);
        }
    }

    private async Task<Result<bool>> DeleteStatus(
        long entityId,
        EntityStatus findStatus,
        CancellationToken cancellationToken)
    {
        // М'яке видалення каскадно обробляє пов'язані дані (фото, позиції тощо)
        // і скидає кеш за ними, тому сутність потрібна разом з навігаціями:
        // без Include колекції лишаються null.
        var entityResult = await _validator
            .GetByIdIncludeAsync(entityId, cancellationToken, findStatus);

        if (!entityResult.IsSuccess || entityResult.Value is null)
            return Result<bool>.Fail(entityResult.Error);

        var delete = await _deleteService
            .SoftDeleteRecordAsync(entityResult.Value, cancellationToken);

        if (!delete.IsSuccess)
            return Result<bool>.Fail(delete.Error);

        await _unitOfWork.CommitAsync();

        return Result<bool>.Success(true);
    }

    private async Task<Result<bool>> ActiveStatus(
        TEntity entity)
    {
        var validation = await ValidateUpdateConflictsAsync(
            entity);

        if (!validation.IsSuccess)
            return Result<bool>.Fail(validation.Error);

        entity.EntityStatus = EntityStatus.Active;
        entity.DeletedAt = null;

        await _unitOfWork.CommitAsync();

        return Result<bool>.Success(true);
    }

    private async Task<Result<bool>> HiddenStatus(
        TEntity entity)
    {
        var validation = await ValidateUpdateConflictsAsync(
           entity);

        if (!validation.IsSuccess)
            return Result<bool>.Fail(validation.Error);

        entity.EntityStatus = EntityStatus.Hidden;
        entity.DeletedAt = null;

        await _unitOfWork.CommitAsync();

        return Result<bool>.Success(true);
    }
}
