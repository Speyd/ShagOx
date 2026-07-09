using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;
using ShagOxServer.Application.DTOs.Common.ImageLoaders.Delete;
using ShagOxServer.Application.DTOs.Common.ImageLoaders.Upload;
using ShagOxServer.Application.Interfaces.Services.Common.ImageLoaders;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Infrastructure.ExternalServices.ImageStorage;
public class CloudinaryImageLoader : IImageLoaderService
{
    private readonly Cloudinary _cloudinary;

    public CloudinaryImageLoader(
        Cloudinary cloudinary)
    {
        _cloudinary = cloudinary;
    }


    public async Task<Result<ImageLoaderDeleteResponse>> DeleteAsync(
        string publicId)
    {
        if (string.IsNullOrWhiteSpace(publicId))
            return Result<ImageLoaderDeleteResponse>.Fail("PublicId is incorrect");

        var deleteParams = new DeletionParams(publicId);
        await _cloudinary.DestroyAsync(deleteParams);

        return Result<ImageLoaderDeleteResponse>.Success(
           new ImageLoaderDeleteResponse(
                publicId,
                DateTime.UtcNow
               )
           );
    }

    public async Task<Result<ImageLoaderUploadResponse>> UploadAsync(
        IFormFile file)
    {
        await using var stream = file.OpenReadStream();

        var uploadParams = new ImageUploadParams
        {
            File = new FileDescription(
                file.FileName,
                stream)
        };

        var result = await _cloudinary.UploadAsync(uploadParams);

        return Result<ImageLoaderUploadResponse>.Success(
          new ImageLoaderUploadResponse(
               result.PublicId,
               result.SecureUrl.ToString()
              )
          );
    }
}
