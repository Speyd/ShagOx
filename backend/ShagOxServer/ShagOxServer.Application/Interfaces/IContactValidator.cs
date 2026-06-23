using ShagOxServer.Application.Validators;

namespace ShagOxServer.Application.Interfaces;
public interface IContactValidator
{
    UserContactType Detect(string value);
}