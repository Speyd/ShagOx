using Microsoft.AspNetCore.Http;
using ShagOxServer.Application.Interfaces.Common.ImageLoaders;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShagOxServer.Application.Services.Common.ImageLoaders;

public class CloudinaryService : IImageLoader
{
    public Task DeleteAsync(string publicId)
    {
        throw new NotImplementedException();
    }

    public Task<(string Url, string PublicId)> UploadAsync(IFormFile file)
    {
        throw new NotImplementedException();
    }
}
