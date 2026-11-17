using ShagOxServer.Domain.Base.Pictures;
using ShagOxServer.Domain.Entities.Dictionaries;

namespace ShagOxServer.Domain.Entities.Specification.Pictures;
public class CategoryImage
    : BasePicture<Category>
{
    public override string ToString()
    {
        return $"Url: {Url}";
    }
}
