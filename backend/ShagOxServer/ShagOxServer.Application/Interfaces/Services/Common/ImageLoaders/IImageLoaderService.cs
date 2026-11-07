using Microsoft.AspNetCore.Http;
using ShagOxServer.Application.DTOs.Common.ImageLoaders.Delete;
using ShagOxServer.Application.DTOs.Common.ImageLoaders.Upload;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Interfaces.Services.Common.ImageLoaders;
public interface IPictureLoaderService
{
    Task<Result<PictureLoaderUploadResponse>> UploadAsync(
        IFormFile file,
        CancellationToken cancellationToken = default);

    Task<Result<PictureLoaderDeleteResponse>> DeleteAsync(
        string publicId);
}
