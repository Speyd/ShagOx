using ShagOxServer.Application.DTOs.Base.Responses;
using ShagOxServer.Application.Interfaces.Services.Base.Delete;
using ShagOxServer.Domain.Entities.Advertisements.Translations;

namespace ShagOxServer.Application.Interfaces.Services.Advertisements.Statuses.Translations.Delete;
public interface IStatusTranslationDeleteService
    : IDeleteService<DeleteResponse>, 
    ISoftDeleteService<StatusTranslation, DeleteResponse>
{
}
