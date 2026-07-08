using ShagOxServer.Application.DTOs.Specification.Images.Create;
using ShagOxServer.Domain.Entities.Specification;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Interfaces.Services.Roles.Specification.Images.Create;
public interface IImageCreateService
{
    Task<Result<ImageCreateResponse>> CreateAsync(
        ImageCreateRequest request);

    Task<Result<ImageCreateResponse>> CreateFromFileAsync(
        ImageFileCreateRequest request);
}