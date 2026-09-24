using Microsoft.Extensions.Logging;
using ShagOxServer.Application.DTOs.Base.Responses;
using ShagOxServer.Application.DTOs.Dictionaries.ProductTypes.Translations.Create;
using ShagOxServer.Application.Interfaces.Persistences;
using ShagOxServer.Application.Interfaces.Repositories.Base;
using ShagOxServer.Application.Interfaces.Services.Dictionaries.ProductTypes.Translations.Create;
using ShagOxServer.Application.Resources.EntityErrorResourcess;
using ShagOxServer.Application.Services.Dictionaries.ProductTypes.Translations.Validator;
using ShagOxServer.Domain.Entities.Dictionaries.Translations;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Services.Dictionaries.ProductTypes.Translations.Create;
public class ProductTypeTranslationCreateService
    : IProductTypeTranslationCreateService
{
    private readonly IRepository<ProductTypeTranslation> _typeRepository;
    private readonly ProductTypeTranslationValidator _typeValidator;

    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<ProductTypeTranslationCreateService> _logger;


    public ProductTypeTranslationCreateService(
        IRepository<ProductTypeTranslation> typeRepository,
        ProductTypeTranslationValidator typeValidator,
        IUnitOfWork unitOfWork,
        ILogger<ProductTypeTranslationCreateService> logger)
    {
        _typeRepository = typeRepository;
        _typeValidator = typeValidator;

        _unitOfWork = unitOfWork;
        _logger = logger;
    }


    public async Task<Result<CreateResponse>> CreateAsync(
        ProductTypeTranslationCreateRequest request)
    {
        var codeValidation = await _typeValidator
            .NotExistsAsync(request.TranslatableId, request.Language);

        if (!codeValidation.IsSuccess)
            return Result<CreateResponse>.Fail(codeValidation.Error);


        var productType = ProductTypeTranslationCreater
            .Create(request);

        await _unitOfWork.BeginTransactionAsync();
        try
        {
            _typeRepository.Add(productType);

            await _unitOfWork.CommitAsync();
        }
        catch(Exception ex)
        {
            await _unitOfWork.RollbackAsync();

            _logger.LogError(
                ex,
                "Failed to create product type translation. " +
                "TranslatableId: {TranslatableId}",
                request.TranslatableId);

            return Result<CreateResponse>
                .Fail(EntityErrorResources.ProductTypeTranslationCreateFailed);
        }

        return Result<CreateResponse>.Success(
            new CreateResponse(
                productType.Id,
                DateTime.UtcNow
        ));
    }
}