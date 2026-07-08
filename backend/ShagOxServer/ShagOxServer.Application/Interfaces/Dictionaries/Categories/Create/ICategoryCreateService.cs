using ShagOxServer.Application.DTOs.Dictionaries.Categories.Create;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Interfaces.Dictionaries.Categories.Create;
public interface ICategoryCreateService
{
    Task<Result<CategoryCreateResponse>> CreateCategoryAsync(
        CategoryCreateRequest request);
}
