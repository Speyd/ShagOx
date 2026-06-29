using ShagOxServer.Application.Common.Results;
using ShagOxServer.Application.DTOs.Dictionaries.Categories;
using ShagOxServer.Domain.Entities.Dictionaries.Enum;

namespace ShagOxServer.Application.Interfaces.Dictionaries.Categories.Query;
public interface ICategoryQueryService
{
    Task<Result<CategoryDto>> GetByIdAsync(int id);

    Task<Result<List<CategoryDto>>> GetByProductTypeAsync(
        ProductType type);
}
