using ShagOxServer.Application.DTOs.Location.Cities.Delete;
using ShagOxServer.Application.Interfaces.Persistences;
using ShagOxServer.Application.Interfaces.Repositories.Location.Cities;
using ShagOxServer.Application.Interfaces.Services.Location.Cities.Delete;
using ShagOxServer.Application.Services.Location.Cities.Validator;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Services.Location.Cities.Delete;
public class CityDeleteService : ICityDeleteService
{
    private readonly ICityRepository _cityRepository;
    private readonly CityValidator _cityValidator;

    private readonly IUnitOfWork _unitOfWork;


    public CityDeleteService(
        ICityRepository cityRepository,
        CityValidator cityValidator,
        IUnitOfWork unitOfWork)
    {
        _cityRepository = cityRepository;
        _cityValidator = cityValidator;
        _unitOfWork = unitOfWork;
    }


    public async Task<Result<CityDeleteResponse>> DeleteAsync(
        int id)
    {
        var city = await _cityValidator.GetByIdAsync(id);
        if (!city.IsSuccess)
            return Result<CityDeleteResponse>.Fail(city.Error ?? "");

        await _unitOfWork.BeginTransactionAsync();

        try
        {
            _cityRepository.Add(city.Value!);

            await _unitOfWork.CommitAsync();
        }
        catch
        {
            await _unitOfWork.RollbackAsync();
            throw;
        }

        return Result<CityDeleteResponse>.Success(
           new CityDeleteResponse(
               city.Value!.Id,
               DateTime.UtcNow
           )
       );
    }
}