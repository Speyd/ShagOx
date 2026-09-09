using ShagOxServer.Application.DTOs.Dictionaries.ProductTypes.Create;
using ShagOxServer.Domain.Entities.Dictionaries;

namespace ShagOxServer.Application.Services.Dictionaries.ProductTypes.Create;
public static class ProductTypeCreater
{
    public static ProductType Create(
        ProductTypeCreateRequest request)
    {
        return new ProductType
        {
            Code = request.Code,
            Description = request.Description
        };
    }
}