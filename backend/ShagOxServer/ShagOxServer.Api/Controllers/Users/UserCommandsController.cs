using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShagOxServer.Application.DTOs.Auth.Users.Contacts.Emails;
using ShagOxServer.Application.DTOs.Auth.Users.Contacts.Passwords;
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
    private readonly IChangePasswordService _passwordService;



    public UserCommandsController(
        IUserUpdateService updateService,
        IChangeEmailService emailService,
        IChangePasswordService passwordService)
    {
        _updateService = updateService;
        _emailService = emailService;
        _passwordService = passwordService;
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
            .ChangeEmail(UserId, request);

        return result.ToActionResult();
    }

    [HttpPut("password")]
    public async Task<IActionResult> UpdatePassword(
        [FromBody] ChangePasswordRequest request)
    {
        var result = await _passwordService
            .ChangePassword(UserId, request);

        return result.ToActionResult();
    }
}