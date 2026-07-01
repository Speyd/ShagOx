using Microsoft.AspNetCore.Mvc;
using ShagOxServer.Application.Common.Results.Extensions;
using ShagOxServer.Application.Interfaces.Specification.Images.Query;

namespace ShagOxServer.Api.Controllers.Images;

[ApiController]
[Route("api/images")]
public class ImageQueriesController : ControllerBase
{
    private readonly IImageQueryService _queryService;

    public ImageQueriesController(
        IImageQueryService queryService)
    {
        _queryService = queryService;
    }


    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var result = await _queryService.GetByIdAsync(id);
        return result.ToActionResult();
    }
}
