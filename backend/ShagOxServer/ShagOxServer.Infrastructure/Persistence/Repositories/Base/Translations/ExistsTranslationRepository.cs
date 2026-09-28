using Microsoft.EntityFrameworkCore;
using ShagOxServer.Application.Interfaces.Repositories.Base.Translations;
using ShagOxServer.Domain.Base;
using ShagOxServer.Infrastructure.Persistence.DbContexts;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Base.Translations;
public class ExistsTranslationRepository<TEntity, TTranslation> 
    : ExistsRepository<TTranslation>, 
    ITranslationExistsRepository<TEntity, TTranslation>
    where TEntity : BaseTranslatable
    where TTranslation : BaseTranslation<TEntity>
{
    public ExistsTranslationRepository(AppDbContext db)
        : base(db)
    {
    }

    public virtual async Task<bool> ExistsAsync(
        long translatableId,
        string language)
    {
        return await _db.Set<TEntity>()
            .AnyAsync(x =>
                EF.Property<long>(
                    x,
                    nameof(BaseTranslation<TTranslation>.TranslatableId)) == translatableId &&
                EF.Property<string>(
                    x,
                    nameof(BaseTranslation<TTranslation>.Language)) == language);
    }

    public virtual async Task<bool> ExistsByLanguageAsync(
        string language)
    {
        return await _db.Set<TEntity>()
            .AnyAsync(x =>
                EF.Property<string>(
                    x,
                    nameof(BaseTranslation<TTranslation>.Language)) == language);
    }

    public virtual async Task<bool> ExistsByIdentificatorAsync(
        string identificator,
        string language)
    {
        IQueryable<TTranslation> query = _db.Set<TTranslation>();

        query = ApplyIncludes(query);

        query = ApplyIdentificatorFilter(query, identificator);

        return await query.AnyAsync(
            x => x.Language == language);
    }

    protected virtual IQueryable<TTranslation> ApplyIdentificatorFilter(
        IQueryable<TTranslation> query,
        string identificator)
    {
        return query;
    }
}