using ShagOxServer.Application.DTOs.Advertisements.Core.Query;
using ShagOxServer.Application.Interfaces.Services.Advertisements.Core.Query;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Services.Advertisements.Core.Query;
public partial class AdvertisementQueryService
    : IAdvertisementQueryService
{
    public async Task<Result<AdvertisementFacetsDto>> GetFacetsAsync(
        long categoryId,
        CancellationToken cancellationToken = default)
    {
        var facets = await _advertisementRepository
            .GetFacetsAsync(categoryId, cancellationToken);

        return Result<AdvertisementFacetsDto>.Success(facets);
    }
}
