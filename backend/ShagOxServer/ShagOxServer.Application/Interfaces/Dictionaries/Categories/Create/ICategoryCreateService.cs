using ShagOxServer.SharedKernel.Results;
using ShagOxServer.Application.DTOs.Dictionaries.Categories.Create;

namespace ShagOxServer.Application.Interfaces.Dictionaries.Categories.Create;
public interface ICategoryCreateService
{
    Task<Result<CategoryCreateResponse>> CreateCategoryAsync(
        CategoryCreateRequest request);
}
