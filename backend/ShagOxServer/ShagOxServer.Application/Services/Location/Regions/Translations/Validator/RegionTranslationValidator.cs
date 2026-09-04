using ShagOxServer.Application.Interfaces.Repositories.Base;
using ShagOxServer.Application.Interfaces.Repositories.Location.Regions.Translations;
using ShagOxServer.Domain.Entities.Location.Translations;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Services.Location.Regions.Translations.Validator;
public class RegionTranslationValidator
{
    private readonly IRepository<RegionTranslation> _regionRepository;
    private readonly IRegionTranslationExistsRepository _regionExistsRepository;


    public RegionTranslationValidator(
        IRepository<RegionTranslation> regionRepository,
        IRegionTranslationExistsRepository regionExistsRepository)
    {
        _regionRepository = regionRepository;
        _regionExistsRepository = regionExistsRepository;
    }


    public async Task<Result<RegionTranslation>> GetByIdAsync(
        int regionId)
    {
        var region = await _regionRepository.GetByIdAsync(regionId);
        if (region is null)
            return Result<RegionTranslation>.NotFound("Region Translation");

        return Result<RegionTranslation>.Success(region);
    }

    public async Task<Result<bool>> ExistsAsync(
        int regionId,
        string language)
    {
        if (!await _regionExistsRepository
            .ExistsAsync(regionId, language))
        {
            return Result<bool>.NotFound("Region Translation");
        }

        return Result<bool>.Success(true);
    }

    public async Task<Result<bool>> NotExistsAsync(
        int regionId,
        string language)
    {
        if (await _regionExistsRepository
            .ExistsAsync(regionId, language))
        {
            return Result<bool>.AlreadyExists("Region Translation");
        }

        return Result<bool>.Success(true);
    }

    public async Task<Result<bool>> ExistsByIdAsync(
        int regionId)
    {
        if (!await _regionExistsRepository
            .ExistsByIdAsync(regionId))
        {
            return Result<bool>.NotFound("Region Translation");
        }

        return Result<bool>.Success(true);
    }

    public async Task<Result<bool>> NotExistsByIdAsync(
        int regionId)
    {
        if (await _regionExistsRepository
            .ExistsByIdAsync(regionId))
        {
            return Result<bool>.AlreadyExists("Region Translation");
        }

        return Result<bool>.Success(true);
    }

    public async Task<Result<bool>> ExistsByLanguageAsync(
        string language)
    {
        if (!await _regionExistsRepository
                .ExistsByLanguageAsync(language))
        {
            return Result<bool>.NotFound("Region");
        }

        return Result<bool>.Success(true);
    }

    public async Task<Result<bool>> NotExistsByLanguageAsync(
        string language)
    {
        if (await _regionExistsRepository
                .ExistsByLanguageAsync(language))
        {
            return Result<bool>.AlreadyExists("Region");
        }

        return Result<bool>.Success(true);
    }
}