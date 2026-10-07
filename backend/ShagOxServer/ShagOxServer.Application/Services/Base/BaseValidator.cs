using ShagOxServer.Application.Interfaces.Repositories.Base;
using ShagOxServer.Application.Interfaces.Repositories.Base.Query;
using ShagOxServer.Application.Resources.EntityNames.Extensions;
using ShagOxServer.Domain.Base;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Services.Base;
public abstract class BaseValidator<TEntity>
    where TEntity : BaseEntity
{
    private readonly IQueryRepository<TEntity> _objectRepository;
    private readonly IExistsRepository<TEntity> _objectExistsRepository;


    public BaseValidator(
        IQueryRepository<TEntity> objectRepository,
        IExistsRepository<TEntity> objectExistsRepository)
    {
        _objectRepository = objectRepository;
        _objectExistsRepository = objectExistsRepository;
    }


    public async Task<Result<TEntity>> GetByIdAsync(
        long entityId)
    {
        var entity = await _objectRepository
            .GetByIdAsync(entityId);

        if (entity is null)
        {
            return Result<TEntity>.NotFound(
                EntityNameExtensions.GetLocalizedName<TEntity>());
        }

        return Result<TEntity>.Success(entity);
    }

    public async Task<Result<bool>> ExistsByIdAsync(
        long entityId)
    {
        if (!await _objectExistsRepository
            .ExistsByIdAsync(entityId))
        {
            return Result<bool>.NotFound(
                EntityNameExtensions.GetLocalizedName<TEntity>());
        }

        return Result<bool>.Success(true);
    }

    public async Task<Result<bool>> NotExistsByIdAsync(
        long entityId)
    {
        if (await _objectExistsRepository
            .ExistsByIdAsync(entityId))
        {
            return Result<bool>.AlreadyExists(
                EntityNameExtensions.GetLocalizedName<TEntity>());
        }

        return Result<bool>.Success(true);
    }
}
