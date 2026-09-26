using ShagOxServer.Domain.Base;

namespace ShagOxServer.Domain.Entities.Dictionaries.Attributes;
public class AttributeDictionaryValue
    : BaseEntity
{
    public long DictionaryId { get; set; }
    public AttributeDictionary Dictionary { get; set; } = null!;

    public string Code { get; set; } = null!;

    public string? Value { get; set; } = null;


    public override string ToString()
    {
        return $"Code: {Code} | Value: {Value ?? "Nan"}";
    }
}