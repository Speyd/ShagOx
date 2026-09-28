using ShagOxServer.Domain.Base;

namespace ShagOxServer.Domain.Entities.Dictionaries.Translations;
public class CategoryTranslation
    : BaseTranslation<Category>
{
    public string Name { get; set; } = null!;


    public override string GetIdentificator()
    {
        return Name;
    }

    public override string ToString()
    {
        return $"{Name}";
    }
}