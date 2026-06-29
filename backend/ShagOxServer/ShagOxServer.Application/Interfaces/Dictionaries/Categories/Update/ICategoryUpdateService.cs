using ShagOxServer.Application.Common.Results;
using ShagOxServer.Application.DTOs.Dictionaries.Categories.Update;

namespace ShagOxServer.Application.Interfaces.Dictionaries.Categories.Update;
public interface ICategoryUpdateService
{
    Task<Result<CategoryUpdateResponse>> UpdateCategoryAsync(
        int categoryId,
        CategoryUpdateRequest request);
}
