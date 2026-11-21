using ShagOxServer.Domain.Base;
using ShagOxServer.Domain.Entities.Account;
using ShagOxServer.Domain.Entities.Advertisements;
using ShagOxServer.Domain.Entities.Orders;
namespace ShagOxServer.Domain.Entities.Specification;

public class Currency 
    : BaseEntity
{
    public string Code { get; set; } = "";
    public int Ccy { get; set; }
    public int DecimalPlaces { get; set; }
    public string Symbol { get; set; } = "";
    public string Name { get; set; } = "";

    public ICollection<User> Users { get; set; }
        = [];


    public override string ToString()
    {
        return Name;
    }
}
