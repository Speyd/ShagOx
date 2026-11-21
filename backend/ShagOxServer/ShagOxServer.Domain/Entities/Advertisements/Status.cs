using ShagOxServer.Domain.Base;
using ShagOxServer.Domain.Entities.Advertisements.Enum;
using ShagOxServer.Domain.Entities.Advertisements.Translations;

namespace ShagOxServer.Domain.Entities.Advertisements;
public class Status
    : BaseTranslatable
{
    public AdvertisementStatus Code { get; set; }

    public ICollection<Advertisement> Advertisements { get; set; } 
        = [];

    public ICollection<StatusTranslation> Translations { get; set; }
        = [];

    public override string GetIdentificator()
    {
        return Code.ToString();
    }

    public override string ToString()
    {
        return $"{Code}";
    }
}
