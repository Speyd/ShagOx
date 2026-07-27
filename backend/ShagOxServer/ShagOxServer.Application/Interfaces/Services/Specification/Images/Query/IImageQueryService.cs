using ShagOxServer.Application.DTOs.Specification.Images;
using ShagOxServer.SharedKernel.Abstractions.Paginations;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Interfaces.Services.Specification.Images.Query;
public interface IImageQueryService
{
    Task<Result<ImageDto>> GetByIdAsync(int id);

    Task<Result<PagedResult<ImageDto>>> GetPagedAsync(
        PaginationParams pagination);
}