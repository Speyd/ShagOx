using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShagOxServer.Application.Common.Results.Extensions;
using ShagOxServer.Application.DTOs.Specification.Images.Create;
using ShagOxServer.Application.DTOs.Specification.Images.Update;
using ShagOxServer.Application.Interfaces.Specification.Images.Create;
using ShagOxServer.Application.Interfaces.Specification.Images.Delete;
using ShagOxServer.Application.Interfaces.Specification.Images.Update;

namespace ShagOxServer.Api.Controllers.Images;

[ApiController]
[Route("api/images")]
[Authorize]
public class ImageCommandsController : ControllerBase
{
    private readonly IImageCreateService _createService;
    private readonly IImageUpdateService _updateService;
    private readonly IImageDeleteService _deleteService;

    public ImageCommandsController(
        IImageCreateService createService,
        IImageUpdateService updateService,
        IImageDeleteService deleteService
        )
    {
        _createService = createService;
        _updateService = updateService;
        _deleteService = deleteService;
    }


    [HttpPost]
    public async Task<IActionResult> Create(ImageCreateRequest request)
    {
        var result = await _createService.CreateImageAsync(request);
        return result.ToActionResult();
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, ImageUpdateRequest request)
    {
        var result = await _updateService.UpdateImageAsync(id, request);
        return result.ToActionResult();
    }


    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _deleteService.DeleteImageAsync(id);
        return result.ToActionResult();
    }
}
