using ShagOxServer.Application.DTOs.Specification.Images.Create;
using ShagOxServer.Application.DTOs.Specification.Images.Create.File;
using ShagOxServer.Domain.Entities.Advertisements;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Interfaces.Services.Specification.Images.Create;
public interface IImageCreateService
{
    Task<Result<ImageCreateResponse>> CreateAsync(
        ImageCreateRequest request);

    Task<Result<ImageCreateResponse>> CreateFromFileAsync(
        ImageFileCreateRequest request);

    Task<Result<ImageCreateResponse>> CreateFromFileAsync(
        Advertisement advertisement,
        ImageFileCreateRequest request);

    Task<Result<ImagesCreateResponse>> CreateFromFilesAsync(
        ImageFilesCreateRequest request);

    Task<Result<ImageCreateResponse>> CreateFromFileInternalAsync(
        ImageFileCreateRequest request);
}