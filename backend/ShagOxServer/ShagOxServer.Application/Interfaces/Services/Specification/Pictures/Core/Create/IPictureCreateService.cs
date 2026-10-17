using ShagOxServer.Application.DTOs.Specification.Pictures.Core.Create.Request;
using ShagOxServer.Application.DTOs.Specification.Pictures.Core.Create.Response;
using ShagOxServer.Application.Interfaces.Services.Base.Create;
using ShagOxServer.Domain.Base;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Interfaces.Services.Specification.Pictures.Core.Create;
public interface IPictureCreateService<TPicture, TRequest>
    : ICreateService<
        PictureCreateResponse,
        TRequest
        >
    where TPicture : BaseEntity
    where TRequest : PictureCreateRequest
{
    //Task<Result<PictureCreateResponse>> CreateAsync(
    //    Advertisement advertisement,
    //    PictureCreateRequest request);

    Task<Result<PictureCreateResponse>> CreateInternalAsync(
        TRequest request);
}
