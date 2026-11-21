using ShagOxServer.Application.DTOs.Specification.Pictures.CategoryImages.Query;
using ShagOxServer.Application.Interfaces.Services.Base.Query;
using ShagOxServer.Domain.Base.Enum;
using ShagOxServer.Domain.Entities.Specification.Pictures;
using ShagOxServer.Domain.Filters.Specification.Pictures.CategoryImages;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Interfaces.Services.Specification.Pictures.CategoryImages.Query;
public interface ICategoryImageQueryService
    : IQueryService<CategoryImageDto,
        CategoryImage, 
        CategoryImageSearchFilter>
{
    Task<Result<CategoryImageDto>> GetByCategoryIdAsync(
        long categoryId,
        CancellationToken cancellationToken = default,
        EntityStatus status = EntityStatus.Active);
}
