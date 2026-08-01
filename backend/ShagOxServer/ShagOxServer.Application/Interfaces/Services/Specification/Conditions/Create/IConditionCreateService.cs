using ShagOxServer.Application.DTOs.Common.Responses;
using ShagOxServer.Application.DTOs.Specification.Conditions.Create;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Interfaces.Services.Specification.Conditions.Create;
public interface IConditionCreateService
{
    Task<Result<CreateResponse>> CreateAsync(
       ConditionCreateRequest request);
}