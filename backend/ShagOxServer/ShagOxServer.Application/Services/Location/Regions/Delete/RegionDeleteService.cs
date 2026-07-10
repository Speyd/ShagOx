using ShagOxServer.Application.DTOs.Location.Regions.Delete;
using ShagOxServer.Application.Interfaces.Persistences;
using ShagOxServer.Application.Interfaces.Repositories.Location.Regions;
using ShagOxServer.Application.Interfaces.Services.Location.Regions.Delete;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Services.Location.Regions.Delete;
public class RegionDeleteService : IRegionDeleteService
{
    private readonly IRegionRepository _repository;
    private readonly IUnitOfWork _unitOfWork;

    public RegionDeleteService(
        IRegionRepository regionRepository,
        IUnitOfWork unitOfWork)
    {
        _repository = regionRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<RegionDeleteResponse>> DeleteRegionAsync(
        int id)
    {
        var region = await _repository.GetByIdAsync(id);
        if (region is null)
            return Result<RegionDeleteResponse>.NotFound("Region");

        await _unitOfWork.BeginTransactionAsync();

        try
        {
            _repository.Delete(region);

            await _unitOfWork.CommitAsync();
        }
        catch
        {
            await _unitOfWork.RollbackAsync();
            throw;
        }

        return Result<RegionDeleteResponse>.Success(
           new RegionDeleteResponse(
               region.Id,
               DateTime.UtcNow
           )
       );
    }
}
