using ShagOxServer.Domain.Base;
using ShagOxServer.Domain.Entities.Account;

namespace ShagOxServer.Domain.Entities.Location;
public class City : BaseEntity
{
    public string Code { get; set; } = "";
    public int RegionId { get; set; }
    public Region Region { get; set; } = null!;

    public List<User> Users { get; set; } = new List<User>();

    public override string ToString()
    {
        return $"{Code}";
    }
}