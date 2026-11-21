using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShagOxServer.Api.Controllers.Base.Query;
using ShagOxServer.Application.DTOs.Specification.Pictures.CategoryImages.Query;
using ShagOxServer.Application.Interfaces.Services.Common.Context;
using ShagOxServer.Application.Interfaces.Services.Specification.Pictures.CategoryImages.Query;
using ShagOxServer.Domain.Filters.Specification.Pictures.CategoryImages;

namespace ShagOxServer.Api.Controllers.Specification.Pictures.CategoryImages.Admin;

[ApiController]
[Route("api/admin/category-images")]
[Authorize(Roles = "Admin")]
public class CategoryImageAdminQueriesController
    : BaseAdminQueryController<CategoryImageDto,
        CategoryImageSearchFilter>
{
    public CategoryImageAdminQueriesController(
        ICategoryImageQueryService queryService,
        IUserContext userContext
    )
        : base(queryService, userContext)
    {
    }
}
