namespace ShagOxServer.Domain.Base;
public abstract class BaseTranslation <T> 
    : BaseEntity
    where T : BaseEntity
{
    public int TranslatableId { get; set; }
    public T Translatable { get; set; } = null!;

    public string Language { get; set; } = null!;
}