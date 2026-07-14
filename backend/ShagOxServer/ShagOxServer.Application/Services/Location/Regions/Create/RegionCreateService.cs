using ShagOxServer.Application.DTOs.Location.Regions.Create;
using ShagOxServer.Application.Interfaces.Persistences;
using ShagOxServer.Application.Interfaces.Repositories.Location.Regions;
using ShagOxServer.Application.Interfaces.Services.Location.Regions.Create;
using ShagOxServer.Application.Services.Location.Regions.Validator;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Services.Location.Regions.Create;
public class RegionCreateService : IRegionCreateService
{
    private readonly IRegionRepository _repository;
    private readonly RegionValidator _validator;

    private readonly IUnitOfWork _unitOfWork;


    public RegionCreateService(
        IRegionRepository regionRepository,
        RegionValidator validator,
        IUnitOfWork unitOfWork)
    {
        _repository = regionRepository;
        _validator = validator;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<RegionCreateResponse>> CreateRegionAsync(
        RegionCreateRequest request)
    {
        var validation = await _validator.NotExistsByNameAsync(request.Name);
        if (!validation.IsSuccess)
            return Result<RegionCreateResponse>.Fail(validation.Error ?? "");

        var region = RegionCreater.CreateRegion(request);

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
}