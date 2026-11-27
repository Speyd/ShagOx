namespace ShagOxServer.Domain.Entities.Verifications.Enum;
public enum VerificationCodePurpose
{
    None,

    RegistrationEmail,
    RegistrationPhone,

    ChangeCurrentEmail,
    ChangeNewEmail,

    ChangeCurrentPhone,
    ChangeNewPhone,

    ChangePasswordEmail,
    ChangePasswordPhone,

    ResetPasswordEmail,
    ResetPasswordPhone,

    EnableTwoFactorEmail,
    DisableTwoFactorEmail,

    EnableTwoFactorPhone,
    DisableTwoFactorPhone,

    InitiateTwoFactorEmail,
    InitiateTwoFactorPhone,
}
