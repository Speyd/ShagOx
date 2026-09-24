using Microsoft.Extensions.Logging;
using ShagOxServer.Application.DTOs.Base.Responses;
using ShagOxServer.Application.DTOs.Location.Cities.Create;
using ShagOxServer.Application.Interfaces.Persistences;
using ShagOxServer.Application.Interfaces.Repositories.Base;
using ShagOxServer.Application.Interfaces.Services.Location.Cities.Create;
using ShagOxServer.Application.Resources.EntityErrorResourcess;
using ShagOxServer.Application.Services.Location.Cities.Validator;
using ShagOxServer.Application.Services.Location.Regions.Validator;
using ShagOxServer.Domain.Entities.Location;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Services.Location.Cities.Create;
public class CityCreateService
    : ICityCreateService
{
    private readonly IRepository<City> _cityRepository;
    private readonly CityValidator _cityValidator;

    private readonly RegionValidator _regionValidator;

    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<CityCreateService> _logger;


    public CityCreateService(
        IRepository<City> cityRepository,
        CityValidator cityValidator,
        RegionValidator regionValidator,
        IUnitOfWork unitOfWork,
        ILogger<CityCreateService> logger)
    {
        _cityRepository = cityRepository;
        _cityValidator = cityValidator;
        _regionValidator = regionValidator;
        _unitOfWork = unitOfWork;
        _logger = logger;
    }


    public async Task<Result<CreateResponse>> CreateAsync(
        CityCreateRequest request)
    {
        var region = await _regionValidator
            .ExistsByIdAsync(request.RegionId);

        if (!region.IsSuccess)
            Result<CreateResponse>.Fail(region.Error);


        var validatorName = await _cityValidator
            .NotExistsAsync(request.RegionId, request.Code);

        if (!validatorName.IsSuccess)
            Result<CreateResponse>.Fail(validatorName.Error);


        var city = CityCreater.Create(request);

        await _unitOfWork.BeginTransactionAsync();

        try
        {
            _cityRepository.Add(city);

            await _unitOfWork.CommitAsync();
        }
        catch(Exception ex)
        {
            await _unitOfWork.RollbackAsync();

            _logger.LogError(
                ex,
                "Failed to create city. " +
                "RegionId: {RegionId}, Code: {Code}",
                request.RegionId,
                request.Code);

            return Result<CreateResponse>
                 .Fail(EntityErrorResources.CityCreateFailed);
        }

        return Result<CreateResponse>.Success(
            new CreateResponse(
                city.Id,
                DateTime.UtcNow
        ));
    }
}