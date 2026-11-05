using ShagOxServer.Application.DTOs.Base.Responses;
using ShagOxServer.Application.Interfaces.Services.Base.Delete;
using ShagOxServer.Domain.Entities.Specification;

namespace ShagOxServer.Application.Interfaces.Services.Specification.Currencies.Delete;
public interface ICurrencyDeleteService
    : IDeleteService<DeleteResponse>,
    ISoftDeleteService<Currency, DeleteResponse>
{
}
