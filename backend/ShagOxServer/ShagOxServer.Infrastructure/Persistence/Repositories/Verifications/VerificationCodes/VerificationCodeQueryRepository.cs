using Microsoft.EntityFrameworkCore;
using ShagOxServer.Application.Interfaces.Repositories.Location.Cities;
using ShagOxServer.Application.Interfaces.Repositories.Verifications.VerificationCodes;
using ShagOxServer.Domain.Entities.Verifications;
using ShagOxServer.Infrastructure.Persistence.DbContexts;
using ShagOxServer.Infrastructure.Persistence.Repositories.Base;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Verifications.VerificationCodes;
public class VerificationCodeQueryRepository
    : QueryRepository<VerificationCode>,
      IVerificationCodeQueryRepository
{
    public VerificationCodeQueryRepository(AppDbContext db)
        : base(db)
    { }


    public async Task<VerificationCode?> GetActiveByUserIdAsync(
        int userId)
    {
        return await _db.VerificationCodes
            .FirstOrDefaultAsync(x =>
                x.UserId == userId &&
                x.UsedAt == null &&
                x.ExpiresAt > DateTime.UtcNow);
    }

    public async Task<VerificationCode?> GetLatestByUserIdAsync(
        int userId)
    {
        return await _db.VerificationCodes
            .Where(x => x.UserId == userId)
            .OrderByDescending(x => x.CreatedAt)
            .FirstOrDefaultAsync();
    }
}