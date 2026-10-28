using ShagOxServer.Application.Interfaces.Services.Caches.Invalidations;
using ShagOxServer.Domain.Base;

namespace ShagOxServer.Application.Services.Caches.Invalidations.Specification.Pictures;
public class PictureOwner<TOwner>
    where TOwner : BaseEntity
{
    private readonly ICacheInvalidationService<TOwner, long> _ownerInvalid;


    public PictureOwner(
        ICacheInvalidationService<TOwner, long> ownerInvalid)
    {
        _ownerInvalid = ownerInvalid;
    }


    public async Task InvalidateCreateAsync(
       long ownerId)
    {
        await _ownerInvalid
            .InvalidateCreateAsync(ownerId);
    }

    public async Task InvalidateDeleteAsync(
        long ownerId)
    {
        await _ownerInvalid
            .InvalidateDeleteAsync(ownerId);
    }

    public async Task InvalidateUpdateAsync(
        long ownerId)
    {
        await _ownerInvalid
            .InvalidateUpdateAsync(ownerId);
    }
}
