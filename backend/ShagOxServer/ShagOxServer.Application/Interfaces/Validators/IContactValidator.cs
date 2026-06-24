using ShagOxServer.Application.Common.Validators;

namespace ShagOxServer.Application.Interfaces.Validators;
public interface IContactValidator
{
    UserContactType Detect(string value);
}