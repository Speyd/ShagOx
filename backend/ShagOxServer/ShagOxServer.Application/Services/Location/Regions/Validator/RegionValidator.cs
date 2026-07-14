using ShagOxServer.Application.Interfaces.Repositories.Location.Regions;
using ShagOxServer.Domain.Entities.Location;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Services.Location.Regions.Validator;
public class RegionValidator
{
    private readonly IRegionRepository _regionRepository;
    private readonly IRegionExistsRepository _regionExistsRepository;


    public RegionValidator(
        IRegionRepository regionRepository,
        IRegionExistsRepository regionExistsRepository)
    {
        _regionRepository = regionRepository;
        _regionExistsRepository = regionExistsRepository;
    }


    public async Task<Result<Region>> GetRegionValidator(
        int regionId)
    {
        var region = await _regionRepository.GetByIdAsync(regionId);
        if (region is null)
            return Result<Region>.NotFound("Region");

        return Result<Region>.Success(region);
    }

    public async Task<Result<bool>> ExistsRegionValidator(
       int regionId)
    {
        var region = await _regionExistsRepository.ExistsAsync(regionId);
        if (!region)
            return Result<bool>.AlreadyExists("Region");

        return Result<bool>.Success(region);
    }

    public async Task<Result<bool>> ExistsRegionByNameValidator(
       string name)
    {
        var region = await _regionExistsRepository.ExistsAsync(name);
        if (!region)
            return Result<bool>.AlreadyExists("Region");

        return Result<bool>.Success(region);
    }
}