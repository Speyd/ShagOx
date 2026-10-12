using ShagOxServer.Application.Interfaces.Repositories.Verifications.VerificationCodes;
using ShagOxServer.Domain.Entities.Verifications;
using ShagOxServer.Domain.Filters.Verifications.VerificationCodes;
using ShagOxServer.Infrastructure.Persistence.Repositories.Base.Query;
using ShagOxServer.Infrastructure.Persistence.Repositories.Verifications.VerificationCodes.Extensions;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Verifications.VerificationCodes.Query;
public partial class VerificationCodeQueryRepository
    : SearchRepository<VerificationCode, VerificationCodeSearchFilter>,
      IVerificationCodeQueryRepository
{
    protected override IQueryable<VerificationCode> ApplyFilter(
       IQueryable<VerificationCode> query,
       VerificationCodeSearchFilter filter)
    {
        return query.Filter(filter);
    }
}
