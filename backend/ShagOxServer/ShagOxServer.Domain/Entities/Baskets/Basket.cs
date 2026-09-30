using ShagOxServer.Domain.Base;
using ShagOxServer.Domain.Entities.Account;

namespace ShagOxServer.Domain.Entities.Baskets;
public class Basket
    : BaseEntity
{
    public User User { get; set; } = null!;
    public long UserId { get; set; }


    public ICollection<BasketItem> BasketItems { get; set; }
        = [];


    public override string ToString()
    {
        return $"userId: {UserId}";
    }
}