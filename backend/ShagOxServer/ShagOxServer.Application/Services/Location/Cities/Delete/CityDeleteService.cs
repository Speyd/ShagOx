using ShagOxServer.Application.DTOs.Location.Cities.Delete;
using ShagOxServer.Application.Interfaces.Persistences;
using ShagOxServer.Application.Interfaces.Repositories.Location.Cities;
using ShagOxServer.Application.Interfaces.Services.Location.Cities.Delete;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Services.Location.Cities.Delete;
public class CityDeleteService : ICityDeleteService
{
    private readonly ICityRepository _repository;

    private readonly IUnitOfWork _unitOfWork;

    public CityDeleteService(
        ICityRepository cityRepository,
        IUnitOfWork unitOfWork)
    {
        _repository = cityRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<CityDeleteResponse>> DeleteCityAsync(
        int id)
    {
        var city = await _repository.GetByIdAsync(id);
        if (city is null)
            return Result<CityDeleteResponse>.NotFound("City");

        await _unitOfWork.BeginTransactionAsync();

        try
        {
            _repository.Add(city);

            await _unitOfWork.CommitAsync();
        }
        catch
        {
            await _unitOfWork.RollbackAsync();
            throw;
        }

        return Result<CityDeleteResponse>.Success(
           new CityDeleteResponse(
               city.Id,
               DateTime.UtcNow
           )
       );
    }
}