using ShagOxServer.Application.Resources.Verifications.TwoFactor.Enable;

namespace ShagOxServer.Application.Services.Verifications.Sending.Email.Processing.Models;

public static partial class EmailViewModelFactory
{
    public static dynamic CreateEnableTwoFactor(
        string code,
        int expirationMinutes)
    {
        return new
        {
            Code = code,

            Title = EnableTwoFactorResources.Title,
            SecurityMessage = EnableTwoFactorResources.SecurityMessage,
            Message = EnableTwoFactorResources.Message,
            Heading = EnableTwoFactorResources.Heading,
            Footer = EnableTwoFactorResources.Footer,
            ExpirationMessage = string.Format(EnableTwoFactorResources.ExpirationMessage,
                expirationMinutes),
            CodeInstruction = EnableTwoFactorResources.CodeInstruction,
        };
    }
}
