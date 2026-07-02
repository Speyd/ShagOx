using ShagOxServer.Application.DTOs.Advertisements;
using ShagOxServer.Application.Interfaces.Advertisements.Query;
using ShagOxServer.Application.Services.Advertisements.Mapping;
using ShagOxServer.Infrastructure.Interfaces.Advertisements;
using ShagOxServer.SharedKernel.Abstractions.Paginations;
using ShagOxServer.SharedKernel.Abstractions.Results;
using ShagOxServer.SharedKernel.Abstractions.Results.Extensions;

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
        PaginationParams pagination)
    {
        var adverts = await _repository.GetPagedAsync(pagination);

        return adverts.ToResultList(AdvertisementMapper.ToDto);
    }

    public async Task<Result<List<AdvertisementDto>>> GetByCategoryAsync(
        int categoryId,
        PaginationParams pagination)
    {
        var adverts = await _repository.GetByCategoryAsync(categoryId, pagination);

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
        PaginationParams pagination)
    {
        var advert = await _repository.GetSellerAdvertisementsAsync(userId, pagination);

        return advert.ToResultList(AdvertisementMapper.ToDto);
    }
    public async Task<Result<List<AdvertisementDto>>> GetPurchasedAdvertisementsAsync(
        int userId,
        PaginationParams pagination)
    {
        var advert = await _repository.GetSellerAdvertisementsAsync(userId, pagination);

        return advert.ToResultList(AdvertisementMapper.ToDto);
    }

    public async Task<Result<List<AdvertisementDto>>> SearchByTitle(
        string title,
        PaginationParams pagination)
    {
        var advert = await _repository.SearchByTitle(title, pagination);

        return advert.ToResultList(AdvertisementMapper.ToDto);
    }

    public async Task<Result<List<AdvertisementDto>>> SearchByDescription(
        string query,
        PaginationParams pagination)
    {
        var advert = await _repository.SearchByDescription(query, pagination);

        return advert.ToResultList(AdvertisementMapper.ToDto);
    }
}
