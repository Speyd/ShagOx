using ShagOxServer.Application.DTOs.Advertisements.Update;
using ShagOxServer.Application.Interfaces.Persistences;
using ShagOxServer.Application.Interfaces.Services.Advertisements.Images;
using ShagOxServer.Application.Interfaces.Services.Advertisements.Update;
using ShagOxServer.Application.Services.Advertisements.Update.Validator;
using ShagOxServer.SharedKernel.Abstractions.Results;
using ShagOxServer.Application.DTOs.Common.Responses;
using ShagOxServer.Application.Services.Advertisements.Validator;

namespace ShagOxServer.Application.Services.Advertisements.Update;
public class AdvertisementUpdateService 
    : IAdvertisementUpdateService
{
    private readonly IAdvertisementImageService _imageService;
    private readonly AdvertisementValidator _validator;
    private readonly AdvertisementUpdateValidator _validatorUpdate;

    private readonly IUnitOfWork _unitOfWork;


    public AdvertisementUpdateService(
        IAdvertisementImageService imageService,
        AdvertisementValidator validator,
        AdvertisementUpdateValidator validatorUpdate,
        IUnitOfWork unitOfWork)
    {
        _validator = validator;
        _validatorUpdate = validatorUpdate;
        _imageService = imageService;
        _unitOfWork = unitOfWork;
    }


    public async Task<Result<UpdateResponse>> UpdateAsync(
        int advertId,
        AdvertisementUpdateRequest request)
    {
        var advert = await _validator.GetByIdAsync(advertId);
        if (!advert.IsSuccess)
            return Result<UpdateResponse>.Fail(advert.Error);

        var validation = await _validatorUpdate.ValidateAsync(request);
        if (!validation.IsSuccess)
            return Result<UpdateResponse>.Fail(validation.Error!);

        await _unitOfWork.BeginTransactionAsync();

        try
        {
            var updatedCount = AdvertisementUpdater
                .UpdateFields(advert.Value!, request);


            var imagesResult = await _imageService
                .SyncImagesAsync(
                    advert.Value!,
                    request
                );


            if (!imagesResult.IsSuccess)
            {
                await _unitOfWork.RollbackAsync();

                return Result<UpdateResponse>
                    .Fail(imagesResult.Error!);
            }


            await _unitOfWork.CommitAsync();


            return Result<UpdateResponse>.Success(
                new UpdateResponse(
                    updatedCount,
                    DateTime.UtcNow
                ));
        }
        catch
        {
            await _unitOfWork.RollbackAsync();
            throw;
        }
    }
}