using Microsoft.AspNetCore.Http;
using ShagOxServer.Application.DTOs.Common.ImageLoaders.Upload;
using ShagOxServer.Application.Interfaces.Services.Common.ImageLoaders;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Services.Specification.Pictures.Validator;
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
        var response = await _loaderService.UploadAsync(file);
        if (!response.IsSuccess || response.Value is null)
            return Result<PictureLoaderUploadResponse>.Fail("Fail Upload Image");

        return response;
    }
}