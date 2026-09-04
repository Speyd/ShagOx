namespace ShagOxServer.Application.Interfaces.Repositories.Base.Translations;
public interface IExistsTranslationRepository<T>
    : IExistsRepository<T>
{
    Task<bool> ExistsAsync(
       int objectId,
       string language);

    Task<bool> ExistsByLanguageAsync(
        string language);
}