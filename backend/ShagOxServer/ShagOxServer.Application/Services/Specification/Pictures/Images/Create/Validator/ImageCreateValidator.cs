using Microsoft.AspNetCore.Http;
using ShagOxServer.Application.DTOs.Common.ImageLoaders.Upload;
using ShagOxServer.Application.Interfaces.Services.Common.ImageLoaders;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Services.Specification.Pictures.Images.Create.Validator;
public class ImageCreateValidator
{
    private readonly IImageLoaderService _loaderService;

    public ImageCreateValidator(
        IImageLoaderService loaderService)
    {
        _loaderService = loaderService;
    }

    public async Task<Result<ImageLoaderUploadResponse>> ImageUploadValidator(
       IFormFile file)
    {
        var response = await _loaderService.UploadAsync(file);
        if (!response.IsSuccess || response.Value is null)
            return Result<ImageLoaderUploadResponse>.Fail("Fail Upload Image");

        return response;
    }
}