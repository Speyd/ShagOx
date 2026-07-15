using ShagOxServer.Application.DTOs.Advertisements.Delete;
using ShagOxServer.Application.Interfaces.Persistences;
using ShagOxServer.Application.Interfaces.Repositories.Advertisements;
using ShagOxServer.Application.Interfaces.Services.Advertisements.Delete;
using ShagOxServer.Application.Interfaces.Services.Specification.Images.Delete;
using ShagOxServer.Application.Services.Advertisements.Validator;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Services.Advertisements.Delete;
public class AdvertisementDeleteService : IAdvertisementDeleteService
{
    private readonly IAdvertisementRepository _advertisementRepository;
    private readonly AdvertisementValidator _advertisementValidator;

    private readonly IImageDeleteService _imageDeleteService;

    private readonly IUnitOfWork _unitOfWork;


    public AdvertisementDeleteService(
        IAdvertisementRepository advertisementRepository,
        AdvertisementValidator advertisementValidator,
        IImageDeleteService imageDeleteService,
        IUnitOfWork unitOfWork)
    {
        _advertisementRepository = advertisementRepository;
        _advertisementValidator = advertisementValidator;
        _imageDeleteService = imageDeleteService;
        _unitOfWork = unitOfWork;
    }


    public async Task<Result<AdvertisementDeleteResponse>> DeleteAsync(int id)
    {
        var advert = await _advertisementValidator.GetByIdAsync(id);
        if (!advert.IsSuccess)
            return Result<AdvertisementDeleteResponse>.Fail(advert.Error ?? "");

        await _unitOfWork.BeginTransactionAsync();
        try
        {
            _advertisementRepository.Delete(advert.Value!);

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

        return Result<AdvertisementDeleteResponse>.Success(
           new AdvertisementDeleteResponse(
               advert.Value!.Id,
               DateTime.UtcNow
           )
       );
    }
}