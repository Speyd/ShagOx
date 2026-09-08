using ShagOxServer.Application.DTOs.Base.Responses;
using ShagOxServer.Application.DTOs.Location.Regions.Translations.Update;
using ShagOxServer.Application.Interfaces.Services.Base;

namespace ShagOxServer.Application.Interfaces.Services.Location.Regions.Translations.Update;
public interface IRegionTranslationUpdateService
    : IUpdateService<UpdateResponse,
        RegionTranslationUpdateRequest>
{
}