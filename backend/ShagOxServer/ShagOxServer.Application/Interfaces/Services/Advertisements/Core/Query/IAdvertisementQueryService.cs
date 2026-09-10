using ShagOxServer.Application.DTOs.Advertisements.Core;
using ShagOxServer.Application.DTOs.Advertisements.Favorites;
using ShagOxServer.Application.Interfaces.Services.Base;
using ShagOxServer.Domain.Filters.Advertisements;
using ShagOxServer.SharedKernel.Abstractions.Paginations;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Interfaces.Services.Advertisements.Core.Query;
public interface IAdvertisementQueryService
    : IQueryService<AdvertisementDto>
{
    Task<Result<PagedResult<AdvertisementDto>>> GetBySellerAsync(
        int userId,
        PaginationParams pagination);

    Task<Result<PagedResult<AdvertisementDto>>> GetPurchasedByUserAsync(
        int userId,
        PaginationParams pagination);

    Task<Result<PagedResult<AdvertisementDto>>> Search(
        AdvertisementSearchFilter filter,
        PaginationParams pagination);
}