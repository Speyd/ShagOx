using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShagOxServer.Api.Controllers.Base.Translations;
using ShagOxServer.Application.DTOs.Dictionaries.Categories.Translations.Query;
using ShagOxServer.Application.Interfaces.Providers;
using ShagOxServer.Application.Interfaces.Services.Common.Context;
using ShagOxServer.Application.Interfaces.Services.Dictionaries.Categories.Translations.Query;
using ShagOxServer.Domain.Filters.Dictionaries.Categories.Translations;

namespace ShagOxServer.Api.Controllers.Dictionaries.Categories.Translations.Admin;

[ApiController]
[Route("api/admin/categories/translations")]
[Authorize(Roles = "Admin")]
public class CategoryTranslationAdminQueriesController
    : BaseTranslationAdminQueryController<CategoryTranslationDto,
        CategoryTranslationSearchFilter>
{
    public CategoryTranslationAdminQueriesController(
        ICategoryTranslationQueryService queryService,
        ILanguageProvider languageProvider,
        IUserContext userContext
    )
        : base(queryService, languageProvider, userContext)
    {
    }
}
