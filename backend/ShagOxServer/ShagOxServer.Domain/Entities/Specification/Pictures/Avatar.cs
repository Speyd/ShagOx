using ShagOxServer.Domain.Base.Pictures;
using ShagOxServer.Domain.Entities.Account;

namespace ShagOxServer.Domain.Entities.Specification.Pictures;
public class Avatar 
    : BasePicture<User>
{
    public override string ToString()
    {
        return $"Url: {Url}";
    }
}
