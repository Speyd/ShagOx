using ShagOxServer.Application.DTOs.Dictionaries.Categories.Delete;
using ShagOxServer.SharedKernel.Results;

namespace ShagOxServer.Application.Interfaces.Dictionaries.Categories.Delete;
public interface ICategoryDeleteService
{
    Task<Result<CategoryDeleteResponse>> DeleteCategoryAsync(
        int id);
}
