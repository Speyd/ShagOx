using ShagOxServer.Application.DTOs.Common.Responses;
using ShagOxServer.Application.Interfaces.Persistences;
using ShagOxServer.Application.Interfaces.Repositories.Base;
using ShagOxServer.Application.Interfaces.Services.Location.Cities.Delete;
using ShagOxServer.Application.Services.Location.Cities.Validator;
using ShagOxServer.Domain.Entities.Location;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Services.Location.Cities.Delete;
public class CityDeleteService 
    : ICityDeleteService
{
    private readonly IRepository<City> _cityRepository;
    private readonly CityValidator _cityValidator;

    private readonly IUnitOfWork _unitOfWork;


    public CityDeleteService(
        IRepository<City> cityRepository,
        CityValidator cityValidator,
        IUnitOfWork unitOfWork)
    {
        _cityRepository = cityRepository;
        _cityValidator = cityValidator;
        _unitOfWork = unitOfWork;
    }


    public async Task<Result<DeleteResponse>> DeleteAsync(
        int id)
    {
        var city = await _cityValidator.GetByIdAsync(id);
        if (!city.IsSuccess)
            return Result<DeleteResponse>.Fail(city.Error);

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

        return Result<DeleteResponse>.Success(
           new DeleteResponse(
               city.Value!.Id,
               DateTime.UtcNow
           )
       );
    }
}