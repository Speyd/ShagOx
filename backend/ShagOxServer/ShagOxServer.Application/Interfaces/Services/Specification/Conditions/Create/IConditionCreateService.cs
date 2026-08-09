using ShagOxServer.Application.DTOs.Common.Responses;
using ShagOxServer.Application.DTOs.Specification.Conditions.Create;
using ShagOxServer.Application.Interfaces.Services.Base;

namespace ShagOxServer.Application.Interfaces.Services.Specification.Conditions.Create;
public interface IConditionCreateService
    : ICreateService<
        CreateResponse,
        ConditionCreateRequest
        >
{
}