using ShagOxServer.Domain.Base.Owner;
using ShagOxServer.Domain.Entities.Auth.Users;

namespace ShagOxServer.Domain.Entities.Baskets;
public class Basket
    : OwnedEntity<User>
{
    public ICollection<BasketItem> BasketItems { get; set; }
        = [];


    public override string ToString()
    {
        return $"OwnerId: {OwnerId}";
    }
}
