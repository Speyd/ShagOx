using ShagOxServer.Domain.Base;

namespace ShagOxServer.Application.Interfaces.Repositories.Base.Translations;
public interface ITranslationExistsRepository<TEntity, TTranslation>
    : IExistsRepository<TTranslation>
    where TEntity: BaseEntity
    where TTranslation : BaseTranslation<TEntity>
{
    Task<bool> ExistsAsync(
       long objectId,
       string language,
        CancellationToken cancellationToken = default);

    Task<bool> ExistsByLanguageAsync(
        string language,
        CancellationToken cancellationToken = default);

    Task<bool> ExistsByIdentificatorAsync(
        string identificator,
        string language,
        CancellationToken cancellationToken = default);
}
