using ShagOxServer.Application.DTOs.Verifications;
using ShagOxServer.Application.Interfaces.Services.Base.Query;
using ShagOxServer.Domain.Entities.Verifications;
using ShagOxServer.Domain.Filters.Verifications.VerificationCodes;

namespace ShagOxServer.Application.Interfaces.Services.Verifications.Codes;
public interface IVerificationCodeQueryService
    : IQueryService<VerificationCodeDto,
        VerificationCode,
        VerificationCodeSearchFilter>
{
}
