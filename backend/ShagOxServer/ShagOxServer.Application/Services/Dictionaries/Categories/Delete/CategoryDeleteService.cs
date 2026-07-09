using ShagOxServer.Application.DTOs.Dictionaries.Categories.Delete;
using ShagOxServer.Application.Interfaces.Repositories.Dictionaries.Categories;
using ShagOxServer.Application.Interfaces.Services.Dictionaries.Categories.Delete;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Services.Dictionaries.Categories.Delete;
public class CategoryDeleteService : ICategoryDeleteService
{
    private readonly ICategoryRepository _repository;

    public CategoryDeleteService(
        ICategoryRepository categoryRepository)
    {
        _repository = categoryRepository;
    }

    public async Task<Result<CategoryDeleteResponse>> DeleteCategoryAsync(
        int id)
    {
        var category = await _repository.GetByIdAsync(id);
        if (category is null)
            return Result<CategoryDeleteResponse>.NotFound("Category");

        await _repository.DeleteAsync(category);

        return Result<CategoryDeleteResponse>.Success(
          new CategoryDeleteResponse(
              category.Id,
              DateTime.UtcNow
          )
      );
    }
}
