using ShagOxServer.Domain.Base;
using ShagOxServer.Domain.Entities.Dictionaries;

namespace ShagOxServer.Domain.Entities.Baskets;
public class BasketAttribute
    : BaseEntity
{
    public AttributeDefinition AttributeDefinition { get; set; } = null!;
    public int AttributeDefinitionId { get; set; }

    public int Order { get; set; }


    public override string ToString()
    {
        return $"Attr.: {AttributeDefinitionId} | Ord.: {Order}";
    }
}