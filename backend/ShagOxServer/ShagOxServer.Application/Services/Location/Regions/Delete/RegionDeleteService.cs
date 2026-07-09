using ShagOxServer.Application.DTOs.Location.Regions.Delete;
using ShagOxServer.Application.Interfaces.Repositories.Location.Regions;
using ShagOxServer.Application.Interfaces.Services.Location.Regions.Delete;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Services.Location.Regions.Delete;
public class RegionDeleteService : IRegionDeleteService
{
    private readonly IRegionRepository _repository;

    public RegionDeleteService(
        IRegionRepository regionRepository)
    {
        _repository = regionRepository;
    }

    public async Task<Result<RegionDeleteResponse>> DeleteRegionAsync(
        int id)
    {
        var region = await _repository.GetByIdAsync(id);
        if (region is null)
            return Result<RegionDeleteResponse>.NotFound("Region");

        await _repository.DeleteAsync(region);
        return Result<RegionDeleteResponse>.Success(
           new RegionDeleteResponse(
               region.Id,
               DateTime.UtcNow
           )
       );
    }
}
