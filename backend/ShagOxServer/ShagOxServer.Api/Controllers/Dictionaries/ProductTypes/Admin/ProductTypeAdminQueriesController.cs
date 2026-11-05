using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShagOxServer.Api.Controllers.Base.Query;
using ShagOxServer.Application.DTOs.Dictionaries.ProductTypes.Query;
using ShagOxServer.Application.Interfaces.Services.Common.Context;
using ShagOxServer.Application.Interfaces.Services.Dictionaries.ProductTypes.Query;
using ShagOxServer.Domain.Filters.Dictionaries.ProductTypes;

namespace ShagOxServer.Api.Controllers.Dictionaries.ProductTypes.Admin;

[ApiController]
[Route("api/admin/product-types")]
[Authorize(Roles = "Admin")]
public class ProductTypeAdminQueriesController
    : BaseAdminQueryController<ProductTypeDto,
       ProductTypeSearchFilter>
{
    public ProductTypeAdminQueriesController(
        IProductTypeQueryService queryService,
        IUserContext userContext
    )
        : base(queryService, userContext)
    {
    }
}
