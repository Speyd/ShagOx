using ShagOxServer.Application.DTOs.Common.Responses;
using ShagOxServer.Application.DTOs.Specification.Conditions.Translations.Create;
using ShagOxServer.Application.Interfaces.Services.Base;

namespace ShagOxServer.Application.Interfaces.Services.Specification.Conditions.Translations.Create;
public interface IConditionTranslationCreateService
    : ICreateService<
        CreateResponse,
        ConditionTranslationCreateRequest
        >
{
}