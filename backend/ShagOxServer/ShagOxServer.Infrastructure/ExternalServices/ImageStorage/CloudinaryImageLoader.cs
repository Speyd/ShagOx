using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;
using ShagOxServer.Application.DTOs.Common.ImageLoaders.Delete;
using ShagOxServer.Application.DTOs.Common.ImageLoaders.Upload;
using ShagOxServer.Application.Interfaces.Services.Common.ImageLoaders;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Infrastructure.ExternalServices.ImageStorage;
public class CloudinaryImageLoader : IPictureLoaderService
{
    private readonly Cloudinary _cloudinary;

    public CloudinaryImageLoader(
        Cloudinary cloudinary)
    {
        _cloudinary = cloudinary;
    }


    public async Task<Result<PictureLoaderDeleteResponse>> DeleteAsync(
        string publicId)
    {
        if (string.IsNullOrWhiteSpace(publicId))
            return Result<PictureLoaderDeleteResponse>
                .Fail("PublicId is incorrect");

        var deleteParams = new DeletionParams(publicId);
        Console.WriteLine($"\n\n\n{publicId}\n\n\n");
        var result = await _cloudinary.DestroyAsync(deleteParams);

        if (result.Error != null)
            return Result<PictureLoaderDeleteResponse>
                .Fail($"Cloudinary delete failed: {result.Error.Message}");

        return Result<PictureLoaderDeleteResponse>.Success(
           new PictureLoaderDeleteResponse(
                publicId,
                DateTime.UtcNow
               )
           );
    }

    public async Task<Result<PictureLoaderUploadResponse>> UploadAsync(
        IFormFile file)
    {
        await using var stream = file.OpenReadStream();

        var uploadParams = new ImageUploadParams
        {
            File = new FileDescription(
                file.FileName,
                stream),
            Folder = "ShagOx"
        };

        var result = await _cloudinary.UploadAsync(uploadParams);

        if (result.Error != null)
        {
            return Result<PictureLoaderUploadResponse>
                .Fail($"Cloudinary upload failed: {result.Error.Message}");
        }

        return Result<PictureLoaderUploadResponse>.Success(
          new PictureLoaderUploadResponse(
               result.PublicId,
               result.SecureUrl.ToString()
              )
          );
    }
}
