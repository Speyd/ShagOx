using ShagOxServer.Application.Resources.Verifications.Email;

namespace ShagOxServer.Application.Services.Verifications.Sending.Email.Processing.Models;
public static partial class EmailViewModelFactory
{
    public static dynamic CreateVerificationEmail(
        string code,
        int expirationMinutes)
    {
        return new
        {
            Code = code,

            Title = EmailVerificationEmailResources.Title,
            SecurityMessage = EmailVerificationEmailResources.SecurityMessage,
            Message = EmailVerificationEmailResources.Message,
            Heading = EmailVerificationEmailResources.Heading,
            Footer = EmailVerificationEmailResources.Footer,
            ExpirationMessage = string.Format(EmailVerificationEmailResources.ExpirationMessage,
                expirationMinutes),
            CodeInstruction = EmailVerificationEmailResources.CodeInstruction,
        };
    }
}
