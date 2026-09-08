using ShagOxServer.Application.DTOs.Base.Responses;
using ShagOxServer.Application.Interfaces.Repositories.Base;
using ShagOxServer.Application.Interfaces.Services.Common.ImageLoaders;
using ShagOxServer.Application.Interfaces.Services.Specification.Pictures.Images.Delete;
using ShagOxServer.Application.Services.Specification.Pictures.Images.Validator;
using ShagOxServer.Domain.Entities.Specification.Pictures;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Services.Specification.Pictures.Images.Delete;
public class ImageDeleteService 
    : IImageDeleteService
{
    private readonly IRepository<Image> _imageRepository;
    private readonly ImageValidator _imageValidator;
    private readonly IPictureLoaderService _loaderService;


    public ImageDeleteService(
        IRepository<Image> imageRepository,
        ImageValidator imageValidator,
        IPictureLoaderService loaderService)
    {
        _imageRepository = imageRepository;
        _imageValidator = imageValidator;
        _loaderService = loaderService;
    }


    public async Task<Result<DeleteResponse>> DeleteAsync(
        int id)
    {
        var image = await _imageValidator.GetByIdAsync(id);
        if (!image.IsSuccess)
            return Result<DeleteResponse>.Fail(image.Error);

        var result = await _loaderService
            .DeleteAsync(image.Value!.PublicId);

        if (!result.IsSuccess)
            return Result<DeleteResponse>.Fail(result.Error!);

        _imageRepository.Delete(image.Value!);

        return Result<DeleteResponse>.Success(
           new DeleteResponse(
               image.Value!.Id,
               DateTime.UtcNow
           )
       );
    }

    public async Task<Result<DeleteResponse>> DeleteRecordAsync(
        int id)
    {
        var image = await _imageValidator.GetByIdAsync(id);
        if (!image.IsSuccess)
            return Result<DeleteResponse>.Fail(image.Error);

        _imageRepository.Delete(image.Value!);

        return Result<DeleteResponse>.Success(
           new DeleteResponse(
               image.Value!.Id,
               DateTime.UtcNow
           )
       );
    }
}