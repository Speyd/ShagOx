using ShagOxServer.Application.DTOs.Base.Responses;
using ShagOxServer.Application.Interfaces.Services.Base.Delete;
using ShagOxServer.Domain.Entities.Advertisements;

namespace ShagOxServer.Application.Interfaces.Services.Advertisements.AdvertisementVariants.Delete;
public interface IAdvertisementVariantDeleteService
    : IDeleteService<DeleteResponse>,
    ISoftDeleteService<AdvertisementVariant, DeleteResponse>
{
}
