using Google.Apis.Auth;
using Microsoft.Extensions.Logging;
using ShagOxServer.Application.DTOs.Auth.Externals.Google;
using ShagOxServer.Application.DTOs.Auth.Login;
using ShagOxServer.Application.Interfaces.Services.Auth.Externals;
using ShagOxServer.Application.Resources.Auth.External.Google;
using ShagOxServer.SharedKernel.Abstractions.Results;
using System.Net.Http.Json;

namespace ShagOxServer.Application.Services.Auth.Login.Externals;
public partial class GoogleLoginService
    : IGoogleLoginService
{
    public async Task<Result<LoginResponse>> LoginAsync(
        GoogleLoginRequest request)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(request.Code))
            {
                return Result<LoginResponse>.Fail(
                    GoogleAuthResources.GoogleAuthorizationCodeEmpty);
            }

            var clientId = _googleOptions.ClientId;
            var clientSecret = _googleOptions.ClientSecret;

            if (string.IsNullOrWhiteSpace(clientId) ||
                string.IsNullOrWhiteSpace(clientSecret))
            {
                return Result<LoginResponse>.InternalServer(
                    GoogleAuthResources.GoogleOAuthNotConfigured);
            }

            var tokenResult = await ExchangeCodeAsync(
                request.Code,
                clientId,
                clientSecret);

            if (!tokenResult.IsSuccess || tokenResult.Value is null)
            {
                return Result<LoginResponse>.Fail(
                    tokenResult.Error);
            }

            if (string.IsNullOrWhiteSpace(tokenResult.Value.IdToken))
            {
                return Result<LoginResponse>.Fail(
                    GoogleAuthResources.GoogleIdTokenMissing);
            }

            var payloadResult = await ValidateIdTokenAsync(
                tokenResult.Value.IdToken,
                clientId);

            if (!payloadResult.IsSuccess || payloadResult.Value is null)
            {
                return Result<LoginResponse>.Fail(
                    payloadResult.Error);
            }

            return await AuthenticateAsync(payloadResult.Value);
        }
        catch (Exception ex)
        {
            _logger.LogError(
                ex,
                "Google login operation failed.");

            return Result<LoginResponse>.Fail(
                GoogleAuthResources.GoogleAuthenticationFailed);
        }
    }


    private async Task<Result<GoogleTokenResponse>> ExchangeCodeAsync(
        string code,
        string clientId,
        string clientSecret)
    {
        var client = _httpClientFactory.CreateClient();

        using var response = await client.PostAsync(
            _googleOptions.TokenLink,
            new FormUrlEncodedContent(
                new Dictionary<string, string>
                {
                    ["code"] = code,
                    ["client_id"] = clientId,
                    ["client_secret"] = clientSecret,
                    ["redirect_uri"] = _googleOptions.RedirectUri,
                    ["grant_type"] = _googleOptions.GrantType
                }));

        if (!response.IsSuccessStatusCode)
        {
            _logger.LogWarning(
                "Google token exchange failed. StatusCode: {StatusCode}",
                response.StatusCode);

            return Result<GoogleTokenResponse>.Fail(
                GoogleAuthResources.GoogleTokenExchangeFailed);
        }

        var tokens = await response.Content
            .ReadFromJsonAsync<GoogleTokenResponse>();

        if (tokens is null ||
            string.IsNullOrWhiteSpace(tokens.IdToken))
        {
            return Result<GoogleTokenResponse>.Fail(
                GoogleAuthResources.GoogleIdTokenMissing);
        }

        return Result<GoogleTokenResponse>.Success(tokens);
    }



    private async Task<Result<GoogleJsonWebSignature.Payload>>
        ValidateIdTokenAsync(
            string idToken,
            string clientId)
    {
        try
        {
            var settings =
                new GoogleJsonWebSignature.ValidationSettings
                {
                    Audience = new[] { clientId }
                };

            var payload = await GoogleJsonWebSignature.ValidateAsync(
                idToken,
                settings);

            return Result<GoogleJsonWebSignature.Payload>
                .Success(payload);
        }
        catch (InvalidJwtException ex)
        {
            _logger.LogWarning(
                ex,
                "Google ID token validation failed.");

            return Result<GoogleJsonWebSignature.Payload>.Fail(
                GoogleAuthResources.GoogleInvalidIdToken);
        }
    }

}
