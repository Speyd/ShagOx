using ShagOxServer.Application.Common.Results;
using ShagOxServer.Application.DTOs.Dictionaries.Categories.Delete;

namespace ShagOxServer.Application.Interfaces.Dictionaries.Categories.Delete;
public interface ICategoryDeleteService
{
    Task<Result<CategoryDeleteResponse>> DeleteCategoryAsync(
        int id);
}
