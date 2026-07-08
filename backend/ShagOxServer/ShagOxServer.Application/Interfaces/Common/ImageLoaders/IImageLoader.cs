using Microsoft.AspNetCore.Http;

namespace ShagOxServer.Application.Interfaces.Common.ImageLoaders;
public interface IImageLoader
{
    Task<(string Url, string PublicId)> UploadAsync(IFormFile file);

    Task DeleteAsync(string publicId);
}
