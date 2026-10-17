using ShagOxServer.Application.DTOs.Specification.Pictures.Core.Create.Request;
using ShagOxServer.Application.DTOs.Specification.Pictures.Core.Create.Response;
using ShagOxServer.Domain.Base;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Interfaces.Services.Specification.Pictures.Core.Create;
public interface IPictureOrderCreateService<TPicture, TRequest>
    : IPictureCreateService<TPicture, TRequest>
    where TPicture : BaseEntity
    where TRequest : PictureCreateRequest
{
    Task<Result<PicturesCreateResponse>> CreateAsync(
        PicturesCreateRequest request);
}
