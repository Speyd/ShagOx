using ShagOxServer.Application.Common.Results;
using ShagOxServer.Application.Common.Results.Extensions;
using ShagOxServer.Application.DTOs.Advertisements;
using ShagOxServer.Application.Interfaces.Advertisements.Query;
using ShagOxServer.Application.Services.Advertisements.Mapping;
using ShagOxServer.Infrastructure.Interfaces.Advertisements;

namespace ShagOxServer.Application.Services.Advertisements.Query;

public class AdvertisementQueryService : IAdvertisementQueryService
{
    private readonly IAdvertisementRepository _repository;

    public AdvertisementQueryService(
        IAdvertisementRepository advertisementRepository)
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
        int categoryId)
    {
        var adverts = await _repository.GetByCategoryAsync(categoryId);

        return adverts.ToResultList(AdvertisementMapper.ToDto);
    }

    public async Task<Result<AdvertisementDto>> GetByIdAsync(
        int id)
    {
        var advert = await _repository.GetByIdAsync(id);

        return advert.ToResult(AdvertisementMapper.ToDto);
    }

    public async Task<Result<AdvertisementDto>> GetSellerAdvertisementsAsync(
        int userId)
    {
        var advert = await _repository.GetSellerAdvertisementsAsync(userId);

        return advert.ToResult(AdvertisementMapper.ToDto);
    }
    public async Task<Result<AdvertisementDto>> GetPurchasedAdvertisementsAsync(
        int userId)
    {
        var advert = await _repository.GetSellerAdvertisementsAsync(userId);

        return advert.ToResult(AdvertisementMapper.ToDto);
    }

    public async Task<Result<List<AdvertisementDto>>> SearchAsync(
        string query)
    {
        var adverts = await _repository.SearchAsync(query);

        return adverts.ToResultList(AdvertisementMapper.ToDto);
    }
}
