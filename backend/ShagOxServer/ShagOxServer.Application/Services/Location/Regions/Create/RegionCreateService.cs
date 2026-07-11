using ShagOxServer.Application.DTOs.Location.Regions.Create;
using ShagOxServer.Application.Interfaces.Persistences;
using ShagOxServer.Application.Interfaces.Repositories.Location.Regions;
using ShagOxServer.Application.Interfaces.Services.Location.Regions.Create;
using ShagOxServer.Domain.Entities.Location;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Services.Location.Regions.Create;
public class RegionCreateService : IRegionCreateService
{
    private readonly IRegionRepository _repository;
    private readonly IRegionExistsRepository _existsRepository;

    private readonly IUnitOfWork _unitOfWork;


    public RegionCreateService(
        IRegionRepository regionRepository,
        IRegionExistsRepository existsRepository,
        IUnitOfWork unitOfWork)
    {
        _repository = regionRepository;
        _existsRepository = existsRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<RegionCreateResponse>> CreateRegionAsync(
        RegionCreateRequest request)
    {
        var validation = await _existsRepository.ExistsAsync(request.Name);
        if (validation)
            return Result<RegionCreateResponse>.AlreadyExists("Region");

        var region = CreateRegion(request);

        await _unitOfWork.BeginTransactionAsync();

        try
        {
            _repository.Add(region);

            await _unitOfWork.CommitAsync();
        }
        catch
        {
            await _unitOfWork.RollbackAsync();
            throw;
        }

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