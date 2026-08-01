using ShagOxServer.Application.DTOs.Common.Responses;
using ShagOxServer.Application.Interfaces.Persistences;
using ShagOxServer.Application.Interfaces.Repositories.Dictionaries.ProductTypes;
using ShagOxServer.Application.Interfaces.Services.Dictionaries.ProductTypes.Delete;
using ShagOxServer.Application.Services.Dictionaries.ProductTypes.Validator;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Services.Dictionaries.ProductTypes.Delete;
public class ProductTypeDeleteService 
    : IProductTypeDeleteService
{
    private readonly IProductTypeRepository _productTypeRepository;
    private readonly ProductTypeValidator _productTypeValidator;

    private readonly IUnitOfWork _unitOfWork;


    public ProductTypeDeleteService(
        IProductTypeRepository productTypeRepository,
        ProductTypeValidator productTypeValidator,
        IUnitOfWork unitOfWork)
    {
        _productTypeRepository = productTypeRepository;
        _productTypeValidator = productTypeValidator;
        _unitOfWork = unitOfWork;
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
        catch
        {
            await _unitOfWork.RollbackAsync();
            throw;
        }

        return Result<DeleteResponse>.Success(
            new DeleteResponse(
                productType.Value!.Id,
                DateTime.UtcNow
            )
        );
    }
}