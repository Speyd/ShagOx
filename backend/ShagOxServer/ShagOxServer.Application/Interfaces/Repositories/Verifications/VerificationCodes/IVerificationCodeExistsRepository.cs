using ShagOxServer.Application.Interfaces.Repositories.Base;
using ShagOxServer.Domain.Base.Enum;
using ShagOxServer.Domain.Entities.Verifications;

namespace ShagOxServer.Application.Interfaces.Repositories.Verifications.VerificationCodes;
public interface IVerificationCodeExistsRepository
    : IExistsRepository<VerificationCode>
{
    Task<bool> ExistsByUserIdAsync(
        long userId,
        CancellationToken cancellationToken = default,
        EntityStatus? status = null);

    Task<bool> ExistsActiveByUserIdAsync(
        long userId,
        CancellationToken cancellationToken = default,
        EntityStatus? status = null);
}
