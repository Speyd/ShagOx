using Microsoft.EntityFrameworkCore;
using ShagOxServer.Application.Interfaces.Repositories.Verifications.VerificationCodes;
using ShagOxServer.Domain.Base.Enum;
using ShagOxServer.Domain.Entities.Verifications;
using ShagOxServer.Infrastructure.Persistence.DbContexts.Primary;
using ShagOxServer.Infrastructure.Persistence.Repositories.Base.Exists;
using ShagOxServer.Infrastructure.Persistence.Repositories.Base.Extensions;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Verifications.VerificationCodes;
public class VerificationCodeExistsRepository
    : ExistsRepository<VerificationCode>,
      IVerificationCodeExistsRepository
{
    public VerificationCodeExistsRepository(
        AppDbContext db)
        : base(db)
    { }


    public async Task<bool> ExistsByUserIdAsync(
        long userId,
        CancellationToken cancellationToken = default,
        EntityStatus? status = null)
    {
        return await _db.VerificationCodes
            .WithStatusFilter(status)
            .AnyAsync(x => x.UserId == userId,
                cancellationToken);
    }

    public async Task<bool> ExistsActiveByUserIdAsync(
        long userId,
        CancellationToken cancellationToken = default,
        EntityStatus? status = null)
    {
        return await _db.VerificationCodes
            .WithStatusFilter(status)
            .AnyAsync(x =>
                x.UserId == userId &&
                x.UsedAt == null &&
                x.ExpiresAt > DateTime.UtcNow,
                cancellationToken);
    }
}
