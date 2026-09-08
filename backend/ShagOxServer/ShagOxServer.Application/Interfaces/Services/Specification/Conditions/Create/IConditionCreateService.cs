using ShagOxServer.Application.DTOs.Base.Responses;
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