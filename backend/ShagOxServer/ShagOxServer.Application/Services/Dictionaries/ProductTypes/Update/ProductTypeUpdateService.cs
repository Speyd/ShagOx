using ShagOxServer.Application.DTOs.Common.Responses;
using ShagOxServer.Application.DTOs.Dictionaries.ProductTypes.Update;
using ShagOxServer.Application.Interfaces.Persistences;
using ShagOxServer.Application.Interfaces.Repositories.Dictionaries.ProductTypes;
using ShagOxServer.Application.Interfaces.Services.Dictionaries.ProductTypes.Update;
using ShagOxServer.Application.Services.Dictionaries.ProductTypes.Validator;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Services.Dictionaries.ProductTypes.Update;
public class ProductTypeUpdateService 
    : IProductTypeUpdateService
{
    private readonly IProductTypeRepository _productTypeRepository;
    private readonly ProductTypeValidator _productTypeValidator;
    private readonly IUnitOfWork _unitOfWork;


    public ProductTypeUpdateService(
        IProductTypeRepository productTypeRepository,
        ProductTypeValidator productTypeValidator,
        IUnitOfWork unitOfWork)
    {
        _productTypeRepository = productTypeRepository;
        _productTypeValidator = productTypeValidator;
        _unitOfWork = unitOfWork;
    }


    public async Task<Result<UpdateResponse>> UpdateAsync(
        int productTypeId,
        ProductTypeUpdateRequest request)
    {
        var productType = await _productTypeValidator
            .GetByIdAsync(productTypeId);

        if (!productType.IsSuccess)
            return Result<UpdateResponse>.Fail(productType.Error);


        if (request.Name is not null)
        {
            var nameValidator = await _productTypeValidator
                .NotExistsByNameAsync(request.Name);

            if (!nameValidator.IsSuccess)
                return Result<UpdateResponse>.Fail(nameValidator.Error);
        }


        var updatedCount = ProductTypeUpdater
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
            _productTypeRepository.Update(productType.Value!);

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