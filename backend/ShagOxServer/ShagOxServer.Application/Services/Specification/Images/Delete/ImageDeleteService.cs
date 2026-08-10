using ShagOxServer.Application.DTOs.Common.Responses;
using ShagOxServer.Application.Interfaces.Repositories.Base;
using ShagOxServer.Application.Interfaces.Services.Common.ImageLoaders;
using ShagOxServer.Application.Interfaces.Services.Specification.Images.Delete;
using ShagOxServer.Application.Services.Specification.Images.Validator;
using ShagOxServer.Domain.Entities.Specification;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Services.Specification.Images.Delete;
public class ImageDeleteService 
    : IImageDeleteService
{
    private readonly IRepository<Image> _imageRepository;
    private readonly ImageValidator _imageValidator;
    private readonly IImageLoaderService _loaderService;


    public ImageDeleteService(
        IRepository<Image> imageRepository,
        ImageValidator imageValidator,
        IImageLoaderService loaderService)
    {
        _imageRepository = imageRepository;
        _imageValidator = imageValidator;
        _loaderService = loaderService;
    }


    public async Task<Result<DeleteResponse>> DeleteAsync(int id)
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

    public async Task<Result<DeleteResponse>> DeleteRecordAsync(int id)
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