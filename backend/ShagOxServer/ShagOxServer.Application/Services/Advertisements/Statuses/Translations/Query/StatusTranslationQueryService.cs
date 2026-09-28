using ShagOxServer.Application.DTOs.Advertisements.Statuses.Translations;
using ShagOxServer.Application.Interfaces.Repositories.Advertisements.Statuses.Translations;
using ShagOxServer.Application.Interfaces.Services.Advertisements.Statuses.Translations.Query;
using ShagOxServer.Application.Services.Advertisements.Statuses.Translations.Mapping;
using ShagOxServer.Application.Services.Base.Translations;
using ShagOxServer.Domain.Entities.Advertisements.Translations;
using ShagOxServer.Domain.Filters.Advertisements.Translations;

namespace ShagOxServer.Application.Services.Advertisements.Statuses.Translations.Query;
public class StatusTranslationQueryService
    : BaseTranslationQueryService<
        StatusTranslationDto,
        StatusTranslation,
        StatusTranslationSearchFilter
        >,
    IStatusTranslationQueryService
{
    public StatusTranslationQueryService(
        IStatusTranslationQueryRepository statusRepository
    )
        : base(statusRepository)
    {
    }


    protected override async Task<StatusTranslationDto> ApplyMapperAsync(
        StatusTranslation entity)
    {
        return StatusTranslationMapper.ToDto(entity);
    }
}