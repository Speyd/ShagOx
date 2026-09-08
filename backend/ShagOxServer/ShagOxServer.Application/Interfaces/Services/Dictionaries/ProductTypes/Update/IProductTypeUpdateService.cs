using ShagOxServer.Application.DTOs.Base.Responses;
using ShagOxServer.Application.DTOs.Dictionaries.ProductTypes.Update;
using ShagOxServer.Application.Interfaces.Services.Base;

namespace ShagOxServer.Application.Interfaces.Services.Dictionaries.ProductTypes.Update;
public interface IProductTypeUpdateService
    : IUpdateService<UpdateResponse, ProductTypeUpdateRequest>
{
}