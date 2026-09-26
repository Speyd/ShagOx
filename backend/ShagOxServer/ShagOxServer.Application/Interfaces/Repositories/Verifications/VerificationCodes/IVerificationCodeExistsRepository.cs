using ShagOxServer.Application.Interfaces.Repositories.Base;
using ShagOxServer.Domain.Entities.Verifications;

namespace ShagOxServer.Application.Interfaces.Repositories.Verifications.VerificationCodes;
public interface IVerificationCodeExistsRepository
    : IExistsRepository<VerificationCode>
{
    Task<bool> ExistsByUserIdAsync(
        long userId);

    Task<bool> ExistsActiveByUserIdAsync(
        long userId);
}