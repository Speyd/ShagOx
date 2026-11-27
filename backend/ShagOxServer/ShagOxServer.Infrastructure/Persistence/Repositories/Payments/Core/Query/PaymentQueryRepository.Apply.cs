using ShagOxServer.Application.Interfaces.Repositories.Payments.Core;
using ShagOxServer.Domain.Base.Enum;
using ShagOxServer.Domain.Entities.Payments;
using ShagOxServer.Infrastructure.Persistence.Repositories.Base.Query;
using ShagOxServer.Infrastructure.Persistence.Repositories.Payments.Core.Extensions;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Payments.Core.Query;
public partial class PaymentQueryRepository
    : QueryRepository<Payment>,
      IPaymentQueryRepository
{
    protected override IQueryable<Payment> ApplyIncludes(
        IQueryable<Payment> query,
        EntityStatus? status = null)
    {
        return query.WithIncludes(status);
    }
}
