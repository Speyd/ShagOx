using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShagOxServer.Application.DTOs.Auth.Users.Contacts.Emails;
using ShagOxServer.Application.DTOs.Auth.Users.Core.Update;
using ShagOxServer.Application.Interfaces.Services.Auth.Users.Contacts;
using ShagOxServer.Application.Interfaces.Services.Auth.Users.Core.Update;
using ShagOxServer.SharedKernel.Abstractions.Results.Extensions;

namespace ShagOxServer.Api.Controllers.Users;

[ApiController]
[Route("api/users")]
[Authorize]
public class UserCommandsController
    : ApiController
{
    private readonly IUserUpdateService _updateService;
    private readonly IChangeEmailService _emailService;


    public UserCommandsController(
        IUserUpdateService updateService,
        IChangeEmailService emailService)
    {
        _updateService = updateService;
        _emailService = emailService;
    }


    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(
        [FromRoute] int id,
        [FromForm] UserUpdateRequest request)
    {
        if (UserId != id)
            return Forbid();

        var result = await _updateService
            .UpdateAsync(id, request);

        return result.ToActionResult();
    }


    [HttpPut("email")]
    public async Task<IActionResult> UpdateEmail(
        [FromBody] ChangeEmailRequest request)
    {
        var result = await _emailService
            .ChangeEmail(44, request);

        return result.ToActionResult();
    }
}