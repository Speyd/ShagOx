using ShagOxServer.Application.DTOs.Base.Responses;
using ShagOxServer.Application.Interfaces.Services.Base.Delete;
using ShagOxServer.Domain.Entities.Specification.Translations;

namespace ShagOxServer.Application.Interfaces.Services.Specification.Conditions.Translations.Delete;
public interface IConditionTranslationDeleteService
    : IDeleteService<DeleteResponse>,
    ISoftDeleteService<ConditionTranslation, DeleteResponse>
{
}
