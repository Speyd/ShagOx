using Microsoft.EntityFrameworkCore;
using ShagOxServer.Application.Interfaces.Repositories.Verifications.VerificationCodes;
using ShagOxServer.Domain.Entities.Verifications;
using ShagOxServer.Domain.Filters.Verifications.VerificationCodes;
using ShagOxServer.Infrastructure.Persistence.DbContexts;
using ShagOxServer.Infrastructure.Persistence.Repositories.Base;
using ShagOxServer.Infrastructure.Persistence.Repositories.Verifications.VerificationCodes.Extensions;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Verifications.VerificationCodes.Query;
public partial class VerificationCodeQueryRepository
    : QueryRepository<VerificationCode, VerificationCodeSearchFilter>,
      IVerificationCodeQueryRepository
{
    public VerificationCodeQueryRepository(AppDbContext db)
        : base(db)
    { }


    public async Task<VerificationCode?> GetActiveByUserIdAsync(
        long userId)
    {
        return await _db.VerificationCodes
            .FirstOrDefaultAsync(x =>
                x.UserId == userId &&
                x.UsedAt == null &&
                x.ExpiresAt > DateTime.UtcNow &&
                x.InvalidatedAt == null);
    }

    public async Task<VerificationCode?> GetLatestByUserIdAsync(
        long userId)
    {
        return await _db.VerificationCodes
            .Where(x => x.UserId == userId)
            .OrderByDescending(x => x.CreatedAt)
            .FirstOrDefaultAsync();
    }
}