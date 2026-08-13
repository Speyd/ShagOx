using ShagOxServer.Domain.Base;

namespace ShagOxServer.Domain.Entities.Advertisements.Translations;
public class StatusTranslation : BaseEntity
{
    public int StatusId { get; set; }
    public Status Status { get; set; } = null!;

    public string Language { get; set; } = null!;

    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;


    public override string ToString()
    {
        return $"{Name}";
    }
}