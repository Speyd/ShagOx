using ShagOxServer.Application.Interfaces.Repositories.Base;
using ShagOxServer.Domain.Entities.Verifications;
using ShagOxServer.Domain.Filters.Verifications.VerificationCodes;

namespace ShagOxServer.Application.Interfaces.Repositories.Verifications.VerificationCodes;
public interface IVerificationCodeQueryRepository
    : IQueryRepository<VerificationCode, 
        VerificationCodeSearchFilter>
{
    Task<VerificationCode?> GetActiveByUserIdAsync(
        long userId);

    Task<VerificationCode?> GetLatestByUserIdAsync(
        long userId);
}