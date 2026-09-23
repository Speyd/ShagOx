using Microsoft.Extensions.Logging;
using ShagOxServer.Application.DTOs.Base.Responses;
using ShagOxServer.Application.Interfaces.Persistences;
using ShagOxServer.Application.Interfaces.Repositories.Base;
using ShagOxServer.Application.Interfaces.Services.Dictionaries.ProductTypes.Delete;
using ShagOxServer.Application.Services.Dictionaries.ProductTypes.Validator;
using ShagOxServer.Domain.Entities.Dictionaries;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Services.Dictionaries.ProductTypes.Delete;
public class ProductTypeDeleteService 
    : IProductTypeDeleteService
{
    private readonly IRepository<ProductType> _productTypeRepository;
    private readonly ProductTypeValidator _productTypeValidator;

    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<ProductTypeDeleteService> _logger;


    public ProductTypeDeleteService(
        IRepository<ProductType> productTypeRepository,
        ProductTypeValidator productTypeValidator,
        IUnitOfWork unitOfWork,
        ILogger<ProductTypeDeleteService> logger)
    {
        _productTypeRepository = productTypeRepository;
        _productTypeValidator = productTypeValidator;
        _unitOfWork = unitOfWork;
        _logger = logger;
    }


    public async Task<Result<DeleteResponse>> DeleteAsync(
        int id)
    {
        var productType = await _productTypeValidator
            .GetByIdAsync(id);

        if (!productType.IsSuccess)
            return Result<DeleteResponse>.Fail(productType.Error);

        await _unitOfWork.BeginTransactionAsync();

        try
        {
            _productTypeRepository.Delete(productType.Value!);

            await _unitOfWork.CommitAsync();
        }
        catch(Exception ex)
        {
            await _unitOfWork.RollbackAsync();

            _logger.LogError(
               ex,
               "Failed to delete product type. Id: {Id}",
               id);

            return Result<DeleteResponse>
                .Fail("Failed to delete product type.");
        }

        return Result<DeleteResponse>.Success(
            new DeleteResponse(
                productType.Value!.Id,
                DateTime.UtcNow
            )
        );
    }
}