using Microsoft.Extensions.Logging;
using ShagOxServer.Application.DTOs.Base.Responses;
using ShagOxServer.Application.Interfaces.Persistences;
using ShagOxServer.Application.Interfaces.Repositories.Base;
using ShagOxServer.Application.Interfaces.Services.Dictionaries.ProductTypes.Translations.Delete;
using ShagOxServer.Application.Resources.EntityErrors;
using ShagOxServer.Application.Services.Base.Delete;
using ShagOxServer.Application.Services.Base.Translations.Query.Mapping;
using ShagOxServer.Application.Services.Caches.Invalidations.Dictionaries.Translation;
using ShagOxServer.Application.Services.Dictionaries.ProductTypes.Translations.Validator;
using ShagOxServer.Domain.Entities.Dictionaries.Translations;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Services.Dictionaries.ProductTypes.Translations.Delete;
public class ProductTypeTranslationDeleteService
    : BaseDeleteService<ProductTypeTranslation>,
    IProductTypeTranslationDeleteService
{
    private readonly IRepository<ProductTypeTranslation> _typeRepository;
    private readonly ProductTypeTranslationValidator _typeValidator;

    private readonly CategoryTranslationInvalidationService _transInvalid;

    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<ProductTypeTranslationDeleteService> _logger;


    public ProductTypeTranslationDeleteService(
        IRepository<ProductTypeTranslation> typeRepository,
        ProductTypeTranslationValidator typeValidator,
        CategoryTranslationInvalidationService transInvalid,
        IUnitOfWork unitOfWork,
        ILogger<ProductTypeTranslationDeleteService> logger
    )
        : base(typeValidator, unitOfWork, logger)
    {
        _typeRepository = typeRepository;
        _typeValidator = typeValidator;
        _transInvalid = transInvalid;
        _unitOfWork = unitOfWork;
        _logger = logger;
    }


    public async Task<Result<DeleteResponse>> DeleteAsync(
        long id,
        CancellationToken cancellationToken = default)
    {
        var attributeType = await _typeValidator
            .GetByIdAsync(id, cancellationToken);

        if (!attributeType.IsSuccess)
            return Result<DeleteResponse>.Fail(attributeType.Error);

        await _unitOfWork.BeginTransactionAsync();

        try
        {
            _typeRepository.Delete(attributeType.Value!);

            await _unitOfWork.CommitAsync();
        }
        catch(Exception ex)
        {
            await _unitOfWork.RollbackAsync();

            _logger.LogError(
                ex,
                "Failed to delete product type translation. Id: {Id}",
                id);

            return Result<DeleteResponse>.Fail(
                EntityErrorResources.ProductTypeTranslationDeleteFailed);
        }

        await InvalidateCache(attributeType.Value!);

        return Result<DeleteResponse>.Success(
           new DeleteResponse(
               attributeType.Value!.Id,
               DateTime.UtcNow
           )
       );
    }

    public override async Task InvalidateCache(
        ProductTypeTranslation entity)
    {
        await _transInvalid.InvalidateDeleteAsync(
             BaseTranslationCacheMapper.ToInfo(entity));
    }
}
