using ShagOxServer.Application.DTOs.Dictionaries.Categories.Create;
using ShagOxServer.Application.Interfaces.Persistences;
using ShagOxServer.Application.Interfaces.Repositories.Dictionaries.Categories;
using ShagOxServer.Application.Interfaces.Services.Dictionaries.Categories.Create;
using ShagOxServer.Application.Services.Dictionaries.Categories.Validator;
using ShagOxServer.SharedKernel.Abstractions.Results;


namespace ShagOxServer.Application.Services.Dictionaries.Categories.Create;
public class CategoryCreateService : ICategoryCreateService
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly CategoryValidator _validator;

    private readonly IUnitOfWork _unitOfWork;


    public CategoryCreateService(
        ICategoryRepository categoryRepository,
        CategoryValidator validator,
        IUnitOfWork unitOfWork)
    {
        _categoryRepository = categoryRepository;
        _validator = validator;
        _unitOfWork = unitOfWork;
    }


    public async Task<Result<CategoryCreateResponse>> CreateAsync(
        CategoryCreateRequest request)
    {
        var exists = await _validator.NotExistsAsync(request.Name, request.ProductType);
        if (!exists.IsSuccess)
            Result<CategoryCreateResponse>.Fail(exists.Error ?? "");

        var category = CategoryCreater.CreateCategory(request);

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

        var response = new CategoryCreateResponse(
            category.Id,
            DateTime.UtcNow
        );

        return Result<CategoryCreateResponse>.Success(response);
    }
}