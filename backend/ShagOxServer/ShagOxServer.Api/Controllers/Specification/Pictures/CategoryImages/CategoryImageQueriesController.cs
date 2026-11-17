using Microsoft.AspNetCore.Mvc;
using ShagOxServer.Api.Controllers.Base.Query;
using ShagOxServer.Application.DTOs.Specification.Pictures.CategoryImages.Query;
using ShagOxServer.Application.Interfaces.Services.Common.Context;
using ShagOxServer.Application.Interfaces.Services.Specification.Pictures.CategoryImages.Query;
using ShagOxServer.Domain.Filters.Specification.Pictures.CategoryImages;

namespace ShagOxServer.Api.Controllers.Specification.Pictures.CategoryImages;

[ApiController]
[Route("api/category-images")]
public class CategoryImageQueriesController
    : BaseQueryController<CategoryImageDto,
        CategoryImageSearchFilter>
{
    public CategoryImageQueriesController(
        ICategoryImageQueryService queryService,
        IUserContext userContext
    )
        : base(queryService, userContext)
    {
    }
}
