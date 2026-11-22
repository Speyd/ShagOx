using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using ShagOxServer.Application.Common.Options.Pictures;
using ShagOxServer.Application.DTOs.Common.ImageLoaders.Delete;
using ShagOxServer.Application.DTOs.Common.ImageLoaders.Upload;
using ShagOxServer.Application.Interfaces.Services.Common.ImageLoaders;
using ShagOxServer.Application.Resources.Pictures;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Infrastructure.ExternalServices.ImageStorage;
public class CloudinaryImageLoader 
    : IPictureLoaderService
{
    private readonly Cloudinary _cloudinary;
    private readonly PictureLoadeOptions _pictureOptions;


    public CloudinaryImageLoader(
        Cloudinary cloudinary,
        IOptions<PictureLoadeOptions> pictureOptions)
    {
        _cloudinary = cloudinary;
        _pictureOptions = pictureOptions.Value;
    }


    public async Task<Result<PictureLoaderDeleteResponse>> DeleteAsync(
        string publicId)
    {
        if (string.IsNullOrWhiteSpace(publicId))
        {
            return Result<PictureLoaderDeleteResponse>.Fail(
                PicturesResources.PicturePublicIdInvalid);
        }

        var deleteParams = new DeletionParams(publicId);

        var result = await _cloudinary.DestroyAsync(deleteParams);

        if (result.Error != null) 
        {
            return Result<PictureLoaderDeleteResponse>.Fail(
                PicturesResources.PictureCloudinaryDeleteFailed);
        }

        return Result<PictureLoaderDeleteResponse>.Success(
           new PictureLoaderDeleteResponse(
                publicId,
                DateTime.UtcNow
               )
           );
    }

    public async Task<Result<PictureLoaderUploadResponse>> UploadAsync(
        IFormFile file,
        CancellationToken cancellationToken = default)
    {
        await using var stream = file.OpenReadStream();

        await using var webpStream = await PictureConverter
            .Convert(
                stream,
                _pictureOptions.PictureFormat,
                cancellationToken);

        var uploadParams = new ImageUploadParams
        {
            File = new FileDescription(
                $"{Path.GetFileNameWithoutExtension(file.FileName)}.{_pictureOptions.PictureFormat}",
                webpStream),
            Folder = _pictureOptions.PictureFolder,
            Format = _pictureOptions.PictureFormat
        };

        var result = await _cloudinary
            .UploadAsync(uploadParams, cancellationToken);

        if (result.Error != null)
        {
            return Result<PictureLoaderUploadResponse>.Fail(
                PicturesResources.PictureCloudinaryUploadFailed);
        }

        return Result<PictureLoaderUploadResponse>.Success(
          new PictureLoaderUploadResponse(
               result.PublicId,
               result.SecureUrl.ToString()
              )
          );
    }
}
