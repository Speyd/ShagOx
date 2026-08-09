using ShagOxServer.Application.DTOs.Common.Responses;
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