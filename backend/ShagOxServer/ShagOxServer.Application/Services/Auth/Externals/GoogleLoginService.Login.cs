using Google.Apis.Auth;
using ShagOxServer.Application.DTOs.Auth.Externals.Google;
using ShagOxServer.Application.DTOs.Auth.Login;
using ShagOxServer.Application.Interfaces.Services.Auth.Externals;
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

        using var httpClient = new HttpClient();

        var tokenResponse = await httpClient.PostAsync(
            _googleSettings.TokenLink,
            new FormUrlEncodedContent(new Dictionary<string, string>
            {
                ["code"] = request.Code,
                ["client_id"] = clientId,
                ["client_secret"] = clientSecret,
                ["redirect_uri"] = _googleSettings.RedirectUri,
                ["grant_type"] = _googleSettings.GrantType
            })
        );


        if (!tokenResponse.IsSuccessStatusCode)
        {
            //var error = await tokenResponse.Content.ReadAsStringAsync();
            return Result<LoginResponse>
                .Fail("Google token exchange failed.");
        }

        var tokens = await tokenResponse.Content
            .ReadFromJsonAsync<GoogleTokenResponse>();

        if (tokens is null ||
            string.IsNullOrWhiteSpace(tokens.IdToken))
        {
            return Result<LoginResponse>
               .Fail("Google did not return an ID token.");
        }

        GoogleJsonWebSignature.Payload payload;

        try
        {
            var settings = new GoogleJsonWebSignature.ValidationSettings
            {
                Audience = new[]
                {
                clientId
            }
            };

            payload = await GoogleJsonWebSignature.ValidateAsync(
                tokens.IdToken,
                settings
            );
        }
        catch (InvalidJwtException)
        {
            return Result<LoginResponse>
              .Fail("Invalid Google ID token.");
        }

        var result = await AuthenticateAsync(payload);

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
}