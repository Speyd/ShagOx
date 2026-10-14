using Microsoft.AspNetCore.Mvc;
using ShagOxServer.Api.Controllers.Api;
using ShagOxServer.Api.Controllers.Base;
using ShagOxServer.Application.DTOs.Dictionaries.ProductTypes.Query;
using ShagOxServer.Application.Interfaces.Services.Dictionaries.ProductTypes.Query;
using ShagOxServer.Domain.Filters.Dictionaries.ProductTypes;

namespace ShagOxServer.Api.Controllers.Dictionaries.ProductTypes;

[ApiController]
[Route("api/product-types")]
public class ProductTypeQueriesController 
    : BaseQueryController<ProductTypeDto,
       ProductTypeSearchFilter>
{
    public ProductTypeQueriesController(
        IProductTypeQueryService queryService
    )
        : base(queryService)
    {
    }
}
