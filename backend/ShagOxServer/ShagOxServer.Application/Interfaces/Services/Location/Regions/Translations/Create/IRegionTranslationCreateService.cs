using ShagOxServer.Application.DTOs.Common.Responses;
using ShagOxServer.Application.DTOs.Location.Regions.Translations.Create;
using ShagOxServer.Application.Interfaces.Services.Base;

namespace ShagOxServer.Application.Interfaces.Services.Location.Regions.Translations.Create;
public interface IRegionTranslationCreateService
    : ICreateService<
        CreateResponse,
        RegionTranslationCreateRequest
        >
{
}