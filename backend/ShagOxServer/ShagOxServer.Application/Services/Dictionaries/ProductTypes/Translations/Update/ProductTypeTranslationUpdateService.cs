using ShagOxServer.Application.DTOs.Base.Responses;
using ShagOxServer.Application.DTOs.Dictionaries.ProductTypes.Translations.Update;
using ShagOxServer.Application.Interfaces.Persistences;
using ShagOxServer.Application.Interfaces.Repositories.Base;
using ShagOxServer.Application.Interfaces.Services.Dictionaries.ProductTypes.Translations.Update;
using ShagOxServer.Application.Services.Base.Translations;
using ShagOxServer.Application.Services.Dictionaries.ProductTypes.Translations.Validator;
using ShagOxServer.Application.Services.Dictionaries.ProductTypes.Validator;
using ShagOxServer.Domain.Entities.Dictionaries;
using ShagOxServer.Domain.Entities.Dictionaries.Translations;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Services.Dictionaries.ProductTypes.Translations.Update;
public class ProductTypeTranslationUpdateService
    : BaseTranslationUpdateSerivce<ProductType, ProductTypeTranslation>,
    IProductTypeTranslationUpdateService
{
    private readonly IRepository<ProductTypeTranslation> _typeRepository;
    private readonly ProductTypeTranslationValidator _typeTranslationValidator;


    private readonly IUnitOfWork _unitOfWork;


    public ProductTypeTranslationUpdateService(
        IRepository<ProductTypeTranslation> typeRepository,
        ProductTypeTranslationValidator typeTranslationValidator,
        ProductTypeValidator typeValidator,
        IUnitOfWork unitOfWork
    ) : base(typeValidator, typeTranslationValidator)
    {
        _typeRepository = typeRepository;
        _typeTranslationValidator = typeTranslationValidator;
        _unitOfWork = unitOfWork;
    }


    public async Task<Result<UpdateResponse>> UpdateAsync(
        int statusTranslationId,
        ProductTypeTranslationUpdateRequest request)
    {
        var productType = await _typeTranslationValidator
            .GetByIdAsync(statusTranslationId);

        if (!productType.IsSuccess)
            return Result<UpdateResponse>.Fail(productType.Error);


        var validation = await
             ValidateUpdatesAsync(productType.Value!, request);

        if (!validation.IsSuccess)
            return Result<UpdateResponse>.Fail(validation.Error);


        var updatedCount = ProductTypeTranslationUpdater
            .ApplyUpdates(productType.Value!, request);

        var result = new UpdateResponse(
            updatedCount,
            DateTime.UtcNow
        );

        if (updatedCount == 0)
            return Result<UpdateResponse>.Success(result);

        await _unitOfWork.BeginTransactionAsync();
        try
        {
            _typeRepository.Update(productType.Value!);

            await _unitOfWork.CommitAsync();
        }
        catch
        {
            await _unitOfWork.RollbackAsync();
            throw;
        }

        return Result<UpdateResponse>.Success(result);
    }
}