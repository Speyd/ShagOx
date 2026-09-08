using ShagOxServer.Application.DTOs.Base.Responses;
using ShagOxServer.Application.Interfaces.Persistences;
using ShagOxServer.Application.Interfaces.Repositories.Base;
using ShagOxServer.Application.Interfaces.Services.Advertisements.Core.Delete;
using ShagOxServer.Application.Interfaces.Services.Specification.Pictures.Images.Delete;
using ShagOxServer.Application.Services.Advertisements.Core.Validator;
using ShagOxServer.Domain.Entities.Advertisements;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Services.Advertisements.Core.Delete;
public class AdvertisementDeleteService 
    : IAdvertisementDeleteService
{
    private readonly IRepository<Advertisement> _advertRepository;
    private readonly AdvertisementValidator _advertValidator;

    private readonly IImageDeleteService _imageDeleteService;

    private readonly IUnitOfWork _unitOfWork;


    public AdvertisementDeleteService(
        IRepository<Advertisement> advertRepository,
        AdvertisementValidator advertValidator,
        IImageDeleteService imageDeleteService,
        IUnitOfWork unitOfWork)
    {
        _advertRepository = advertRepository;
        _advertValidator = advertValidator;
        _imageDeleteService = imageDeleteService;
        _unitOfWork = unitOfWork;
    }


    public async Task<Result<DeleteResponse>> DeleteAsync(
        int id)
    {
        var advert = await _advertValidator
            .GetByIdAsync(id);

        if (!advert.IsSuccess)
            return Result<DeleteResponse>.Fail(advert.Error);

        await _unitOfWork.BeginTransactionAsync();

        try
        {
            _advertRepository.Delete(advert.Value!);

            foreach (var image in advert.Value!.Images)
            {
                await _imageDeleteService.DeleteAsync(image.Id);
            }

            await _unitOfWork.CommitAsync();
        }
        catch
        {
            await _unitOfWork.RollbackAsync();
            throw;
        }

        return Result<DeleteResponse>.Success(
           new DeleteResponse(
               advert.Value!.Id,
               DateTime.UtcNow
           )
       );
    }
}