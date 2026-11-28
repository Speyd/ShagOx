using ShagOxServer.Application.Resources.Verifications.Password.Change;
using ShagOxServer.Application.Resources.Verifications.Password.Reset;

namespace ShagOxServer.Application.Services.Verifications.Sending.Email.Processing.Models;
public static partial class EmailViewModelFactory
{
    public static dynamic ChangePasswordConfirmation(
        string code,
        int expirationMinutes)
    {
        return new
        {
            Code = code,

            Title = ChangePasswordEmail.Title,
            SecurityMessage = ChangePasswordEmail.SecurityMessage,
            Message = ChangePasswordEmail.Message,
            Heading = ChangePasswordEmail.Heading,
            Footer = ChangePasswordEmail.Footer,
            ExpirationMessage = string.Format(ChangePasswordEmail.ExpirationMessage,
                expirationMinutes),
            CodeInstruction = ChangePasswordEmail.CodeInstruction,
        };
    }

    public static dynamic ResetPasswordConfirmation(
        string code,
        int expirationMinutes)
    {
        return new
        {
            Code = code,

            Title = ResetPasswordEmail.Title,
            SecurityMessage = ResetPasswordEmail.SecurityMessage,
            Message = ResetPasswordEmail.Message,
            Heading = ResetPasswordEmail.Heading,
            Footer = ResetPasswordEmail.Footer,
            ExpirationMessage = string.Format(ResetPasswordEmail.ExpirationMessage,
                expirationMinutes),
            CodeInstruction = ResetPasswordEmail.CodeInstruction,
        };
    }

}
