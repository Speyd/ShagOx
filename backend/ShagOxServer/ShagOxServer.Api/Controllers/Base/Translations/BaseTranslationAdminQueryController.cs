using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShagOxServer.Api.Controllers.Base.Query;
using ShagOxServer.Application.DTOs.Base;
using ShagOxServer.Application.Interfaces.Providers;
using ShagOxServer.Application.Interfaces.Services.Base.Translations.Translation;
using ShagOxServer.Domain.Base.Enum;
using ShagOxServer.Domain.Filters;
using ShagOxServer.SharedKernel.Abstractions.Paginations;
using ShagOxServer.SharedKernel.Abstractions.Results.Extensions;

namespace ShagOxServer.Api.Controllers.Base.Translations;

[ApiController]
[Authorize(Roles = "Admin")]
public abstract class BaseTranslationAdminQueryController<TDto, TFilter>
    : BaseAdminQueryController<TDto, TFilter>
    where TDto : BaseDto
    where TFilter : BaseFilter
{
    protected readonly ITranslationQueryOperations<TDto, TFilter>
       _translationQueryService;

    private readonly ILanguageProvider _languageProvider;


    protected BaseTranslationAdminQueryController(
       ITranslationQueryOperations<TDto, TFilter> queryService,
       ILanguageProvider languageProvider
    )
        : base(queryService)
    {
        _translationQueryService = queryService;
        _languageProvider = languageProvider;
    }


    [HttpGet]
    public override async Task<IActionResult> GetPaged(
        [FromQuery] PaginationParams pagination,
        [FromQuery] EntityStatus status,
        CancellationToken cancellationToken)
    {
        string language = _languageProvider.Language;

        var result = await _translationQueryService
            .GetPagedAsync(pagination, language,
                cancellationToken, status);

        return result.ToActionResult();
    }
}
