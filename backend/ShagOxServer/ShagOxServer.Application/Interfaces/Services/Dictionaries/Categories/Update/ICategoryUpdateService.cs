using ShagOxServer.Application.DTOs.Dictionaries.Categories.Update;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Interfaces.Services.Dictionaries.Categories.Update;
public interface ICategoryUpdateService
{
    Task<Result<CategoryUpdateResponse>> UpdateCategoryAsync(
        int categoryId,
        CategoryUpdateRequest request);
}
