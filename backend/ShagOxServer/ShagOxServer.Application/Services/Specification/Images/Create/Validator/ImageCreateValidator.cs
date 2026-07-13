using Microsoft.AspNetCore.Http;
using ShagOxServer.Application.DTOs.Common.ImageLoaders.Upload;
using ShagOxServer.Application.DTOs.Specification.Images.Create;
using ShagOxServer.Application.Interfaces.Repositories.Advertisements;
using ShagOxServer.Application.Interfaces.Repositories.Specification.Images;
using ShagOxServer.Application.Interfaces.Services.Common.ImageLoaders;
using ShagOxServer.Domain.Entities.Advertisements;
using ShagOxServer.SharedKernel.Abstractions.Results;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShagOxServer.Application.Services.Specification.Images.Create.Validator;
public class ImageCreateValidator
{
    private readonly IImageLoaderService _loaderService;

    private readonly IAdvertisementRepository _advertisementRepository;


    public ImageCreateValidator(
        IImageLoaderService loaderService,
        IAdvertisementRepository advertisementRepository)
    {
        _loaderService = loaderService;
        _advertisementRepository = advertisementRepository;
    }

    public async Task<Result<Advertisement>> AdvertisementValidator(
        int advertId)
    {
        var advert = await _advertisementRepository.GetByIdAsync(advertId);
        if (advert is null)
            return Result<Advertisement>.NotFound("Advertisement");

        return Result<Advertisement>.Success(advert);
    }

    public async Task<Result<ImageLoaderUploadResponse>> ImageUploaderValidator(
       IFormFile file)
    {
        var response = await _loaderService.UploadAsync(file);
        if (!response.IsSuccess || response.Value is null)
            return Result<ImageLoaderUploadResponse>.Fail("Fail Upload Image");

        return response;
    }
}
