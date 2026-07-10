using Microsoft.AspNetCore.Http;
using ShagOxServer.Application.DTOs.Advertisements.Create;
using ShagOxServer.Application.DTOs.Specification.Images.Create;
using ShagOxServer.Application.Interfaces.Persistences;
using ShagOxServer.Application.Interfaces.Repositories.Advertisements;
using ShagOxServer.Application.Interfaces.Services.Advertisements.Create;
using ShagOxServer.Application.Interfaces.Services.Common.ImageLoaders;
using ShagOxServer.Application.Interfaces.Services.Roles.Specification.Images.Create;
using ShagOxServer.Application.Services.Advertisements.Create.Validator;
using ShagOxServer.Domain.Entities.Advertisements;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Services.Advertisements.Create;
public class AdvertisementCreateService : IAdvertisementCreateService
{
    private readonly IAdvertisementRepository _advertisementRepository;
    private readonly AdvertisementCreateValidator _validator;
 
    private readonly IImageCreateService _imageService;
    private readonly IImageLoaderService _loaderService;

    private readonly IUnitOfWork _unitOfWork;


    public AdvertisementCreateService(
        IAdvertisementRepository advertisementRepository,
        AdvertisementCreateValidator validator,
        IImageCreateService imageService,
        IImageLoaderService loaderService,
        IUnitOfWork unitOfWork)
    {
        _advertisementRepository = advertisementRepository;
        _validator = validator;
       
        _imageService = imageService;
        _loaderService = loaderService;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<AdvertisementCreateResponse>> CreateAdvertisementAsync(
        AdvertisementCreateRequest request)
    {
        var validation = await _validator.ValidateAsync(request);
        if (!validation.IsSuccess)
            return Result<AdvertisementCreateResponse>.Fail(validation.Error!);

        var advert = CreateAdvertisement(request);

        await _unitOfWork.BeginTransactionAsync();
        try
        {
            _advertisementRepository.Add(advert);

            await _unitOfWork.SaveChangesAsync();


            var imagesResult = await CreateImagesAsync(
                advert.Id,
                request.Images);


            if (!imagesResult.IsSuccess)
            {
                await _unitOfWork.RollbackAsync();

                return Result<AdvertisementCreateResponse>
                    .Fail(imagesResult.Error!);
            }


            await _unitOfWork.CommitAsync();
        }
        catch
        {
            await _unitOfWork.RollbackAsync();
            throw;
        }

        var response = new AdvertisementCreateResponse(
            advert.Id,
            advert.CreatedAt
        );

        return Result<AdvertisementCreateResponse>.Success(response);
    }

    private Advertisement CreateAdvertisement(
        AdvertisementCreateRequest request)
    {
        return new Advertisement
        {
            Title = request.Title,
            Description = request.Description ?? "",
            Popularity = request.Popularity,

            CurrencyId = request.CurrencyId,
            CategoryId = request.CategoryId,
            ConditionId = request.ConditionId,
            SellerId = request.SellerId,

            Properties = request.Properties ?? new()
        };
    }

    private async Task<Result<bool>> CreateImagesAsync(
        int advertisementId,
        List<IFormFile> images)
    {
        var uploadedImages = new List<string>();

        foreach (var image in images)
        {
            var result = await _imageService.CreateFromFileInternalAsync(
                new ImageFileCreateRequest(
                    image,
                    advertisementId));


            if (!result.IsSuccess)
            {
                foreach (var publicId in uploadedImages)
                {
                    await _loaderService.DeleteAsync(publicId);
                }

                return Result<bool>
                    .Fail(result.Error!);
            }


            uploadedImages.Add(result.Value!.PublicId);
        }

        return Result<bool>.Success(true);
    }
}