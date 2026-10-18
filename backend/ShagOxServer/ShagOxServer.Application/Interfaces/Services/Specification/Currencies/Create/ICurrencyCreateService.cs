using ShagOxServer.Application.DTOs.Base.Responses;
using ShagOxServer.Application.DTOs.Specification.Currencies.Create;
using ShagOxServer.Application.Interfaces.Services.Base.Create;

namespace ShagOxServer.Application.Interfaces.Services.Specification.Currencies.Create;
public interface ICurrencyCreateService
    : ICreateService<
        CreateResponse,
        CurrencyCreateRequest
        >
{
}