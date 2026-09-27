using ShagOxServer.Application.DTOs.Dictionaries.ProductTypes;
using ShagOxServer.Application.Interfaces.Repositories.Dictionaries.ProductTypes;
using ShagOxServer.Application.Interfaces.Services.Dictionaries.ProductTypes.Query;
using ShagOxServer.Application.Services.Base;
using ShagOxServer.Application.Services.Dictionaries.ProductTypes.Mapping;
using ShagOxServer.Domain.Entities.Dictionaries;
using ShagOxServer.Domain.Filters.Dictionaries.ProductTypes;

namespace ShagOxServer.Application.Services.Dictionaries.ProductTypes.Query;
public class ProductTypeQueryService 
    : BaseQueryService<
        ProductTypeDto,
        ProductType,
        ProductTypeSearchFilter
        >,
    IProductTypeQueryService
{
    public ProductTypeQueryService(
        IProductTypeQueryRepository productTypeQueryRepository
    )
        : base(productTypeQueryRepository)
    {
    }


    protected override ProductTypeDto ApplyMapper(
        ProductType entity)
    {
        return ProductTypeMapper.ToDto(entity);
    }
}