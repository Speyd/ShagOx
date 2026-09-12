using ShagOxServer.Domain.Base;
using ShagOxServer.Domain.Entities.Account;

namespace ShagOxServer.Domain.Entities.Baskets;
public class Basket
    : BaseEntity
{
    public User User { get; set; } = null!;
    public int UserId { get; set; }


    public List<BasketItem> BasketItems { get; set; } 
        = new List<BasketItem>();

    public override string ToString()
    {
        return $"userId: {UserId}";
    }
}