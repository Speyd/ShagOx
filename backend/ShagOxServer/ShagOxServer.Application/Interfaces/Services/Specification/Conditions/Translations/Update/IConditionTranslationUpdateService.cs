using ShagOxServer.Application.DTOs.Common.Responses;
using ShagOxServer.Application.DTOs.Specification.Conditions.Translations.Update;
using ShagOxServer.Application.Interfaces.Services.Base;

namespace ShagOxServer.Application.Interfaces.Services.Specification.Conditions.Translations.Update;
public interface IConditionTranslationUpdateService
    : IUpdateService<UpdateResponse,
      ConditionTranslationUpdateRequest>
{
}