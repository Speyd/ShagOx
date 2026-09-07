using ShagOxServer.Application.Interfaces.Repositories.Base;
using ShagOxServer.Application.Interfaces.Repositories.Location.Regions;
using ShagOxServer.Application.Services.Base;
using ShagOxServer.Domain.Entities.Location;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Services.Location.Regions.Validator;
public class RegionValidator
    : BaseValidator<Region>
{
    private readonly IRegionExistsRepository _regionExistsRepository;


    public RegionValidator(
        IRepository<Region> regionRepository,
        IRegionExistsRepository regionExistsRepository
    ) : base(regionRepository, regionExistsRepository)
    {
        _regionExistsRepository = regionExistsRepository;
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