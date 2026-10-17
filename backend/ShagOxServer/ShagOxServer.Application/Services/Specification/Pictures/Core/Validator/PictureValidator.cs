using Microsoft.AspNetCore.Http;
using ShagOxServer.Application.DTOs.Common.ImageLoaders.Delete;
using ShagOxServer.Application.DTOs.Common.ImageLoaders.Upload;
using ShagOxServer.Application.Interfaces.Services.Common.ImageLoaders;
using ShagOxServer.Application.Resources.EntityErrors;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Services.Specification.Pictures.Core.Validator;
public class PictureValidator
{
    private readonly IPictureLoaderService _loaderService;

    public PictureValidator(
        IPictureLoaderService loaderService)
    {
        _loaderService = loaderService;
    }

    public async Task<Result<PictureLoaderUploadResponse>> PictureUploadValidator(
       IFormFile file)
    {
        var response = await _loaderService
            .UploadAsync(file);

        if (!response.IsSuccess || response.Value is null)
        {
            return Result<PictureLoaderUploadResponse>
                .Fail(EntityErrorResources.ExternalImageUploadFailed);
        }

        return response;
    }

    public async Task<Result<PictureLoaderDeleteResponse>> PictureDeleteValidator(
       string publicId)
    {
        var response = await _loaderService
            .DeleteAsync(publicId);

        if (!response.IsSuccess || response.Value is null)
        {
            return Result<PictureLoaderDeleteResponse>
                .Fail(EntityErrorResources.ExternalImageDeleteFailed);
        }

        return response;
    }
}
