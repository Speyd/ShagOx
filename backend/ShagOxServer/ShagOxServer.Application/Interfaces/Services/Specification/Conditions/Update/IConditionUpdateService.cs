using ShagOxServer.Application.DTOs.Common.Responses;
using ShagOxServer.Application.DTOs.Specification.Conditions.Update;
using ShagOxServer.Application.Interfaces.Services.Base;

namespace ShagOxServer.Application.Interfaces.Services.Specification.Conditions.Update;
public interface IConditionUpdateService
    : IUpdateService<UpdateResponse, ConditionUpdateRequest>
{
}