using Microsoft.AspNetCore.Mvc;
using ShagOxServer.Api.Controllers.Api;
using ShagOxServer.Api.Controllers.Base.Translations;
using ShagOxServer.Application.DTOs.Dictionaries.Attributes.AttributeDefinitions.Translations.Query;
using ShagOxServer.Application.Interfaces.Providers;
using ShagOxServer.Application.Interfaces.Services.Common.Context;
using ShagOxServer.Application.Interfaces.Services.Dictionaries.Attributes.AttributeDefinitions.Translations.Query;
using ShagOxServer.Domain.Filters.Dictionaries.Attributes.AttributeDefinitions.Translations;

namespace ShagOxServer.Api.Controllers.Dictionaries.Attributes.AttributeDefinitions.Translations;

[ApiController]
[Route("api/attribute-definitions/translations")]
public class AttributeDefinitionTranslationQueriesController
    : BaseTranslationQueryController<AttributeDefinitionTranslationDto,
        AttributeDefinitionTranslationSearchFilter>
{
    public AttributeDefinitionTranslationQueriesController(
        IAttributeDefinitionTranslationQueryService queryService,
        ILanguageProvider languageProvider,
        IUserContext userContext
    )
        : base(queryService, languageProvider, userContext)
    {
    }
}
