using ShagOxServer.Application.Resources.Verifications.TwoFactor.Disable;
using ShagOxServer.Application.Resources.Verifications.TwoFactor.Enable;
using ShagOxServer.Application.Resources.Verifications.TwoFactor.Initiate;

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

    public static dynamic CreateDisableTwoFactor(
        string code,
        int expirationMinutes)
    {
        return new
        {
            Code = code,

            Title = DisableTwoFactorResources.Title,
            SecurityMessage = DisableTwoFactorResources.SecurityMessage,
            Message = DisableTwoFactorResources.Message,
            Heading = DisableTwoFactorResources.Heading,
            Footer = DisableTwoFactorResources.Footer,
            ExpirationMessage = string.Format(DisableTwoFactorResources.ExpirationMessage,
                expirationMinutes),
            CodeInstruction = DisableTwoFactorResources.CodeInstruction,
        };
    }

    public static dynamic CreateInitiateTwoFactor(
        string code,
        int expirationMinutes)
    {
        return new
        {
            Code = code,

            Title = InitiateTwoFactorResources.Title,
            SecurityMessage = InitiateTwoFactorResources.SecurityMessage,
            Message = InitiateTwoFactorResources.Message,
            Heading = InitiateTwoFactorResources.Heading,
            Footer = InitiateTwoFactorResources.Footer,
            ExpirationMessage = string.Format(InitiateTwoFactorResources.ExpirationMessage,
                expirationMinutes),
            CodeInstruction = InitiateTwoFactorResources.CodeInstruction,
        };
    }
}
