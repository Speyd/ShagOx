using ShagOxServer.Application.DTOs.Common.Responses;
using ShagOxServer.Application.DTOs.Dictionaries.ProductTypes.Create;
using ShagOxServer.Application.Interfaces.Persistences;
using ShagOxServer.Application.Interfaces.Repositories.Dictionaries.ProductTypes;
using ShagOxServer.Application.Interfaces.Services.Dictionaries.ProductTypes.Create;
using ShagOxServer.Application.Services.Dictionaries.ProductTypes.Validator;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Services.Dictionaries.ProductTypes.Create;

public class ProductTypeCreateService 
    : IProductTypeCreateService
{
    private readonly IProductTypeRepository _productTypeRepository;
    private readonly ProductTypeValidator _validator;

    private readonly IUnitOfWork _unitOfWork;


    public ProductTypeCreateService(
        IProductTypeRepository productTypeRepository,
        ProductTypeValidator validator,
        IUnitOfWork unitOfWork)
    {
        _productTypeRepository = productTypeRepository;
        _validator = validator;
        _unitOfWork = unitOfWork;
    }


    public async Task<Result<CreateResponse>> CreateAsync(
       ProductTypeCreateRequest request)
    {
        var exists = await _validator
            .NotExistsByNameAsync(request.Name);

        if (!exists.IsSuccess)
            Result<CreateResponse>.Fail(exists.Error);


        var productType = ProductTypeCreater.Create(request);

        await _unitOfWork.BeginTransactionAsync();

        try
        {
            _productTypeRepository.Add(productType);

            await _unitOfWork.CommitAsync();
        }
        catch
        {
            await _unitOfWork.RollbackAsync();
            throw;
        }

        return Result<CreateResponse>.Success(
            new CreateResponse(
                productType.Id,
                DateTime.UtcNow
        ));
    }
}