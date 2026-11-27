using ShagOxServer.Domain.Base;
using ShagOxServer.Domain.Base.Cache;

namespace ShagOxServer.Application.Interfaces.Repositories.Base;
public interface ICommandRepository<TEntity>
    where TEntity : BaseEntity
{
}
