using ShagOxServer.Application.DTOs.Base.Responses;
using ShagOxServer.Application.DTOs.Specification.Conditions.Update;
using ShagOxServer.Application.Interfaces.Services.Base;

namespace ShagOxServer.Application.Interfaces.Services.Specification.Conditions.Update;
public interface IConditionUpdateService
    : IUpdateService<UpdateResponse, ConditionUpdateRequest>
{
}