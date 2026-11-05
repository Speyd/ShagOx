using Microsoft.AspNetCore.Mvc;
using ShagOxServer.Api.Controllers.Api;
using ShagOxServer.Api.Controllers.Base.Translations;
using ShagOxServer.Application.DTOs.Specification.Conditions.Translations.Query;
using ShagOxServer.Application.Interfaces.Providers;
using ShagOxServer.Application.Interfaces.Services.Common.Context;
using ShagOxServer.Application.Interfaces.Services.Specification.Conditions.Translations.Query;
using ShagOxServer.Domain.Filters.Specification.Conditions.Translations;

namespace ShagOxServer.Api.Controllers.Specification.Conditions.Translations;

[ApiController]
[Route("api/conditions/translations")]
public class ConditionTranslationQueriesController
    : BaseTranslationQueryController<ConditionTranslationDto,
        ConditionTranslationSearchFilter>
{
    public ConditionTranslationQueriesController(
        IConditionTranslationQueryService queryService,
        ILanguageProvider languageProvider,
        IUserContext userContext
    )
        : base(queryService, languageProvider, userContext)
    {
    }
}
