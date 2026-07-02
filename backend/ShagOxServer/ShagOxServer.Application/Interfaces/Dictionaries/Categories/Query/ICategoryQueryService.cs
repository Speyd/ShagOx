using ShagOxServer.Application.DTOs.Dictionaries.Categories;
using ShagOxServer.Domain.Entities.Dictionaries.Enum;
using ShagOxServer.SharedKernel.Results;

namespace ShagOxServer.Application.Interfaces.Dictionaries.Categories.Query;
public interface ICategoryQueryService
{
    Task<Result<CategoryDto>> GetByIdAsync(int id);

    Task<Result<CategoryDto>> GetByNameAsync(string name);

    Task<Result<List<CategoryDto>>> GetByProductTypeAsync(
        ProductType type);

    Task<Result<List<CategoryDto>>> SearchByName(
        string name,
        int page,
        int pageSize);
}
