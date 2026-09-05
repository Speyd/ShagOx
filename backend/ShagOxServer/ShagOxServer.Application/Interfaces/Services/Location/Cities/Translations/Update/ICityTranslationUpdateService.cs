using ShagOxServer.Application.DTOs.Common.Responses;
using ShagOxServer.Application.DTOs.Location.Cities.Translations.Update;
using ShagOxServer.Application.Interfaces.Services.Base;

namespace ShagOxServer.Application.Interfaces.Services.Location.Cities.Translations.Update;
public interface ICityTranslationUpdateService
    : IUpdateService<UpdateResponse,
        CityTranslationUpdateRequest>
{
}