using ShagOxServer.Domain.Base;
using ShagOxServer.Domain.Entities.Deliveries.Translations;

namespace ShagOxServer.Domain.Entities.Deliveries;
public class DeliveryProvider
     : BaseTranslatable
{
    public string Code { get; set; } = null!;

    public ICollection<DeliveryProviderTranslation> Translations { get; set; }
        = [];


    public override string GetIdentificator()
    {
        return Code;
    }

    public override string ToString()
    {
        return $"Id: {Id} | Code: {Code}";
    }
}
