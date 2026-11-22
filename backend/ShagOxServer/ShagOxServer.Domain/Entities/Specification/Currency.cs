using ShagOxServer.Domain.Base;
using ShagOxServer.Domain.Entities.Account;
namespace ShagOxServer.Domain.Entities.Specification;

public class Currency 
    : BaseEntity
{
    public string Code { get; set; } = "";
    public int NumericCode { get; set; }
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
