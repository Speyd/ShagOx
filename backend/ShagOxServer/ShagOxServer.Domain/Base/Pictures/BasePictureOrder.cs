using ShagOxServer.Domain.Entities.Specification.Pictures;

namespace ShagOxServer.Domain.Base.Pictures;
public abstract class BasePictureOrder<TOwner>
    : BasePicture<TOwner>
    where TOwner : BaseEntity
{
    public int Order { get; set; }

    public static int GetNextOrder<TPicture>(
        IHasPictures<TOwner, TPicture> owner)
        where TPicture : BasePictureOrder<TOwner>
    {
        return owner.Pictures
            .Select(x => x.Order)
            .DefaultIfEmpty(0)
            .Max() + 1;
    }
}
