using ShagOxServer.Domain.Base;

namespace ShagOxServer.Domain.Entities.Baskets;
public class BasketAttribute
    : BaseEntity
{
    public int CategoryId { get; set; }
    public int AttributeDefinitionId { get; set; }

    public List<int> SortOrder { get; set; } 
        = new List<int>();

    public override string ToString()
    {
        return $"Cat.: {CategoryId} | Attr.: {AttributeDefinitionId}";
    }
}