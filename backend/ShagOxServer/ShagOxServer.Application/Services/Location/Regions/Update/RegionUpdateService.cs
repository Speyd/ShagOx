using ShagOxServer.Application.DTOs.Location.Regions.Update;
using ShagOxServer.Application.Interfaces.Persistences;
using ShagOxServer.Application.Interfaces.Repositories.Location.Regions;
using ShagOxServer.Application.Interfaces.Services.Location.Regions.Update;
using ShagOxServer.Application.Services.Location.Regions.Validator;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Services.Location.Regions.Update;
public class RegionUpdateService : IRegionUpdateService
{
    private readonly IRegionRepository _repository;
    private readonly RegionValidator _validator;

    private readonly IUnitOfWork _unitOfWork;


    public RegionUpdateService(
        IRegionRepository regionRepository,
        RegionValidator validator,
        IUnitOfWork unitOfWork)
    {
        _repository = regionRepository;
        _validator = validator;
        _unitOfWork = unitOfWork;
    }


    public async Task<Result<RegionUpdateResponse>> UpdateRegionAsync(
        int regionId,
        RegionUpdateRequest request)
    {
        var region = await _validator.GetByIdAsync(regionId);
        if (!region.IsSuccess)
            return Result<RegionUpdateResponse>.Fail(region.Error ?? "");

        if (request.Name is not null)
        {
            var valid = await _validator.NotExistsByNameAsync(request.Name);
            if (!valid.IsSuccess)
                return Result<RegionUpdateResponse>.Fail(valid.Error ?? "");
        }

        var updatedCount = RegionUpdater.ApplyUpdates(region.Value!, request);

        var result = new RegionUpdateResponse(
                DateTime.UtcNow,
                updatedCount
            );

        if (updatedCount == 0)
            return Result<RegionUpdateResponse>.Success(result);

        await _unitOfWork.BeginTransactionAsync();

        try
        {
            _repository.Update(region.Value!);

            await _unitOfWork.CommitAsync();
        }
        catch
        {
            await _unitOfWork.RollbackAsync();
            throw;
        }

        return Result<RegionUpdateResponse>.Success(result);
    }
}