using ShagOxServer.Application.DTOs.Specification.Pictures.Core.Create.Request;
using ShagOxServer.Application.DTOs.Specification.Pictures.Core.Create.Response;
using ShagOxServer.Domain.Base.Pictures;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Interfaces.Services.Specification.Pictures.Core.Create;
public interface IPictureOrderCreateService<TPicture, TRequest>
    : IPictureCreateService<TPicture, TRequest>
    where TPicture : BasePictureOrder
    where TRequest : PictureCreateRequest
{
    Task<Result<PicturesCreateResponse>> CreateAsync(
        PicturesCreateRequest request);
}
