using Microsoft.Extensions.Logging;
using ShagOxServer.Application.DTOs.Base.Responses;
using ShagOxServer.Application.Interfaces.Persistences;
using ShagOxServer.Application.Interfaces.Repositories.Base;
using ShagOxServer.Application.Interfaces.Services.Location.Regions.Delete;
using ShagOxServer.Application.Resources.EntityErrors;
using ShagOxServer.Application.Services.Location.Regions.Validator;
using ShagOxServer.Domain.Entities.Location;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Services.Location.Regions.Delete;
public class RegionDeleteService 
    : IRegionDeleteService
{
    private readonly IRepository<Region> _regionRepository;
    private readonly RegionValidator _regionValidator;

    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<RegionDeleteService> _logger;


    public RegionDeleteService(
        IRepository<Region> regionRepository,
        RegionValidator regionValidator,
        IUnitOfWork unitOfWork,
        ILogger<RegionDeleteService> logger)
    {
        _regionRepository = regionRepository;
        _regionValidator = regionValidator;
        _unitOfWork = unitOfWork;
        _logger = logger;
    }


    public async Task<Result<DeleteResponse>> DeleteAsync(
        int id)
    {
        var region = await _regionValidator.GetByIdAsync(id);
        if (!region.IsSuccess)
            return Result<DeleteResponse>.Fail(region.Error);

        await _unitOfWork.BeginTransactionAsync();

        try
        {
            _regionRepository.Delete(region.Value!);

            await _unitOfWork.CommitAsync();
        }
        catch(Exception ex)
        {
            await _unitOfWork.RollbackAsync();

            _logger.LogError(
                ex,
                "Failed to delete region. Id: {Id}",
                id);

            return Result<DeleteResponse>
                 .Fail(EntityError.RegionDeleteFailed);
        }

        return Result<DeleteResponse>.Success(
           new DeleteResponse(
               region.Value!.Id,
               DateTime.UtcNow
           )
       );
    }
}