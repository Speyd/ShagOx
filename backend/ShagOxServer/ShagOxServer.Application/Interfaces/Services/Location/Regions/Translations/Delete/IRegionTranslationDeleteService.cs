using ShagOxServer.Application.DTOs.Base.Responses;
using ShagOxServer.Application.Interfaces.Services.Base.Delete;
using ShagOxServer.Domain.Entities.Location.Translations;

namespace ShagOxServer.Application.Interfaces.Services.Location.Regions.Translations.Delete;
public interface IRegionTranslationDeleteService
    : IDeleteService<DeleteResponse>,
    ISoftDeleteService<RegionTranslation, DeleteResponse>
{
}
