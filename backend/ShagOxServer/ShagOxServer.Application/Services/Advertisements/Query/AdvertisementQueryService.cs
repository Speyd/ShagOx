using ShagOxServer.Application.DTOs.Advertisements;
using ShagOxServer.Application.Interfaces.Repositories.Advertisements;
using ShagOxServer.Application.Interfaces.Services.Advertisements.Query;
using ShagOxServer.Application.Services.Advertisements.Mapping;
using ShagOxServer.Domain.Filters.Advertisements;
using ShagOxServer.SharedKernel.Abstractions.Paginations;
using ShagOxServer.SharedKernel.Abstractions.Results;
using ShagOxServer.SharedKernel.Abstractions.Results.Extensions;

namespace ShagOxServer.Application.Services.Advertisements.Query;
public class AdvertisementQueryService : IAdvertisementQueryService
{
    private readonly IAdvertisementQueryRepository _advertisementRepository;

    public AdvertisementQueryService(
        IAdvertisementQueryRepository advertisementRepository)
    {
        _advertisementRepository = advertisementRepository;
    }

    public async Task<Result<List<AdvertisementDto>>> GetPagedAsync(
        PaginationParams pagination)
    {
        var adverts = await _advertisementRepository
            .GetPagedAsync(pagination);

        return adverts.ToResultList(AdvertisementMapper.ToDto);
    }

    public async Task<Result<AdvertisementDto>> GetByIdAsync(
        int id)
    {
        var advert = await _advertisementRepository
            .GetByIdAsync(id);

        return advert.ToResult(AdvertisementMapper.ToDto);
    }

    public async Task<Result<List<AdvertisementDto>>> GetBySellerAsync(
        int userId,
        PaginationParams pagination)
    {
        var advert = await _advertisementRepository
            .GetBySellerAsync(userId, pagination);

        return advert.ToResultList(AdvertisementMapper.ToDto);
    }
    public async Task<Result<List<AdvertisementDto>>> GetPurchasedByUserAsync(
        int userId,
        PaginationParams pagination)
    {
        var advert = await _advertisementRepository
            .GetPurchasedByUserAsync(userId, pagination);

        return advert.ToResultList(AdvertisementMapper.ToDto);
    }

    public async Task<Result<List<AdvertisementDto>>> Search(
        AdvertisementSearchFilter filter,
        PaginationParams pagination)
    {
        var advert = await _advertisementRepository
            .Search(filter, pagination);

        return advert.ToResultList(AdvertisementMapper.ToDto);
    }
}