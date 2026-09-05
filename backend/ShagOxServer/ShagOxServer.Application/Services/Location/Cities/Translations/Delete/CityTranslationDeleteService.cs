using ShagOxServer.Application.DTOs.Common.Responses;
using ShagOxServer.Application.Interfaces.Persistences;
using ShagOxServer.Application.Interfaces.Repositories.Base;
using ShagOxServer.Application.Interfaces.Services.Location.Cities.Translations.Delete;
using ShagOxServer.Application.Services.Location.Cities.Translations.Validator;
using ShagOxServer.Domain.Entities.Location.Translations;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Services.Location.Cities.Translations.Delete;
public class CityTranslationDeleteService
    : ICityTranslationDeleteService
{
    private readonly IRepository<CityTranslation> _cityRepository;
    private readonly CityTranslationValidator _cityValidator;

    private readonly IUnitOfWork _unitOfWork;


    public CityTranslationDeleteService(
        IRepository<CityTranslation> cityRepository,
        CityTranslationValidator cityValidator,
        IUnitOfWork unitOfWork)
    {
        _cityRepository = cityRepository;
        _cityValidator = cityValidator;
        _unitOfWork = unitOfWork;
    }


    public async Task<Result<DeleteResponse>> DeleteAsync(
        int id)
    {
        var status = await _cityValidator
            .GetByIdAsync(id);

        if (!status.IsSuccess)
            return Result<DeleteResponse>.Fail(status.Error);

        await _unitOfWork.BeginTransactionAsync();

        try
        {
            _cityRepository.Delete(status.Value!);

            await _unitOfWork.CommitAsync();
        }
        catch
        {
            await _unitOfWork.RollbackAsync();
            throw;
        }

        return Result<DeleteResponse>.Success(
           new DeleteResponse(
               status.Value!.Id,
               DateTime.UtcNow
           )
       );
    }
}