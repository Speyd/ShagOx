using ShagOxServer.Domain.Base;

namespace ShagOxServer.Domain.Entities.Deliveries.Translations;
public class DeliveryProviderTranslation
    : BaseTranslation<DeliveryProvider>
{
    public string Name { get; set; } = null!;


    public override string GetTranslationValue()
    {
        return Name;
    }

    public override string ToString()
    {
        return $"{Name}";
    }
}
