using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Interfaces.Services.Auth.Users.Contacts.UserNames;
public interface IUserNameService
{
    Task<Result<string>> GenerateUniqueAsync(
        string baseName);
}