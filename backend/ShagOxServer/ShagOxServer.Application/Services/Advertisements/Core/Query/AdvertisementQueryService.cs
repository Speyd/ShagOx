using ShagOxServer.Application.DTOs.Advertisements.Core;
using ShagOxServer.Application.Interfaces.Repositories.Advertisements;
using ShagOxServer.Application.Interfaces.Services.Advertisements.Core.Query;
using ShagOxServer.Application.Services.Advertisements.Core.Mapping;
using ShagOxServer.Application.Services.Base;
using ShagOxServer.Domain.Entities.Advertisements;
using ShagOxServer.Domain.Filters.Advertisements;
using ShagOxServer.SharedKernel.Abstractions.Paginations;
using ShagOxServer.SharedKernel.Abstractions.Results;
using ShagOxServer.SharedKernel.Abstractions.Results.Extensions;

namespace ShagOxServer.Application.Services.Advertisements.Core.Query;
public class AdvertisementQueryService 
    : BaseQueryService<
        AdvertisementDto, 
        Advertisement, 
        AdvertisementSearchFilter>,
      IAdvertisementQueryService
{
    private readonly IAdvertisementQueryRepository _advertisementRepository;


    public AdvertisementQueryService(
        IAdvertisementQueryRepository advertisementRepository
    )
        : base(advertisementRepository)
    {
        _advertisementRepository = advertisementRepository;
    }


    protected override AdvertisementDto ApplyMapper(
        Advertisement entity)
    {
        return AdvertisementMapper.ToDto(entity);
    }

    public async Task<Result<PagedResult<AdvertisementDto>>> GetBySellerAsync(
        long userId,
        PaginationParams pagination)
    {
        var advert = await _advertisementRepository
            .GetBySellerAsync(userId, pagination);

        return advert.ToResultPaged(ApplyMapper);
    }
    public async Task<Result<PagedResult<AdvertisementDto>>> GetPurchasedByUserAsync(
        long userId,
        PaginationParams pagination)
    {
        var advert = await _advertisementRepository
            .GetPurchasedByUserAsync(userId, pagination);

        return advert.ToResultPaged(ApplyMapper);
    }
}