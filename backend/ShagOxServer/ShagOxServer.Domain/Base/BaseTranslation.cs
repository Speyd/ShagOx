namespace ShagOxServer.Domain.Base;
/// <summary>
/// Represents a translation associated with a translatable entity.
/// </summary>
/// <typeparam name="T">
/// The type of the entity being translated.
/// </typeparam>
public abstract class BaseTranslation<T> 
    : BaseEntity
    where T : BaseEntity
{
    public int TranslatableId { get; set; }
    public T Translatable { get; set; } = null!;

    public string Language { get; set; } = null!;
}