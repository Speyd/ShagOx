using ShagOxServer.Application.Common.Validators.Enum;

namespace ShagOxServer.Application.Interfaces.Services.Common.Validators;
public interface IContactValidator
{
    UserContactType Detect(string value);
    bool TryDetect(string value, out UserContactType type);
}