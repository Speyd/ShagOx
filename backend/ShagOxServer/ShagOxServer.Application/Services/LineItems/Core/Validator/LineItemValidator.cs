using ShagOxServer.Application.Interfaces.Repositories.Base;
using ShagOxServer.Application.Interfaces.Repositories.Base.Query;
using ShagOxServer.Application.Interfaces.Repositories.LineItems.Core;
using ShagOxServer.Application.Interfaces.Services.Caches;
using ShagOxServer.Application.Resources.EntityNames.Extensions;
using ShagOxServer.Application.Services.Base.Validator;
using ShagOxServer.Domain.Entities.LineItems.Core;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Services.LineItems.Core.Validator;
public class LineItemValidator<TEntity>
    : BaseValidator<TEntity>
    where TEntity : LineItem
{
    private readonly ILineItemExistsRepository<TEntity> _itemExistsRepository;


    public LineItemValidator(
        IRepository<TEntity> repository,
        ILineItemExistsRepository<TEntity> itemExistsRepository,
        IQueryRepository<TEntity> itemQeuryRepository,
        IDataCacheService cache
    )
        : base(repository, itemQeuryRepository, itemExistsRepository, cache)
    {
        _itemExistsRepository = itemExistsRepository;
    }

    public async Task<Result<bool>> ExistsAsync(
        long advertisementid,
        long? advertisementVariantId,
        long parentId,
        CancellationToken cancellationToken = default)
    {
        if (!await _itemExistsRepository
                .ExistsAsync(
                    advertisementid,
                    advertisementVariantId,
                    parentId,
                    cancellationToken))
        {
            return Result<bool>.NotFound(
                EntityNameExtensions.GetLocalizedName<TEntity>());
        }

        return Result<bool>.Success(true);
    }

    public async Task<Result<bool>> NotExistsAsync(
        long advertisementid,
        long? advertisementVariantId,
        long parentId,
        CancellationToken cancellationToken = default)
    {
        if (await _itemExistsRepository
                .ExistsAsync(
                    advertisementid, advertisementVariantId,
                    parentId,
                    cancellationToken))
        {
            return Result<bool>.AlreadyExists(
                EntityNameExtensions.GetLocalizedName<TEntity>());
        }

        return Result<bool>.Success(true);
    }

    public async Task<Result<bool>> ExistsByParentAsync(
        long itemId,
        long parentId,
        CancellationToken cancellationToken = default)
    {
        if (!await _itemExistsRepository
                .ExistsByParentAsync(itemId,
                    parentId, cancellationToken))
        {
            return Result<bool>.NotFound(
                EntityNameExtensions.GetLocalizedName<TEntity>());
        }

        return Result<bool>.Success(true);
    }

    public async Task<Result<bool>> NotExistsByParentAsync(
        long itemId,
        long parentId,
        CancellationToken cancellationToken = default)
    {
        if (await _itemExistsRepository
                .ExistsByParentAsync(itemId,
                    parentId, cancellationToken))
        {
            return Result<bool>.AlreadyExists(
                EntityNameExtensions.GetLocalizedName<TEntity>());
        }

        return Result<bool>.Success(true);
    }
}
