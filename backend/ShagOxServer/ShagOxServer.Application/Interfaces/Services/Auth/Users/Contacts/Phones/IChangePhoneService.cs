using ShagOxServer.Application.DTOs.Auth.Users.Contacts.Phones;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Interfaces.Services.Auth.Users.Contacts.Phones;
public interface IChangePhoneService
{
    Task<Result<bool>> ChangePhone(
        int userId,
        ChangePhoneRequest request);
}