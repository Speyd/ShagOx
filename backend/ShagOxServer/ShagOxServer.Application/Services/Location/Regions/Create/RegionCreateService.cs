using ShagOxServer.Application.Common.Results;
using ShagOxServer.Application.DTOs.Location.Regions.Create;
using ShagOxServer.Application.Interfaces.Location.Regions.Create;
using ShagOxServer.Domain.Entities.Location;
using ShagOxServer.Infrastructure.Interfaces.Location.Regions;

namespace ShagOxServer.Application.Services.Location.Regions.Create;
public class RegionCreateService : IRegionCreateService
{
    private readonly IRegionRepository _repository;

    public RegionCreateService(
        IRegionRepository regionRepository)
    {
        _repository = regionRepository;
    }

    public async Task<Result<RegionCreateResponse>> CreateRegionAsync(
        RegionCreateRequest request)
    {
        var validation = await _repository.ExistsAsync(request.Name);
        if (validation)
            return Result<RegionCreateResponse>.Fail("A region with that name has already been created");

        var region = CreateRegion(request);

        await _repository.AddAsync(region);

        var response = new RegionCreateResponse(
            region.Id,
            DateTime.UtcNow
        );

        return Result<RegionCreateResponse>.Success(response);
    }

    private Region CreateRegion(
        RegionCreateRequest request)
    {
        return new Region
        {
            Name = request.Name
        };
    }
}
