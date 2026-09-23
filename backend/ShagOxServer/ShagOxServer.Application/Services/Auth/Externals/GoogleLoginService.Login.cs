using Google.Apis.Auth;
using Microsoft.Extensions.Logging;
using ShagOxServer.Application.DTOs.Auth.Externals.Google;
using ShagOxServer.Application.DTOs.Auth.Login;
using ShagOxServer.Application.Interfaces.Services.Auth.Externals;
using ShagOxServer.Domain.Entities.Advertisements;
using ShagOxServer.SharedKernel.Abstractions.Results;
using System.Net.Http.Json;

namespace ShagOxServer.Application.Services.Auth.Externals;
public partial class GoogleLoginService
    : IGoogleLoginService
{
    public async Task<Result<LoginResponse>> LoginAsync(
        GoogleLoginRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.Code))
        {
            return Result<LoginResponse>
                .Fail("Google authorization code is empty.");
        }

        var clientId = _googleSettings.ClientId;
        var clientSecret = _googleSettings.ClientSecret;

        if (string.IsNullOrWhiteSpace(clientId) ||
            string.IsNullOrWhiteSpace(clientSecret))
        {
            return Result<LoginResponse>
                .InternalServer("Google OAuth is not configured.");
        }

        var tokenResult = await ExchangeCodeAsync(
            request.Code,
            clientId,
            clientSecret);

        if (!tokenResult.IsSuccess ||
            tokenResult.Value is null)
        {
            return Result<LoginResponse>
                .Fail(tokenResult.Error);
        }

        var payloadResult = await ValidateIdTokenAsync(
            tokenResult.Value.IdToken!,
            clientId);

        if (!payloadResult.IsSuccess)
        {
            return Result<LoginResponse>
                .Fail(payloadResult.Error);
        }

        var result = await AuthenticateAsync(payloadResult.Value!);

        if (!result.IsSuccess)
        {
            return Result<LoginResponse>
                .Unauthorized(result.Error);
        }

        if (result.Value?.Token is null)
        {
            return Result<LoginResponse>
                .InternalServer();
        }

        return result;
    }

    private async Task<Result<GoogleTokenResponse>> ExchangeCodeAsync(
        string code,
        string clientId,
        string clientSecret)
    {
        using var httpClient = new HttpClient();

        var response = await httpClient.PostAsync(
            _googleSettings.TokenLink,
            new FormUrlEncodedContent(new Dictionary<string, string>
            {
                ["code"] = code,
                ["client_id"] = clientId,
                ["client_secret"] = clientSecret,
                ["redirect_uri"] = _googleSettings.RedirectUri,
                ["grant_type"] = _googleSettings.GrantType
            }));

        if (!response.IsSuccessStatusCode)
        {
            var error = await response.Content.ReadAsStringAsync();

            _logger.LogError(
                "Google token exchange failed. StatusCode: {StatusCode}, Error: {Error}",
                response.StatusCode,
                error);

            return Result<GoogleTokenResponse>
                .Fail("Google token exchange failed.");
        }

        var tokens = await response.Content
            .ReadFromJsonAsync<GoogleTokenResponse>();

        if (tokens is null ||
            string.IsNullOrWhiteSpace(tokens.IdToken))
        {
            return Result<GoogleTokenResponse>
                .Fail("Google did not return an ID token.");
        }

        _logger.LogInformation(
            "Google token exchange completed successfully.");

        return Result<GoogleTokenResponse>
            .Success(tokens);
    }

    private async Task<Result<GoogleJsonWebSignature.Payload>> ValidateIdTokenAsync(
        string idToken,
        string clientId)
    {
        try
        {
            var settings = new GoogleJsonWebSignature.ValidationSettings
            {
                Audience = new[]
                {
                clientId
            }
            };

            var payload = await GoogleJsonWebSignature.ValidateAsync(
                idToken,
                settings);

            return Result<GoogleJsonWebSignature.Payload>
                .Success(payload);
        }
        catch (InvalidJwtException ex)
        {
            _logger.LogError(
                ex,
                "Invalid Jwt. ClientId: {ClientId}",
                clientId);

            return Result<GoogleJsonWebSignature.Payload>
                .Fail("Invalid Google ID token.");
        }
    }
}