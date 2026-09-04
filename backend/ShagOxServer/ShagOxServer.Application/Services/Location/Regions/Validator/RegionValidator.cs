using ShagOxServer.Application.Interfaces.Repositories.Base;
using ShagOxServer.Application.Interfaces.Repositories.Location.Regions;
using ShagOxServer.Domain.Entities.Location;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Services.Location.Regions.Validator;
public class RegionValidator
{
    private readonly IRepository<Region> _regionRepository;
    private readonly IRegionExistsRepository _regionExistsRepository;


    public RegionValidator(
        IRepository<Region> regionRepository,
        IRegionExistsRepository regionExistsRepository)
    {
        _regionRepository = regionRepository;
        _regionExistsRepository = regionExistsRepository;
    }


    public async Task<Result<Region>> GetByIdAsync(
        int regionId)
    {
        var region = await _regionRepository.GetByIdAsync(regionId);
        if (region is null)
            return Result<Region>.NotFound("Region");

        return Result<Region>.Success(region);
    }

    public async Task<Result<bool>> ExistsByIdAsync(
       int regionId)
    {
        if (!await _regionExistsRepository.ExistsByIdAsync(regionId))
            return Result<bool>.NotFound("Region");

        return Result<bool>.Success(true);
    }

    public async Task<Result<bool>> NotExistsByIdAsync(
       int regionId)
    {
        if (await _regionExistsRepository.ExistsByIdAsync(regionId))
            return Result<bool>.AlreadyExists("Region");

        return Result<bool>.Success(true);
    }

    public async Task<Result<bool>> ExistsByCodeAsync(
       string name)
    {
        if (!await _regionExistsRepository.ExistsByCodeAsync(name))
            return Result<bool>.NotFound("Region");

        return Result<bool>.Success(true);
    }

    public async Task<Result<bool>> NotExistsByCodeAsync(
       string name)
    {
        if (await _regionExistsRepository.ExistsByCodeAsync(name))
            return Result<bool>.AlreadyExists("Region");

        return Result<bool>.Success(true);
    }
}