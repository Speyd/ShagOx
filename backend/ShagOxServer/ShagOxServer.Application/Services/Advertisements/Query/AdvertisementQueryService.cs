using ShagOxServer.Application.Common.Results;
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

    public async Task<Result<List<AdvertisementDto>>> GetAllAsync(int page, int pageSize)
    {
        var adverts = await _repository.GetPagedAsync(page, pageSize);

        return Result<List<AdvertisementDto>>.Success(
            adverts.Select(AdvertisementMapper.ToDto).ToList()
        );
    }

    public async Task<Result<List<AdvertisementDto>>> GetByCategoryAsync(int categoryId)
    {
        var adverts = await _repository.GetByCategoryAsync(categoryId);

        return Result<List<AdvertisementDto>>.Success(
            adverts.Select(AdvertisementMapper.ToDto).ToList()
        );
    }

    public async Task<Result<AdvertisementDto>> GetByIdAsync(int id)
    {
        var advert = await _repository.GetByIdAsync(id);

        if (advert is null)
            return Result<AdvertisementDto>.Fail("Advertisement not found");

        return Result<AdvertisementDto>.Success(AdvertisementMapper.ToDto(advert));
    }

    public async Task<Result<List<AdvertisementDto>>> SearchAsync(string query)
    {
        var adverts = await _repository.SearchAsync(query);

        return Result<List<AdvertisementDto>>.Success(
            adverts.Select(AdvertisementMapper.ToDto).ToList()
        );
    }
}
