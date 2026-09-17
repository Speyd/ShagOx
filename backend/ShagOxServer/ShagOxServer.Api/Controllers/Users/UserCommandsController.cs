using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShagOxServer.Application.DTOs.Auth.Users.Contacts.Emails;
using ShagOxServer.Application.DTOs.Auth.Users.Contacts.Passwords;
using ShagOxServer.Application.DTOs.Auth.Users.Contacts.Phones;
using ShagOxServer.Application.DTOs.Auth.Users.Core.Update;
using ShagOxServer.Application.Interfaces.Services.Auth.Users.Contacts.Emails;
using ShagOxServer.Application.Interfaces.Services.Auth.Users.Contacts.Passwords;
using ShagOxServer.Application.Interfaces.Services.Auth.Users.Contacts.Phones;
using ShagOxServer.Application.Interfaces.Services.Auth.Users.Core.Update;
using ShagOxServer.SharedKernel.Abstractions.Results.Extensions;

namespace ShagOxServer.Api.Controllers.Users;

[ApiController]
[Route("api/users")]
public class UserCommandsController
    : ApiController
{
    private readonly IUserUpdateService _updateService;
    private readonly IChangeEmailService _emailService;
    private readonly IChangePhoneService _phoneService;
    private readonly IChangePasswordService _passwordService;
    private readonly IResetPasswordService _resetPasswordService;


    public UserCommandsController(
        IUserUpdateService updateService,
        IChangeEmailService emailService,
        IChangePhoneService phoneService,
        IChangePasswordService passwordService,
        IResetPasswordService resetPasswordService)
    {
        _updateService = updateService;
        _emailService = emailService;
        _phoneService = phoneService;
        _passwordService = passwordService;
        _resetPasswordService = resetPasswordService;
    }

    [Authorize]
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

    [Authorize]
    [HttpPut("email")]
    public async Task<IActionResult> ChangeEmail(
        [FromBody] ChangeEmailRequest request)
    {
        var result = await _emailService
            .ChangeEmail(UserId, request);

        return result.ToActionResult();
    }

    [Authorize]
    [HttpPut("phone")]
    public async Task<IActionResult> ChangePhone(
        [FromBody] ChangePhoneRequest request)
    {
        var result = await _phoneService
            .ChangePhone(UserId, request);

        return result.ToActionResult();
    }

    [Authorize]
    [HttpPut("password")]
    public async Task<IActionResult> ChangePassword(
        [FromBody] ChangePasswordRequest request)
    {
        var result = await _passwordService
            .ChangePassword(UserId, request);

        return result.ToActionResult();
    }

    [HttpPut("reset-password")]
    public async Task<IActionResult> ResetPassword(
        [FromBody] ResetPasswordRequest request)
    {
        var result = await _resetPasswordService
            .ResetPassword(request);

        return result.ToActionResult();
    }
}