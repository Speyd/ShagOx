using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShagOxServer.Api.Controllers.Base.Query;
using ShagOxServer.Application.DTOs.Dictionaries.Categories.Query;
using ShagOxServer.Application.Interfaces.Services.Dictionaries.Categories.Query;
using ShagOxServer.Domain.Filters.Dictionaries.Categories;

namespace ShagOxServer.Api.Controllers.Dictionaries.Categories.Admin;

[ApiController]
[Route("api/admin/categories")]
[Authorize(Roles = "Admin")]
public class CategoryAdminQueriesController
    : BaseAdminQueryController<CategoryDto,
        CategorySearchFilter>
{
    public CategoryAdminQueriesController(
        ICategoryQueryService queryService
    )
        : base(queryService)
    {
    }
}
