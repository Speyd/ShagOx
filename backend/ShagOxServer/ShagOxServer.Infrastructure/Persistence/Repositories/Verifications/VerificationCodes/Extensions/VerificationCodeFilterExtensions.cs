using ShagOxServer.Domain.Entities.Verifications;
using ShagOxServer.Domain.Filters.Verifications.VerificationCodes;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Verifications.VerificationCodes.Extensions;
public static class VerificationCodeFilterExtensions
{
    public static IQueryable<VerificationCode> Filter(
        this IQueryable<VerificationCode> query,
       VerificationCodeSearchFilter filter)
    {
        if (filter is null)
            return query;

        if (filter.UserId.HasValue)
        {
            query = query.Where(x =>
                x.UserId == filter.UserId);
        }

        if (filter.ExpiresAt.HasValue)
        {
            query = query.Where(x =>
                x.ExpiresAt == filter.ExpiresAt);
        }

        if (filter.UsedAt.HasValue)
        {
            query = query.Where(x =>
                x.UsedAt == filter.UsedAt);
        }

        if (filter.Attempts.HasValue)
        {
            query = query.Where(x =>
                x.Attempts == filter.Attempts);
        }

        if (filter.CreatedAt.HasValue)
        {
            query = query.Where(x =>
                x.CreatedAt == filter.CreatedAt);
        }

        if (filter.InvalidatedAt.HasValue)
        {
            query = query.Where(x =>
                x.InvalidatedAt == filter.InvalidatedAt);
        }

        if (filter.Purpose != default)
        {
            query = query.Where(x =>
                x.Purpose == filter.Purpose);
        }

        return query;
    }
}