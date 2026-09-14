using ShagOxServer.Application.Interfaces.Repositories.Base;
using ShagOxServer.Domain.Entities.Verifications;

namespace ShagOxServer.Application.Interfaces.Repositories.Verifications.VerificationCodes;
public interface IVerificationCodeExistsRepository
    : IExistsRepository<VerificationCode>
{
    Task<bool> ExistsByUserIdAsync(
        int userId);

    Task<bool> ExistsActiveByUserIdAsync(
        int userId);
}