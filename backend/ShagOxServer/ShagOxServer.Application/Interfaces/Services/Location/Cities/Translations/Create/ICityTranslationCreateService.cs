using ShagOxServer.Application.DTOs.Base.Responses;
using ShagOxServer.Application.DTOs.Location.Cities.Translations.Create;
using ShagOxServer.Application.Interfaces.Services.Base;

namespace ShagOxServer.Application.Interfaces.Services.Location.Cities.Translations.Create;
public interface ICityTranslationCreateService
    : ICreateService<
        CreateResponse,
        CityTranslationCreateRequest
        >
{
}