using ShagOxServer.Domain.Base;
using ShagOxServer.Domain.Entities.Advertisements.Translations;

namespace ShagOxServer.Domain.Entities.Advertisements;
public class Status
    : BaseEntity
{
    public string Code { get; set; } = null!;

    public ICollection<Advertisement> Advertisements { get; set; } 
        = [];

    public ICollection<StatusTranslation> Translations { get; set; }
        = [];

    public override string ToString()
    {
        return $"{Code}";
    }
}