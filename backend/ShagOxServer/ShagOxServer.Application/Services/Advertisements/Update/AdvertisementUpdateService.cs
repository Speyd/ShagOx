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
    private readonly IAdvertisementQueryRepository _advertisementQueryRepository;
    private readonly IAdvertisementImageService _imageService;
    private readonly AdvertisementUpdateValidator _validator;

    private readonly IUnitOfWork _unitOfWork;


    public AdvertisementUpdateService(
        IAdvertisementQueryRepository advertisementQueryRepository,
        IAdvertisementImageService imageService,
        AdvertisementUpdateValidator validator,
        IUnitOfWork unitOfWork)
    {
        _advertisementQueryRepository = advertisementQueryRepository;
        _validator = validator;
        _imageService = imageService;
        _unitOfWork = unitOfWork;
    }


    public async Task<Result<AdvertisementUpdateResponse>> UpdateAsync(
        int advertId,
        AdvertisementUpdateRequest request)
    {
        var advert = await _advertisementQueryRepository.GetByIdAsync(advertId);

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
                    advert,
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