using Microsoft.Extensions.Logging;
using ShagOxServer.Application.DTOs.Base.Responses;
using ShagOxServer.Application.Interfaces.Persistences;
using ShagOxServer.Application.Interfaces.Repositories.Base;
using ShagOxServer.Application.Interfaces.Services.Dictionaries.ProductTypes.Translations.Delete;
using ShagOxServer.Application.Resources.EntityErrorResourcess;
using ShagOxServer.Application.Services.Dictionaries.ProductTypes.Translations.Validator;
using ShagOxServer.Domain.Entities.Dictionaries.Translations;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Services.Dictionaries.ProductTypes.Translations.Delete;
public class ProductTypeTranslationDeleteService
    : IProductTypeTranslationDeleteService
{
    private readonly IRepository<ProductTypeTranslation> _typeRepository;
    private readonly ProductTypeTranslationValidator _typeValidator;

    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<ProductTypeTranslationDeleteService> _logger;


    public ProductTypeTranslationDeleteService(
        IRepository<ProductTypeTranslation> typeRepository,
        ProductTypeTranslationValidator typeValidator,
        IUnitOfWork unitOfWork,
        ILogger<ProductTypeTranslationDeleteService> logger)
    {
        _typeRepository = typeRepository;
        _typeValidator = typeValidator;
        _unitOfWork = unitOfWork;
        _logger = logger;
    }


    public async Task<Result<DeleteResponse>> DeleteAsync(
        int id)
    {
        var attributeType = await _typeValidator
            .GetByIdAsync(id);

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

            return Result<DeleteResponse>
                .Fail(EntityErrorResources.ProductTypeTranslationDeleteFailed);
        }

        return Result<DeleteResponse>.Success(
           new DeleteResponse(
               attributeType.Value!.Id,
               DateTime.UtcNow
           )
       );
    }
}