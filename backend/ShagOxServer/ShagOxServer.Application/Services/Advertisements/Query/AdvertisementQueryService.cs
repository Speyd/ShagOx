using ShagOxServer.Application.Common.Results;
using ShagOxServer.Application.Common.Results.Extensions;
using ShagOxServer.Application.DTOs.Advertisements;
using ShagOxServer.Application.Interfaces.Advertisements.Query;
using ShagOxServer.Application.Services.Advertisements.Mapping;
using ShagOxServer.Infrastructure.Interfaces.Advertisements;

namespace ShagOxServer.Application.Services.Advertisements.Query;

public class AdvertisementQueryService : IAdvertisementQueryService
{
    private readonly IAdvertisementQueryRepository _repository;

    public AdvertisementQueryService(
        IAdvertisementQueryRepository advertisementRepository)
    {
        _repository = advertisementRepository;
    }

    public async Task<Result<List<AdvertisementDto>>> GetAllAsync(
        int page, int pageSize)
    {
        var adverts = await _repository.GetPagedAsync(page, pageSize);

        return adverts.ToResultList(AdvertisementMapper.ToDto);
    }

    public async Task<Result<List<AdvertisementDto>>> GetByCategoryAsync(
        int categoryId,
        int page,
        int pageSize)
    {
        var adverts = await _repository.GetByCategoryAsync(categoryId, page, pageSize);

        return adverts.ToResultList(AdvertisementMapper.ToDto);
    }

    public async Task<Result<AdvertisementDto>> GetByIdAsync(
        int id)
    {
        var advert = await _repository.GetByIdAsync(id);

        return advert.ToResult(AdvertisementMapper.ToDto);
    }

    public async Task<Result<List<AdvertisementDto>>> GetSellerAdvertisementsAsync(
        int userId,
        int page,
        int pageSize)
    {
        var advert = await _repository.GetSellerAdvertisementsAsync(userId, page, pageSize);

        return advert.ToResultList(AdvertisementMapper.ToDto);
    }
    public async Task<Result<List<AdvertisementDto>>> GetPurchasedAdvertisementsAsync(
        int userId,
        int page,
        int pageSize)
    {
        var advert = await _repository.GetSellerAdvertisementsAsync(userId, page, pageSize);

        return advert.ToResultList(AdvertisementMapper.ToDto);
    }

    public async Task<Result<List<AdvertisementDto>>> SearchByTitle(
        string title,
        int page,
        int pageSize)
    {
        var advert = await _repository.SearchByTitle(title, page, pageSize);

        return advert.ToResultList(AdvertisementMapper.ToDto);
    }

    public async Task<Result<List<AdvertisementDto>>> SearchByDescription(
        string query,
        int page,
        int pageSize)
    {
        var advert = await _repository.SearchByDescription(query, page, pageSize);

        return advert.ToResultList(AdvertisementMapper.ToDto);
    }
}
