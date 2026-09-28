using ShagOxServer.Domain.Base;
using ShagOxServer.Domain.Entities.Account;

namespace ShagOxServer.Domain.Entities.Advertisements;
public class Favorite 
    : BaseEntity
{
    public long UserId { get; set; }
    public User User { get; set; } = null!;


    public long AdvertisementId { get; set; }
    public Advertisement Advertisement { get; set; } = null!;


    public override string ToString()
    {
        return $"{UserId}-{AdvertisementId}";
    }
}
