using ShagOxServer.Application.Common.Results;
using ShagOxServer.Application.DTOs.Location.Regions.Create;
using ShagOxServer.Application.Interfaces.Location.Regions.Create;
using ShagOxServer.Domain.Entities.Location;
using ShagOxServer.Infrastructure.Interfaces.Location.Regions;

namespace ShagOxServer.Application.Services.Location.Regions.Create;
public class RegionCreateService : IRegionCreateService
{
    private readonly IRegionRepository _repository;
    private readonly IRegionExistsRepository _existsRepository;


    public RegionCreateService(
        IRegionRepository regionRepository,
        IRegionExistsRepository existsRepository)
    {
        _repository = regionRepository;
        _existsRepository = existsRepository;
    }

    public async Task<Result<RegionCreateResponse>> CreateRegionAsync(
        RegionCreateRequest request)
    {
        var validation = await _existsRepository.ExistsAsync(request.Name);
        if (validation)
            return Result<RegionCreateResponse>.AlreadyExists("Region");

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
