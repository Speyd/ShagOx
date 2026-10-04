using ShagOxServer.Domain.Entities.Verifications;

namespace ShagOxServer.Application.Services.Caches.Keys.Verifications;
public static class VerificationCodeCache
{
    public static readonly string Prefix =
       CacheKeys.LanguagePrefix<VerificationCode>();
}
