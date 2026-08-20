using ShagOxServer.Application.DTOs.Specification.Pictures.Create;
using ShagOxServer.Application.DTOs.Specification.Pictures.Images.Create;
using ShagOxServer.Application.DTOs.Specification.Pictures.Images.Create.File;
using ShagOxServer.Domain.Entities.Advertisements;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Interfaces.Services.Specification.Pictures.Images.Create;
public interface IImageCreateService
{
    Task<Result<PictureCreateResponse>> CreateAsync(
        ImageCreateRequest request);

    Task<Result<PictureCreateResponse>> CreateFromFileAsync(
        ImageFileCreateRequest request);

    Task<Result<PictureCreateResponse>> CreateFromFileAsync(
        Advertisement advertisement,
        ImageFileCreateRequest request);

    Task<Result<ImagesCreateResponse>> CreateFromFilesAsync(
        ImageFilesCreateRequest request);

    Task<Result<PictureCreateResponse>> CreateFromFileInternalAsync(
        ImageFileCreateRequest request);
}