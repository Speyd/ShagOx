using Microsoft.EntityFrameworkCore;
using ShagOxServer.Application.Interfaces.Repositories.Base.Translations;
using ShagOxServer.Domain.Base;
using ShagOxServer.Infrastructure.Persistence.DbContexts;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Base.Translations;
public class ExistsTranslationRepository<T> 
    : ExistsRepository<T>, IExistsTranslationRepository<T>
    where T: BaseEntity
{
    public ExistsTranslationRepository(AppDbContext db)
        : base(db)
    {
    }

    public virtual async Task<bool> ExistsAsync(
        int translatableId,
        string language)
    {
        return await _db.Set<T>()
            .AnyAsync(x =>
                EF.Property<int>(
                    x,
                    nameof(BaseTranslation<T>.TranslatableId)) == translatableId &&
                EF.Property<string>(
                    x,
                    nameof(BaseTranslation<T>.Language)) == language);
    }

    public virtual async Task<bool> ExistsByLanguageAsync(
        string language)
    {
        return await _db.Set<T>()
            .AnyAsync(x =>
                EF.Property<string>(
                    x,
                    nameof(BaseTranslation<T>.Language)) == language);
    }
}