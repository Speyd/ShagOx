using ShagOxServer.Application.DTOs.Location.Regions.Delete;
using ShagOxServer.Application.Interfaces.Persistences;
using ShagOxServer.Application.Interfaces.Repositories.Location.Regions;
using ShagOxServer.Application.Interfaces.Services.Location.Regions.Delete;
using ShagOxServer.Application.Services.Location.Regions.Validator;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Services.Location.Regions.Delete;
public class RegionDeleteService : IRegionDeleteService
{
    private readonly IRegionRepository _regionRepository;
    private readonly RegionValidator _regionValidator;

    private readonly IUnitOfWork _unitOfWork;


    public RegionDeleteService(
        IRegionRepository regionRepository,
        RegionValidator regionValidator,
        IUnitOfWork unitOfWork)
    {
        _regionRepository = regionRepository;
        _regionValidator = regionValidator;
        _unitOfWork = unitOfWork;
    }


    public async Task<Result<RegionDeleteResponse>> DeleteAsync(
        int id)
    {
        var region = await _regionValidator.GetByIdAsync(id);
        if (!region.IsSuccess)
            return Result<RegionDeleteResponse>.Fail(region.Error ?? "");

        await _unitOfWork.BeginTransactionAsync();

        try
        {
            _regionRepository.Delete(region.Value!);

            await _unitOfWork.CommitAsync();
        }
        catch
        {
            await _unitOfWork.RollbackAsync();
            throw;
        }

        return Result<RegionDeleteResponse>.Success(
           new RegionDeleteResponse(
               region.Value!.Id,
               DateTime.UtcNow
           )
       );
    }
}