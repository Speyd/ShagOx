using ShagOxServer.Application.DTOs.Advertisements.Statuses.Translations.Create;
using ShagOxServer.Application.DTOs.Common.Responses;
using ShagOxServer.Application.Interfaces.Services.Base;

namespace ShagOxServer.Application.Interfaces.Services.Advertisements.Statuses.Translations.Create;
public interface IStatusTranslationCreateService
    : ICreateService<
        CreateResponse,
        StatusTranslationCreateRequest
        >
{
}