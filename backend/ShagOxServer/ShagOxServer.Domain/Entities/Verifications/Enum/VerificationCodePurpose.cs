namespace ShagOxServer.Domain.Entities.Verifications.Enum;
public enum VerificationCodePurpose
{
    RegistrationEmail = 0,
    RegistrationPhone = 1,

    ChangeEmail = 2,
    ChangePhone = 3,

    ResetPassword = 4
}