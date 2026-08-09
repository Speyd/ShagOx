using ShagOxServer.Application.DTOs.Common.Responses;
using ShagOxServer.Application.DTOs.Location.Regions.Update;
using ShagOxServer.Application.Interfaces.Persistences;
using ShagOxServer.Application.Interfaces.Repositories.Base;
using ShagOxServer.Application.Interfaces.Services.Location.Regions.Update;
using ShagOxServer.Application.Services.Location.Regions.Validator;
using ShagOxServer.Domain.Entities.Location;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Services.Location.Regions.Update;
public class RegionUpdateService : IRegionUpdateService
{
    private readonly IRepository<Region> _regionRepository;
    private readonly RegionValidator _regionValidator;

    private readonly IUnitOfWork _unitOfWork;


    public RegionUpdateService(
        IRepository<Region> regionRepository,
        RegionValidator regionValidator,
        IUnitOfWork unitOfWork)
    {
        _regionRepository = regionRepository;
        _regionValidator = regionValidator;
        _unitOfWork = unitOfWork;
    }


    public async Task<Result<UpdateResponse>> UpdateAsync(
        int regionId,
        RegionUpdateRequest request)
    {
        var region = await _regionValidator.GetByIdAsync(regionId);
        if (!region.IsSuccess)
            return Result<UpdateResponse>.Fail(region.Error);

        if (request.Name is not null)
        {
            var nameValidation = await _regionValidator
                .NotExistsByNameAsync(request.Name);

            if (!nameValidation.IsSuccess)
                return Result<UpdateResponse>.Fail(nameValidation.Error);
        }

        var updatedCount = RegionUpdater
            .ApplyUpdates(region.Value!, request);

        var result = new UpdateResponse(
            updatedCount,
            DateTime.UtcNow
        );

        if (updatedCount == 0)
            return Result<UpdateResponse>.Success(result);

        await _unitOfWork.BeginTransactionAsync();

        try
        {
            _regionRepository.Update(region.Value!);

            await _unitOfWork.CommitAsync();
        }
        catch
        {
            await _unitOfWork.RollbackAsync();
            throw;
        }

        return Result<UpdateResponse>.Success(result);
    }
}