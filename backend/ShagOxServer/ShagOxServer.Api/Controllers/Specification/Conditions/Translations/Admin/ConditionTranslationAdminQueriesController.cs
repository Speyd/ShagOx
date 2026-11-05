using Microsoft.AspNetCore.Mvc;
using ShagOxServer.Api.Controllers.Base.Translations;
using ShagOxServer.Application.DTOs.Specification.Conditions.Translations.Query;
using ShagOxServer.Application.Interfaces.Providers;
using ShagOxServer.Application.Interfaces.Services.Common.Context;
using ShagOxServer.Application.Interfaces.Services.Specification.Conditions.Translations.Query;
using ShagOxServer.Domain.Filters.Specification.Conditions.Translations;

namespace ShagOxServer.Api.Controllers.Specification.Conditions.Translations.Admin;

[ApiController]
[Route("api/admin/conditions/translations")]
public class ConditionTranslationAdminQueriesController
    : BaseTranslationAdminQueryController<ConditionTranslationDto,
        ConditionTranslationSearchFilter>
{
    public ConditionTranslationAdminQueriesController(
        IConditionTranslationQueryService queryService,
        ILanguageProvider languageProvider,
        IUserContext userContext
    )
        : base(queryService, languageProvider, userContext)
    {
    }
}
