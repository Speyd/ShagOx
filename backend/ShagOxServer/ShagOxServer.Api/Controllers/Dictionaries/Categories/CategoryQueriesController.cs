using Microsoft.AspNetCore.Mvc;
using ShagOxServer.Api.Controllers.Api;
using ShagOxServer.Api.Controllers.Base;
using ShagOxServer.Application.DTOs.Dictionaries.Categories.Query;
using ShagOxServer.Application.Interfaces.Services.Dictionaries.Categories.Query;
using ShagOxServer.Domain.Filters.Dictionaries.Categories;

namespace ShagOxServer.Api.Controllers.Dictionaries.Categories;

[ApiController]
[Route("api/categories")]
public class CategoryQueriesController 
    : BaseQueryController<CategoryDto,
        CategorySearchFilter>
{
    public CategoryQueriesController(
        ICategoryQueryService queryService
    )
        : base(queryService)
    {
    }
}
