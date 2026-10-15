using Microsoft.Extensions.Logging;
using ShagOxServer.Application.DTOs.Specification.Pictures.Core.Create.Request;
using ShagOxServer.Application.DTOs.Specification.Pictures.Core.Create.Response;
using ShagOxServer.Application.DTOs.Specification.Pictures.Create;
using ShagOxServer.Application.Interfaces.Services.Specification.Pictures.Images.Create;
using ShagOxServer.Application.Resources.EntityErrors;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Services.Specification.Pictures.Images.Create;
public partial class AdvertisementImageCreateService
    : IAdvertisementImageCreateService
{
    public async Task<Result<PicturesCreateResponse>> CreateAsync(
        PicturesCreateRequest request)
    {
        var uploadedImages = new List<string>();

        int order = 0;

        await _unitOfWork.BeginTransactionAsync();
        try
        {
            foreach (var file in request.Files)
            {
                var result = await CreateInternalAsync(
                    new PictureOrderCreateRequest(
                        file,
                        request.OwnerId,
                        order++));


                if (!result.IsSuccess)
                    throw new Exception("Fail image create");

                uploadedImages.Add(result.Value!.PublicId);
            }

            await _unitOfWork.CommitAsync();
        }
        catch (Exception ex)
        {
            await _unitOfWork.RollbackAsync();

            _logger.LogError(
               ex,
               "Failed to create images(Count:{Count}). " +
               "AdvertisementId: {AdvertisementId}",
               request.Files.Count,
               request.OwnerId);

            foreach (var image in uploadedImages)
                await _loaderService.DeleteAsync(image);

            return Result<PicturesCreateResponse>
                .Fail(EntityErrorResources.ImageCreateFailed);
        }

        return Result<PicturesCreateResponse>.Success(
            new PicturesCreateResponse(
                uploadedImages.Count,
                DateTime.UtcNow
        ));
    }

    public async Task<Result<PictureCreateResponse>> CreateInternalAsync(
        PictureOrderCreateRequest request)
    {
        var resultLoaderValid = await _pictureValidator
            .PictureUploadValidator(request.File);

        try
        {
            if (!resultLoaderValid.IsSuccess)
                throw new Exception("Fail image upload");

            var image = AdvertisementImageCreater.Create(
                request,
                resultLoaderValid.Value!
            );

            _imageRepository.Add(image);

            await _unitOfWork.SaveChangesAsync();

            return await Success(image);
        }
        catch (Exception ex)
        {
            _logger.LogError(
               ex,
               "Failed to create image. " +
               "AdvertisementId: {AdvertisementId}",
               request.OwnerId);

            if (resultLoaderValid.Value?.PublicId is not null)
            {
                await _loaderService
                    .DeleteAsync(resultLoaderValid.Value.PublicId);

                return Result<PictureCreateResponse>
                    .Fail(resultLoaderValid.Error);
            }

            return Result<PictureCreateResponse>
                .Fail(EntityErrorResources.ImageCreateFailed);
        }
    }
}
