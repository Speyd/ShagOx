using ShagOxServer.Application.DTOs.Base.Responses;
using ShagOxServer.Application.DTOs.Specification.Pictures.Images.Update;
using ShagOxServer.Application.Interfaces.Persistences;
using ShagOxServer.Application.Interfaces.Repositories.Base;
using ShagOxServer.Application.Interfaces.Services.Specification.Pictures.Images.Update;
using ShagOxServer.Application.Services.Advertisements.Core.Validator;
using ShagOxServer.Application.Services.Specification.Pictures.Images.Validator;
using ShagOxServer.Domain.Entities.Specification.Pictures;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Services.Specification.Pictures.Images.Update;
public class ImageUpdateService 
    : IImageUpdateService
{
    private readonly IRepository<Image> _imageRepository;
    private readonly ImageValidator _imageValidator;

    private readonly AdvertisementValidator _advertValidator;

    private readonly IUnitOfWork _unitOfWork;


    public ImageUpdateService(
        IRepository<Image> imageRepository,
        ImageValidator imageValidator,
        AdvertisementValidator advertisementValidator,
        IUnitOfWork unitOfWork)
    {
        _imageRepository = imageRepository;
        _imageValidator = imageValidator;
        _advertValidator = advertisementValidator;
        _unitOfWork = unitOfWork;
    }


    public async Task<Result<UpdateResponse>> UpdateAsync(
        int imageId,
        ImageUpdateRequest request)
    {
        var image = await _imageValidator.GetByIdAsync(imageId);
        if (!image.IsSuccess)
            return Result<UpdateResponse>.Fail(image.Error);

        int? newOrder = request.Order;

        if (request.AdvertisementId is not null &&
            request.Order is not null)
        {
            var advert = await _advertValidator
                .GetByIdAsync(request.AdvertisementId.Value);

            var orderExists = advert.Value!.Images.Any(x =>
                x.Id != imageId &&
                x.Order == request.Order);

            if (orderExists)
                newOrder = ImageUpdater.GetNextOrder(advert.Value);
        }

        var updatedCount = ImageUpdater
            .ApplyUpdates(image.Value!, newOrder, request);

        var response = new UpdateResponse(
            updatedCount,
            DateTime.UtcNow
        );

        if (updatedCount == 0)
            return Result<UpdateResponse>.Success(response);

        try
        {
            _imageRepository.Update(image.Value!);

            await _unitOfWork.CommitAsync();
        }
        catch
        {
            await _unitOfWork.RollbackAsync();

            return Result<UpdateResponse>
                .Fail("Failed to update image.");
        }

        return Result<UpdateResponse>.Success(response);
    }
}