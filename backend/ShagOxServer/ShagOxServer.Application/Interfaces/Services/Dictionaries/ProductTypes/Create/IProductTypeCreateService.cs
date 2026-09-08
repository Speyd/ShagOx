using ShagOxServer.Application.DTOs.Base.Responses;
using ShagOxServer.Application.DTOs.Dictionaries.ProductTypes.Create;
using ShagOxServer.Application.Interfaces.Services.Base;

namespace ShagOxServer.Application.Interfaces.Services.Dictionaries.ProductTypes.Create;
public interface IProductTypeCreateService
    : ICreateService<
        CreateResponse,
        ProductTypeCreateRequest
        >
{
}