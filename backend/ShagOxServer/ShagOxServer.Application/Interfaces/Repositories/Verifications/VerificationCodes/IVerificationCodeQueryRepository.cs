using ShagOxServer.Application.Interfaces.Repositories.Base.Query;
using ShagOxServer.Domain.Base.Enum;
using ShagOxServer.Domain.Entities.Verifications;
using ShagOxServer.Domain.Filters.Verifications.VerificationCodes;

namespace ShagOxServer.Application.Interfaces.Repositories.Verifications.VerificationCodes;
public interface IVerificationCodeQueryRepository
    : ISearchRepository<VerificationCode, 
        VerificationCodeSearchFilter>
{
    Task<VerificationCode?> GetActiveByUserIdAsync(
        long userId,
        CancellationToken cancellationToken = default,
        EntityStatus? status = null);

    Task<VerificationCode?> GetLatestByUserIdAsync(
        long userId,
        CancellationToken cancellationToken = default,
        EntityStatus? status = null);
}
