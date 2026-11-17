using ShagOxServer.Application.DTOs.Advertisements.Core.Query;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Interfaces.Services.Advertisements.Core.Query;
public partial interface IAdvertisementQueryService
{
    Task<Result<AdvertisementFacetsDto>> GetFacetsAsync(
        long categoryId,
        CancellationToken cancellationToken = default);
}
