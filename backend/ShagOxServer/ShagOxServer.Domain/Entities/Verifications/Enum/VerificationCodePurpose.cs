namespace ShagOxServer.Domain.Entities.Verifications.Enum;
public enum VerificationCodePurpose
{
    RegistrationEmail = 0,
    RegistrationPhone = 1,

    ChangeEmail = 2,
    ChangePhone = 3,


    ChangePasswordEmail = 4,
    ChangePasswordPhone = 5,

    ResetPasswordEmail = 6,
    ResetPasswordPhone = 7
}