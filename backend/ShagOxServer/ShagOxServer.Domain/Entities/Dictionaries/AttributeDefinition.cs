
namespace ShagOxServer.Domain.Entities.Dictionaries;
public class AttributeDefinition
{
    public int Id { get; set; }

    public int CategoryId { get; set; }
    public Category Category { get; set; } = null!;

    public string Key { get; set; } = null!;
    public string Type { get; set; } = null!;
    public bool Required { get; set; }

    public int? Min { get; set; }
    public int? Max { get; set; }
}
