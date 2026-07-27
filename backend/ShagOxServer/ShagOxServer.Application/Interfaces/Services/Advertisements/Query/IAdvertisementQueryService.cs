using ShagOxServer.Application.DTOs.Advertisements;
using ShagOxServer.Domain.Filters.Advertisements;
using ShagOxServer.SharedKernel.Abstractions.Paginations;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Interfaces.Services.Advertisements.Query;
public interface IAdvertisementQueryService
{
    Task<Result<AdvertisementDto>> GetByIdAsync(int id);

    Task<Result<PagedResult<AdvertisementDto>>> GetBySellerAsync(
        int userId,
        PaginationParams pagination);

    Task<Result<PagedResult<AdvertisementDto>>> GetPurchasedByUserAsync(
        int userId,
        PaginationParams pagination);

    Task<Result<PagedResult<AdvertisementDto>>> GetPagedAsync(
        PaginationParams pagination);

    Task<Result<PagedResult<AdvertisementDto>>> Search(
        AdvertisementSearchFilter filter,
        PaginationParams pagination);
}