using ShagOxServer.Application.Interfaces.Repositories.Base;
using ShagOxServer.Application.Interfaces.Services.Common.Context;
using ShagOxServer.Domain.Base;
using ShagOxServer.Infrastructure.Persistence.DbContexts.Primary;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Base;

public class CommandRepository<TEntity>
    : RepositoryContext<TEntity>,
    ICommandRepository<TEntity>
    where TEntity : BaseEntity
{
    public CommandRepository(
        AppDbContext db,
        IUserContext userContext
    )
        : base(db, userContext)
    {
    }
}
