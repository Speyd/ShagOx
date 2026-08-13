using ShagOxServer.Domain.Base;
using ShagOxServer.Domain.Entities.Advertisements.Translations;

namespace ShagOxServer.Domain.Entities.Advertisements;
public class Status : BaseEntity
{
    public string Code { get; set; } = null!;

    public List<Advertisement> Advertisements { get; set; } = new();

    public List<StatusTranslation> Translations { get; set; } = new();

    public override string ToString()
    {
        return $"{Code}";
    }
}