using ShagOxServer.Application.Resources.Verifications.ChangeEmail.ChangeCurrency;
using ShagOxServer.Application.Resources.Verifications.ChangeEmail.CreateNew;

namespace ShagOxServer.Application.Services.Verifications.Sending.Email.Processing.Models;
public static partial class EmailViewModelFactory
{
    public static dynamic CreateNewEmailConfirmation(
        string code,
        int expirationMinutes)
    {
        return new
        {
            Code = code,

            Title = CreateNewEmailResources.Title,
            SecurityMessage = CreateNewEmailResources.SecurityMessage,
            Message = CreateNewEmailResources.Message,
            Heading = CreateNewEmailResources.Heading,
            Footer = CreateNewEmailResources.Footer,
            ExpirationMessage = string.Format(CreateNewEmailResources.ExpirationMessage,
                expirationMinutes),
            CodeInstruction = CreateNewEmailResources.CodeInstruction,
        };
    }

    public static dynamic CreateChangeApprovalEmailConfirmation(
        string code,
        int expirationMinutes,
        string cuurentEmail,
        string newEmail)
    {
        return new
        {
            Code = code,

            Title = EmailChangeApprovalResources.Title,
            SecurityMessage = EmailChangeApprovalResources.SecurityMessage,
            Message = string.Format(EmailChangeApprovalResources.Message,
                cuurentEmail),
            NewEmailLabel = string.Format(EmailChangeApprovalResources.NewEmailLabel,
                newEmail),
            Heading = EmailChangeApprovalResources.Heading,
            Footer = EmailChangeApprovalResources.Footer,
            ExpirationMessage = string.Format(EmailChangeApprovalResources.ExpirationMessage,
                expirationMinutes),
            CodeInstruction = EmailChangeApprovalResources.CodeInstruction,
        };
    }

}
