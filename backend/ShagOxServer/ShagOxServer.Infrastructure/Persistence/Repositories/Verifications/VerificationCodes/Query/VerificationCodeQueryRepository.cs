using Microsoft.EntityFrameworkCore;
using ShagOxServer.Application.Interfaces.Repositories.Verifications.VerificationCodes;
using ShagOxServer.Domain.Base.Enum;
using ShagOxServer.Domain.Entities.Verifications;
using ShagOxServer.Domain.Filters.Verifications.VerificationCodes;
using ShagOxServer.Infrastructure.Persistence.DbContexts.Replica;
using ShagOxServer.Infrastructure.Persistence.Repositories.Base.Extensions;
using ShagOxServer.Infrastructure.Persistence.Repositories.Base.Query;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Verifications.VerificationCodes.Query;
public partial class VerificationCodeQueryRepository
    : SearchRepository<VerificationCode, VerificationCodeSearchFilter>,
      IVerificationCodeQueryRepository
{
    public VerificationCodeQueryRepository(
        ReplicaDbContext db)
        : base(db)
    { }


    public async Task<VerificationCode?> GetActiveByUserIdAsync(
        long userId,
        CancellationToken cancellationToken = default,
        EntityStatus? status = null)
    {
        return await _db.VerificationCodes
            .WithStatusFilter(status)
            .FirstOrDefaultAsync(x =>
                x.UserId == userId &&
                x.UsedAt == null &&
                x.ExpiresAt > DateTime.UtcNow &&
                x.InvalidatedAt == null,
                cancellationToken);
    }

    public async Task<VerificationCode?> GetLatestByUserIdAsync(
        long userId,
        CancellationToken cancellationToken = default,
        EntityStatus? status = null)
    {
        return await _db.VerificationCodes
            .WithStatusFilter(status)
            .Where(x => x.UserId == userId)
            .OrderByDescending(x => x.CreatedAt)
            .FirstOrDefaultAsync(cancellationToken);
    }
}
