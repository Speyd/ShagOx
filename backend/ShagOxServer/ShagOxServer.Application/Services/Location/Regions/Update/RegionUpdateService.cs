using ShagOxServer.Application.DTOs.Location.Regions.Update;
using ShagOxServer.Application.Interfaces.Repositories.Location.Regions;
using ShagOxServer.Application.Interfaces.Services.Location.Regions.Update;
using ShagOxServer.Domain.Entities.Location;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Services.Location.Regions.Update;

public class RegionUpdateService : IRegionUpdateService
{
    private readonly IRegionRepository _repository;
    private readonly IRegionExistsRepository _existsRepository;


    public RegionUpdateService(
        IRegionRepository regionRepository,
        IRegionExistsRepository existsRepository)
    {
        _repository = regionRepository;
        _existsRepository = existsRepository;
    }

    public async Task<Result<RegionUpdateResponse>> UpdateRegionAsync(
        int regionId,
        RegionUpdateRequest request)
    {
        var region = await _repository.GetByIdAsync(regionId);

        if (region is null)
            return Result<RegionUpdateResponse>.NotFound("Region");

        var valid = await _existsRepository.ExistsAsync(request.Name);
        if (valid && request.Name is not null ||
            request.Name is null)
            return Result<RegionUpdateResponse>.Fail(
                "Name ist exists or null");

        var updatedCount = ApplyUpdates(region, request);

        var result = new RegionUpdateResponse(
                DateTime.UtcNow,
                updatedCount
            );

        if (updatedCount == 0)
            return Result<RegionUpdateResponse>.Success(result);

        await _repository.UpdateAsync(region);

        return Result<RegionUpdateResponse>.Success(result);
    }
    
    private static int ApplyUpdates(
        Region region,
        RegionUpdateRequest request)
    {
        int countUpdated = 0;

        if (request.Name is not null)
        {
            region.Name = request.Name;
            countUpdated++;
        }

        return countUpdated;
    }
}
