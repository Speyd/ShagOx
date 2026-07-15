using ShagOxServer.Application.DTOs.Location.Regions.Create;
using ShagOxServer.Application.Interfaces.Persistences;
using ShagOxServer.Application.Interfaces.Repositories.Location.Regions;
using ShagOxServer.Application.Interfaces.Services.Location.Regions.Create;
using ShagOxServer.Application.Services.Location.Regions.Validator;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Services.Location.Regions.Create;
public class RegionCreateService : IRegionCreateService
{
    private readonly IRegionRepository _regionRepository;
    private readonly RegionValidator _regionValidator;

    private readonly IUnitOfWork _unitOfWork;


    public RegionCreateService(
        IRegionRepository regionRepository,
        RegionValidator regionValidator,
        IUnitOfWork unitOfWork)
    {
        _regionRepository = regionRepository;
        _regionValidator = regionValidator;
        _unitOfWork = unitOfWork;
    }


    public async Task<Result<RegionCreateResponse>> CreateAsync(
        RegionCreateRequest request)
    {
        var validation = await _regionValidator.NotExistsByNameAsync(request.Name);
        if (!validation.IsSuccess)
            return Result<RegionCreateResponse>.Fail(validation.Error ?? "");

        var region = RegionCreater.CreateRegion(request);

        await _unitOfWork.BeginTransactionAsync();

        try
        {
            _regionRepository.Add(region);

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
}