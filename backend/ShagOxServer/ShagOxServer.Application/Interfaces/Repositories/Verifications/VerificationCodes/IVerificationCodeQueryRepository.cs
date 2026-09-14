using ShagOxServer.Application.Interfaces.Repositories.Base;
using ShagOxServer.Domain.Entities.Verifications;

namespace ShagOxServer.Application.Interfaces.Repositories.Verifications.VerificationCodes;
public interface IVerificationCodeQueryRepository
    : IQueryRepository<VerificationCode>
{
    Task<VerificationCode?> GetActiveByUserIdAsync(
       int userId);

    Task<VerificationCode?> GetLatestByUserIdAsync(
        int userId);
}