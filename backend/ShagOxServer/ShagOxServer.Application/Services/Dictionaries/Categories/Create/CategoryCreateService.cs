using ShagOxServer.Application.DTOs.Common.Responses;
using ShagOxServer.Application.DTOs.Dictionaries.Categories.Create;
using ShagOxServer.Application.Interfaces.Persistences;
using ShagOxServer.Application.Interfaces.Repositories.Dictionaries.Categories;
using ShagOxServer.Application.Interfaces.Services.Dictionaries.Categories.Create;
using ShagOxServer.Application.Services.Dictionaries.Categories.Validator;
using ShagOxServer.Application.Services.Dictionaries.ProductTypes.Validator;
using ShagOxServer.SharedKernel.Abstractions.Results;


namespace ShagOxServer.Application.Services.Dictionaries.Categories.Create;
public class CategoryCreateService : ICategoryCreateService
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly CategoryValidator _categoryValidator;
    private readonly ProductTypeValidator _productTypeValidator;


    private readonly IUnitOfWork _unitOfWork;


    public CategoryCreateService(
        ICategoryRepository categoryRepository,
        CategoryValidator categoryValidator,
        ProductTypeValidator productTypeValidator,
        IUnitOfWork unitOfWork)
    {
        _categoryRepository = categoryRepository;
        _categoryValidator = categoryValidator;
        _productTypeValidator = productTypeValidator;
        _unitOfWork = unitOfWork;
    }


    public async Task<Result<CreateResponse>> CreateAsync(
        CategoryCreateRequest request)
    {
        var typeValidator = await _productTypeValidator
           .ExistsByIdAsync(request.ProductTypeId);

        if (!typeValidator.IsSuccess)
            Result<CreateResponse>.Fail(typeValidator.Error);


        var nameValidator = await _categoryValidator
            .NotExistsAsync(request.Name, request.ProductTypeId);

        if (!nameValidator.IsSuccess)
            Result<CreateResponse>.Fail(nameValidator.Error);  


        var category = CategoryCreater.Create(request);

        await _unitOfWork.BeginTransactionAsync();

        try
        {
            _categoryRepository.Add(category);

            await _unitOfWork.CommitAsync();
        }
        catch
        {
            await _unitOfWork.RollbackAsync();
            throw;
        }

        var response = new CreateResponse(
            category.Id,
            DateTime.UtcNow
        );

        return Result<CreateResponse>.Success(response);
    }
}