using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShagOxServer.Application.DTOs.Auth.Login;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Interfaces.Services.Auth.Cookies;
public interface IAuthCookieService
{
    Task<IActionResult> SetAccessToken(
        Result<LoginResponse> response);

    void DeleteAccessToken(
       HttpResponse response);
}