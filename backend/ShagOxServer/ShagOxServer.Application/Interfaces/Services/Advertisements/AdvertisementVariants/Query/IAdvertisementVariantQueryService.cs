using ShagOxServer.Application.DTOs.Advertisements.AdvertisementVariants.Query;
using ShagOxServer.Application.Interfaces.Services.Base;
using ShagOxServer.Domain.Entities.Advertisements;
using ShagOxServer.Domain.Filters.Advertisements;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Interfaces.Services.Advertisements.AdvertisementVariants.Query;
public interface IAdvertisementVariantQueryService
    : IQueryService<AdvertisementVariantDto,
        AdvertisementVariant,
        AdvertisementVariantSearchFilter>
{
    Task<Result<List<VariantAttributeDto>>> GetVariantAttributeAsync(
        AdvertisementVariant variant);
}