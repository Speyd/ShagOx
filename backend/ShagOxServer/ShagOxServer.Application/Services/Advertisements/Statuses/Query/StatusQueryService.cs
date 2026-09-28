using ShagOxServer.Application.DTOs.Advertisements.Statuses;
using ShagOxServer.Application.Interfaces.Providers;
using ShagOxServer.Application.Interfaces.Repositories.Advertisements.Statuses;
using ShagOxServer.Application.Interfaces.Repositories.Advertisements.Statuses.Translations;
using ShagOxServer.Application.Interfaces.Services.Advertisements.Statuses.Query;
using ShagOxServer.Application.Services.Advertisements.Statuses.Mapping;
using ShagOxServer.Application.Services.Base.Translations;
using ShagOxServer.Domain.Entities.Advertisements;
using ShagOxServer.Domain.Filters.Advertisements;

namespace ShagOxServer.Application.Services.Advertisements.Statuses.Query;
public class StatusQueryService 
    : BaseTranslatableQueryService<
        StatusDto,
        Status,
        StatusSearchFilter
        >,
    IStatusQueryService
{
    private readonly IStatusTranslationQueryRepository _translationRepository;
    private readonly ILanguageProvider _language;


    public StatusQueryService(
        IStatusQueryRepository statusRepository,
        IStatusTranslationQueryRepository translationRepository,
        ILanguageProvider language
    )
        : base(statusRepository)
    {
        _translationRepository = translationRepository;
        _language = language;
    }


    public override async Task<StatusDto> ApplyMapperAsync(
        Status entity)
    {
        var translation = await _translationRepository
            .GetByIdentificatorAsync(entity.Code, _language.Language);

        return StatusMapper.ToDto(entity, translation?.Name);
    }
}