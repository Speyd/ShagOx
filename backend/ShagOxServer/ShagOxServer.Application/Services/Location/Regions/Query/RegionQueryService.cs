using ShagOxServer.Application.DTOs.Location.Regions;
using ShagOxServer.Application.Interfaces.Providers;
using ShagOxServer.Application.Interfaces.Repositories.Location.Regions;
using ShagOxServer.Application.Interfaces.Repositories.Location.Regions.Translations;
using ShagOxServer.Application.Interfaces.Services.Location.Regions.Query;
using ShagOxServer.Application.Services.Base;
using ShagOxServer.Application.Services.Base.Translations;
using ShagOxServer.Application.Services.Location.Regions.Mapping;
using ShagOxServer.Domain.Entities.Location;
using ShagOxServer.Domain.Filters.Location.Regions;
using ShagOxServer.SharedKernel.Abstractions.Results;
using ShagOxServer.SharedKernel.Abstractions.Results.Extensions;

namespace ShagOxServer.Application.Services.Location.Regions.Query;
public class RegionQueryService 
    : BaseTranslatableQueryService<
        RegionDto,
        Region,
        RegionSearchFilter
        >,
    IRegionQueryService
{
    private readonly IRegionQueryRepository _regionQueryRepository;
    private readonly IRegionTranslationQueryRepository _translationRepository;

    private readonly ILanguageProvider _language;



    public RegionQueryService(
        IRegionQueryRepository regionQueryRepository,
        IRegionTranslationQueryRepository translationRepository,
        ILanguageProvider language
    )
        : base(regionQueryRepository)
    {
        _regionQueryRepository = regionQueryRepository;
        _translationRepository = translationRepository;
        _language = language;
    }


    public override async Task<RegionDto> ApplyMapperAsync(
        Region entity)
    {
        var translation = await _translationRepository
            .GetByIdentificatorAsync(entity.Code, _language.Language);

        return RegionMapper.ToDto(entity, translation?.Name);
    }
}