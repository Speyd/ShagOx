using ShagOxServer.Application.DTOs.Dictionaries.Categories.Delete;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Interfaces.Services.Dictionaries.Categories.Delete;
public interface ICategoryDeleteService
{
    Task<Result<CategoryDeleteResponse>> DeleteCategoryAsync(
        int id);
}
