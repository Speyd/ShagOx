using ShagOxServer.Application.Common.Validators;

namespace ShagOxServer.Application.Interfaces.Common.Validators;
public interface IContactValidator
{
    UserContactType Detect(string value);
}