using ShagOxServer.Application.DTOs.Base.Responses;
using ShagOxServer.Application.DTOs.Specification.Currencies.Update;
using ShagOxServer.Application.Interfaces.Services.Base;

namespace ShagOxServer.Application.Interfaces.Services.Specification.Currencies.Update;
public interface ICurrencyUpdateService
    : IUpdateService<UpdateResponse, CurrencyUpdateRequest>
{
}