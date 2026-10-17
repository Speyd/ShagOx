using ShagOxServer.Domain.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShagOxServer.Application.Interfaces.Repositories.Specification.Pictures.Core;
public interface IPictureOrderQueryRepository<TEntity>
    where TEntity : BaseEntity
{
    Task<List<TEntity>> GetByOwnerAsync(
        long ownerId,
        CancellationToken cancellationToken = default);
}
