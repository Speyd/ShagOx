using ShagOxServer.Application.Interfaces.Repositories.Base.Query;
using ShagOxServer.Domain.Entities.Verifications;
using ShagOxServer.Domain.Filters.Verifications.VerificationCodes;

namespace ShagOxServer.Application.Interfaces.Repositories.Verifications.VerificationCodes;
public interface IVerificationCodeQueryRepository
    : ISearchRepository<VerificationCode, 
        VerificationCodeSearchFilter>
{
    Task<VerificationCode?> GetActiveByUserIdAsync(
        long userId);

    Task<VerificationCode?> GetLatestByUserIdAsync(
        long userId);
}
