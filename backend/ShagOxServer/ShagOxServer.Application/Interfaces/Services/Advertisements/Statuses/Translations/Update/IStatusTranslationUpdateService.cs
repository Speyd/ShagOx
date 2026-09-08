using ShagOxServer.Application.DTOs.Advertisements.Statuses.Translations.Update;
using ShagOxServer.Application.DTOs.Base.Responses;
using ShagOxServer.Application.Interfaces.Services.Base;

namespace ShagOxServer.Application.Interfaces.Services.Advertisements.Statuses.Translations.Update;
public interface IStatusTranslationUpdateService
    : IUpdateService<UpdateResponse, 
        StatusTranslationUpdateRequest>
{
}