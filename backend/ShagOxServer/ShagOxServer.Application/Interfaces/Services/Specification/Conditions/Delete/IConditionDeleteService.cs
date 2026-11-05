using ShagOxServer.Application.DTOs.Base.Responses;
using ShagOxServer.Application.Interfaces.Services.Base.Delete;
using ShagOxServer.Domain.Entities.Specification;

namespace ShagOxServer.Application.Interfaces.Services.Specification.Conditions.Delete;
public interface IConditionDeleteService
    : IDeleteService<DeleteResponse>,
    ISoftDeleteService<Condition, DeleteResponse>
{
}
