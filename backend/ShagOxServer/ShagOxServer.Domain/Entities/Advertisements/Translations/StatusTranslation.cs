using ShagOxServer.Domain.Base;

namespace ShagOxServer.Domain.Entities.Advertisements.Translations;
public class StatusTranslation 
    : BaseTranslation<Status>
{
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;


    public override string ToString()
    {
        return $"{Name}";
    }
}