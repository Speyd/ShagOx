using Microsoft.EntityFrameworkCore;
using ShagOxServer.Domain.Base.Cache;

namespace ShagOxServer.Application.DTOs.Cache;

public record PendingCacheInvalidation
(
    IEntityCache Entity,
    EntityState State,
    object CacheContext
);
