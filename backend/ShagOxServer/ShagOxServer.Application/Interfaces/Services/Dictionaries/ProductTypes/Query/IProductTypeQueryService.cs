using ShagOxServer.Application.DTOs.Dictionaries.ProductTypes;
using ShagOxServer.Application.Interfaces.Services.Base;
using ShagOxServer.Application.Interfaces.Services.Base.Translations;
using ShagOxServer.Domain.Entities.Dictionaries;
using ShagOxServer.Domain.Filters.Dictionaries.ProductTypes;

namespace ShagOxServer.Application.Interfaces.Services.Dictionaries.ProductTypes.Query;
public interface IProductTypeQueryService
    : ITranslatableQueryService<ProductTypeDto,
        ProductType,
        ProductTypeSearchFilter>
{
}