using Microsoft.AspNetCore.Mvc;
using ShagOxServer.Application.Interfaces.Services.Roles.Specification.Images.Query;
using ShagOxServer.SharedKernel.Abstractions.Results.Extensions;

namespace ShagOxServer.Api.Controllers.Images;

[ApiController]
[Route("api/images")]
public class ImageQueriesController : ApiController
{
    private readonly IImageQueryService _queryService;

    public ImageQueriesController(
        IImageQueryService queryService)
    {
        _queryService = queryService;
    }


    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(
        [FromRoute] int id)
    {
        var result = await _queryService.GetByIdAsync(id);
        return result.ToActionResult();
    }
}
