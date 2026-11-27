using ShagOxServer.Application.DTOs.Auth.Users.Contacts.Phones;
using ShagOxServer.Domain.Entities.Verifications.Enum;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Interfaces.Services.Auth.Users.Contacts.Phones;
public interface IChangePhoneService
{
    Task<Result<bool>> ChangePhone(
        long userId,
        ChangePhoneRequest request,
        VerificationCodePurpose purpose);
}
