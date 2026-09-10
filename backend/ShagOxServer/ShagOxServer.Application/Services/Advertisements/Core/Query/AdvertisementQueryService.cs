using ShagOxServer.Application.DTOs.Advertisements.Core;
using ShagOxServer.Application.Interfaces.Repositories.Advertisements;
using ShagOxServer.Application.Interfaces.Services.Advertisements.Core.Query;
using ShagOxServer.Application.Services.Advertisements.Core.Mapping;
using ShagOxServer.Domain.Filters.Advertisements;
using ShagOxServer.SharedKernel.Abstractions.Paginations;
using ShagOxServer.SharedKernel.Abstractions.Results;
using ShagOxServer.SharedKernel.Abstractions.Results.Extensions;

namespace ShagOxServer.Application.Services.Advertisements.Core.Query;
public class AdvertisementQueryService 
    : IAdvertisementQueryService
{
    private readonly IAdvertisementQueryRepository _advertisementRepository;


    public AdvertisementQueryService(
        IAdvertisementQueryRepository advertisementRepository)
    {
        _advertisementRepository = advertisementRepository;
    }


    public async Task<Result<PagedResult<AdvertisementDto>>> GetPagedAsync(
        PaginationParams pagination)
    {
        var adverts = await _advertisementRepository
            .GetPagedAsync(pagination);

        return adverts.ToResultPaged(AdvertisementMapper.ToDto);
    }

    public async Task<Result<AdvertisementDto>> GetByIdAsync(
        int id)
    {
        var advert = await _advertisementRepository
            .GetByIdAsync(id);

        return advert.ToResult(AdvertisementMapper.ToDto);
    }

    public async Task<Result<PagedResult<AdvertisementDto>>> GetBySellerAsync(
        int userId,
        PaginationParams pagination)
    {
        var advert = await _advertisementRepository
            .GetBySellerAsync(userId, pagination);

        return advert.ToResultPaged(AdvertisementMapper.ToDto);
    }
    public async Task<Result<PagedResult<AdvertisementDto>>> GetPurchasedByUserAsync(
        int userId,
        PaginationParams pagination)
    {
        var advert = await _advertisementRepository
            .GetPurchasedByUserAsync(userId, pagination);

        return advert.ToResultPaged(AdvertisementMapper.ToDto);
    }

    public async Task<Result<PagedResult<AdvertisementDto>>> Search(
        AdvertisementSearchFilter filter,
        PaginationParams pagination)
    {
        var advert = await _advertisementRepository
            .Search(filter, pagination);

        return advert.ToResultPaged(AdvertisementMapper.ToDto);
    }
}