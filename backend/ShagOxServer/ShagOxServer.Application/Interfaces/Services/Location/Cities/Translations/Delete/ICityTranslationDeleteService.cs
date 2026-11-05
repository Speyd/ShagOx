using ShagOxServer.Application.DTOs.Base.Responses;
using ShagOxServer.Application.Interfaces.Services.Base.Delete;
using ShagOxServer.Domain.Entities.Location.Translations;

namespace ShagOxServer.Application.Interfaces.Services.Location.Cities.Translations.Delete;
public interface ICityTranslationDeleteService
    : IDeleteService<DeleteResponse>,
    ISoftDeleteService<CityTranslation, DeleteResponse>
{
}
