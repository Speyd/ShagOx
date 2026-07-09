using ShagOxServer.Application.Common.Validators;

namespace ShagOxServer.Application.Interfaces.Services.Common.Validators;
public interface IContactValidator
{
    UserContactType Detect(string value);
}