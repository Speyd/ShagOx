using Microsoft.AspNetCore.WebUtilities;
using ShagOxServer.Application.Interfaces.Services.Auth.Users.UserSessions;
using System.Security.Cryptography;
using System.Text;

namespace ShagOxServer.Application.Services.Auth.Tokens.RefreshToken;
public class RefreshTokenService 
    : IRefreshTokenService
{
    public string Generate()
    {
        return WebEncoders.Base64UrlEncode(
            RandomNumberGenerator.GetBytes(64)
        );
    }

    public string Hash(string token)
    {
        var bytes = SHA256.HashData(
            Encoding.UTF8.GetBytes(token)
        );

        return Convert.ToHexString(bytes);
    }
}
