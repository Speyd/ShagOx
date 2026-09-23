using Microsoft.Extensions.Logging;
using ShagOxServer.Application.DTOs.Base.Responses;
using ShagOxServer.Application.DTOs.Location.Regions.Create;
using ShagOxServer.Application.Interfaces.Persistences;
using ShagOxServer.Application.Interfaces.Repositories.Base;
using ShagOxServer.Application.Interfaces.Services.Location.Regions.Create;
using ShagOxServer.Application.Services.Location.Regions.Validator;
using ShagOxServer.Domain.Entities.Location;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Services.Location.Regions.Create;
public class RegionCreateService 
    : IRegionCreateService
{
    private readonly IRepository<Region> _regionRepository;
    private readonly RegionValidator _regionValidator;

    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<RegionCreateService> _logger;


    public RegionCreateService(
        IRepository<Region> regionRepository,
        RegionValidator regionValidator,
        IUnitOfWork unitOfWork,
        ILogger<RegionCreateService> logger)
    {
        _regionRepository = regionRepository;
        _regionValidator = regionValidator;
        _unitOfWork = unitOfWork;
        _logger = logger;
    }


    public async Task<Result<CreateResponse>> CreateAsync(
        RegionCreateRequest request)
    {
        var validation = await _regionValidator
            .NotExistsByCodeAsync(request.Code);

        if (!validation.IsSuccess)
            return Result<CreateResponse>.Fail(validation.Error);

        var region = RegionCreater.Create(request);

        await _unitOfWork.BeginTransactionAsync();

        try
        {
            _regionRepository.Add(region);

            await _unitOfWork.CommitAsync();
        }
        catch(Exception ex)
        {
            await _unitOfWork.RollbackAsync();

            _logger.LogError(
               ex,
               "Failed to create region. Code: {Code}",
               request.Code);

            return Result<CreateResponse>
                 .Fail("Failed to create region.");
        }

        return Result<CreateResponse>.Success(
            new CreateResponse(
                region.Id,
                DateTime.UtcNow
        ));
    }
}