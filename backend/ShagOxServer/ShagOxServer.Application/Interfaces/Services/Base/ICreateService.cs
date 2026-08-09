using ShagOxServer.Application.DTOs.Common.Responses;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Interfaces.Services.Base;
public interface ICreateService<TResponse, TRequest>
    where TResponse : CreateResponse
{
    Task<Result<TResponse>> CreateAsync(
        TRequest request);
}