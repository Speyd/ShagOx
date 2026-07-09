using Microsoft.AspNetCore.Http;
using ShagOxServer.Application.DTOs.Common.ImageLoaders.Delete;
using ShagOxServer.Application.DTOs.Common.ImageLoaders.Upload;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Interfaces.Services.Common.ImageLoaders;
public interface IImageLoaderService
{
    Task<Result<ImageLoaderUploadResponse>> UploadAsync(
        IFormFile file);

    Task<Result<ImageLoaderDeleteResponse>> DeleteAsync(
        string publicId);
}
