using Microsoft.EntityFrameworkCore;
using ShagOxServer.Application.Interfaces.Repositories.Verifications.VerificationCodes;
using ShagOxServer.Domain.Entities.Verifications;
using ShagOxServer.Infrastructure.Persistence.DbContexts;
using ShagOxServer.Infrastructure.Persistence.Repositories.Base;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Verifications.VerificationCodes;
public class VerificationCodeExistsRepository
    : ExistsRepository<VerificationCode>,
      IVerificationCodeExistsRepository
{
    public VerificationCodeExistsRepository(AppDbContext db)
        : base(db)
    { }


    public async Task<bool> ExistsByUserIdAsync(int userId)
    {
        return await _db.VerificationCodes
            .AnyAsync(x => x.UserId == userId);
    }

    public async Task<bool> ExistsActiveByUserIdAsync(int userId)
    {
        return await _db.VerificationCodes
            .AnyAsync(x =>
                x.UserId == userId &&
                x.UsedAt == null &&
                x.ExpiresAt > DateTime.UtcNow);
    }
}