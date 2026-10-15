using Microsoft.AspNetCore.Mvc;
using ShagOxServer.Api.Controllers.Api;
using ShagOxServer.Api.Controllers.Base;
using ShagOxServer.Application.DTOs.Dictionaries.Categories.Translations;
using ShagOxServer.Application.Interfaces.Providers;
using ShagOxServer.Application.Interfaces.Services.Dictionaries.Categories.Translations.Query;
using ShagOxServer.Domain.Filters.Dictionaries.Categories.Translations;

namespace ShagOxServer.Api.Controllers.Dictionaries.Categories.Translations;

[ApiController]
[Route("api/categories/translations")]
public class CategoryTranslationQueriesController
    : BaseTranslationQueryController<CategoryTranslationDto,
        CategoryTranslationSearchFilter>
{
    public CategoryTranslationQueriesController(
        ICategoryTranslationQueryService queryService,
        ILanguageProvider languageProvider
    )
        : base(queryService, languageProvider)
    {
    }
}
