using ShagOxServer.Domain.Base;
using ShagOxServer.Domain.Entities.Account;

namespace ShagOxServer.Domain.Entities.Specification.Pictures;
public class Avatar : BaseImage
{
    public int UserId { get; set; }
    public User User { get; set; } = null!;

    public override string ToString()
    {
        return $"Url: {Url}";
    }
}