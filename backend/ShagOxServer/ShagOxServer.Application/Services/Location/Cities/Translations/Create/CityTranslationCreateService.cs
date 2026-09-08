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


    public CityTranslationCreateService(
        IRepository<CityTranslation> cityRepository,
        CityTranslationValidator cityValidator,
        IUnitOfWork unitOfWork)
    {
        _cityRepository = cityRepository;
        _cityValidator = cityValidator;

        _unitOfWork = unitOfWork;
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
        catch
        {
            await _unitOfWork.RollbackAsync();
            throw;
        }

        return Result<CreateResponse>.Success(
            new CreateResponse(
                city.Id,
                DateTime.UtcNow
        ));
    }
}