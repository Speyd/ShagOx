namespace ShagOxServer.Domain.Entities.Verifications.Enum;
public enum VerificationCodePurpose
{
    None = 0,

    RegistrationEmail = 1,
    RegistrationPhone = 2,

    ChangeEmail = 3,
    ChangePhone = 4,


    ChangePasswordEmail = 5,
    ChangePasswordPhone = 6,

    ResetPasswordEmail = 7,
    ResetPasswordPhone = 8,


    EnableTwoFactorEmail = 9,
    DisableTwoFactorEmail = 10,

    EnableTwoFactorPhone = 11,
    DisableTwoFactorPhone = 12,

    InitiateTwoFactorEmail = 13,
    InitiateTwoFactorPhone = 14,
}
