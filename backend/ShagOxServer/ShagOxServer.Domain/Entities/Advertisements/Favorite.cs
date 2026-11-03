using ShagOxServer.Domain.Base.Owner;
using ShagOxServer.Domain.Entities.Account;

namespace ShagOxServer.Domain.Entities.Advertisements;
public class Favorite 
    : OwnedEntity<User>
{
    public long AdvertisementId { get; set; }
    public Advertisement Advertisement { get; set; } = null!;


    public override string ToString()
    {
        return $"{OwnerId}-{AdvertisementId}";
    }
}
