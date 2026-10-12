using ShagOxServer.Application.Interfaces.Repositories.Base;
using ShagOxServer.Application.Interfaces.Repositories.Base.Query;
using ShagOxServer.Application.Interfaces.Repositories.Location.Regions.Translations;
using ShagOxServer.Application.Services.Base.Translations;
using ShagOxServer.Domain.Entities.Location;
using ShagOxServer.Domain.Entities.Location.Translations;

namespace ShagOxServer.Application.Services.Location.Regions.Translations.Validator;
public class RegionTranslationValidator
    : BaseTranslationValidator<Region,
        RegionTranslation>
{
    public RegionTranslationValidator(
        IQueryRepository<RegionTranslation> regionRepository,
        IRegionTranslationExistsRepository regionExistsRepository
    ) : base(regionRepository, regionExistsRepository)
    {
    }
}
