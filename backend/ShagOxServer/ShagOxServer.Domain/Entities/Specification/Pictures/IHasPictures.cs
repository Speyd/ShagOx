using ShagOxServer.Domain.Base;
using ShagOxServer.Domain.Base.Pictures;

namespace ShagOxServer.Domain.Entities.Specification.Pictures;
public interface IHasPictures<TOwner, TPicture>
    where TOwner : BaseEntity
    where TPicture : BasePictureOrder<TOwner>
{
    ICollection<TPicture> Pictures { get; set; }
}
