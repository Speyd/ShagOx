using Microsoft.EntityFrameworkCore;
using ShagOxServer.Application.Interfaces.Repositories.Verifications.VerificationCodes;
using ShagOxServer.Domain.Entities.Verifications;
using ShagOxServer.Infrastructure.Persistence.DbContexts.Primary;
using ShagOxServer.Infrastructure.Persistence.DbContexts.Replica;
using ShagOxServer.Infrastructure.Persistence.Repositories.Base;

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
        CancellationToken cancellationToken = default)
    {
        return await _db.VerificationCodes
            .AnyAsync(x => x.UserId == userId,
                cancellationToken);
    }

    public async Task<bool> ExistsActiveByUserIdAsync(
        long userId,
        CancellationToken cancellationToken = default)
    {
        return await _db.VerificationCodes
            .AnyAsync(x =>
                x.UserId == userId &&
                x.UsedAt == null &&
                x.ExpiresAt > DateTime.UtcNow,
                cancellationToken);
    }
}
