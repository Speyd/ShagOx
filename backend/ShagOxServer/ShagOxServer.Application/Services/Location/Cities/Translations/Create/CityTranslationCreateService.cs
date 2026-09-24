using Microsoft.Extensions.Logging;
using ShagOxServer.Application.DTOs.Base.Responses;
using ShagOxServer.Application.DTOs.Location.Cities.Translations.Create;
using ShagOxServer.Application.Interfaces.Persistences;
using ShagOxServer.Application.Interfaces.Repositories.Base;
using ShagOxServer.Application.Interfaces.Services.Location.Cities.Translations.Create;
using ShagOxServer.Application.Services.Location.Cities.Translations.Validator;
using ShagOxServer.Domain.Entities.Location.Translations;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Services.Location.Cities.Translations.Create;
public class CityTranslationCreateService
    : ICityTranslationCreateService
{
    private readonly IRepository<CityTranslation> _cityRepository;
    private readonly CityTranslationValidator _cityValidator;

    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<CityTranslationCreateService> _logger;


    public CityTranslationCreateService(
        IRepository<CityTranslation> cityRepository,
        CityTranslationValidator cityValidator,
        IUnitOfWork unitOfWork,
        ILogger<CityTranslationCreateService> logger)
    {
        _cityRepository = cityRepository;
        _cityValidator = cityValidator;

        _unitOfWork = unitOfWork;
        _logger = logger;
    }


    public async Task<Result<CreateResponse>> CreateAsync(
        CityTranslationCreateRequest request)
    {
        var codeValidation = await _cityValidator
            .NotExistsAsync(request.TranslatableId, request.Language);

        if (!codeValidation.IsSuccess)
            return Result<CreateResponse>.Fail(codeValidation.Error);


        var city = CityTranslationCreater.Create(request);

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
               "Failed to create city translation. " +
               "TranslatableId: {TranslatableId}",
               request.TranslatableId);

            return Result<CreateResponse>
                 .Fail("Failed to create city translation.");
        }

        return Result<CreateResponse>.Success(
            new CreateResponse(
                city.Id,
                DateTime.UtcNow
        ));
    }
}