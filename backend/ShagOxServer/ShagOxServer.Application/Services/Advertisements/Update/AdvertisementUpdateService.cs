using ShagOxServer.Application.DTOs.Advertisements.Update;
using ShagOxServer.Application.Interfaces.Persistences;
using ShagOxServer.Application.Interfaces.Repositories.Advertisements;
using ShagOxServer.Application.Interfaces.Services.Advertisements.Images;
using ShagOxServer.Application.Interfaces.Services.Advertisements.Update;
using ShagOxServer.Application.Services.Advertisements.Update.Validator;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Services.Advertisements.Update;

public class AdvertisementUpdateService : IAdvertisementUpdateService
{
    private readonly IAdvertisementRepository _advertisementRepository;
    private readonly IAdvertisementImageService _imageService;
    private readonly AdvertisementUpdateValidator _validator;

    private readonly IUnitOfWork _unitOfWork;


    public AdvertisementUpdateService(
        IAdvertisementRepository advertisementRepository,
        IAdvertisementImageService imageService,
        AdvertisementUpdateValidator validator,
        IUnitOfWork unitOfWork)
    {
        _advertisementRepository = advertisementRepository;
        _validator = validator;
        _imageService = imageService;
        _unitOfWork = unitOfWork;
    }


    public async Task<Result<AdvertisementUpdateResponse>> UpdateAdvertisementAsync(
        int advertId,
        AdvertisementUpdateRequest request)
    {
        var advert = await _advertisementRepository.GetByIdAsync(advertId);

        if (advert is null)
            return Result<AdvertisementUpdateResponse>
                .NotFound("Advertisement");


        var validation = await _validator.ValidateAsync(request);

        if (!validation.IsSuccess)
            return Result<AdvertisementUpdateResponse>
                .Fail(validation.Error!);



        await _unitOfWork.BeginTransactionAsync();

        try
        {
            var updatedCount =
                AdvertisementUpdater.UpdateFields(advert, request);


            var imagesResult =
                await _imageService.SyncImagesAsync(
                    advertId,
                    request);


            if (!imagesResult.IsSuccess)
            {
                await _unitOfWork.RollbackAsync();

                return Result<AdvertisementUpdateResponse>
                    .Fail(imagesResult.Error!);
            }


            await _unitOfWork.CommitAsync();


            return Result<AdvertisementUpdateResponse>.Success(
                new AdvertisementUpdateResponse(
                    DateTime.UtcNow,
                    updatedCount));
        }
        catch
        {
            await _unitOfWork.RollbackAsync();
            throw;
        }
    }
}