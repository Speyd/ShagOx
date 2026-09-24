using ShagOxServer.Application.Common.Validators.Enum;
using ShagOxServer.Application.Interfaces.Services.Common.Validators;
using System.Text.RegularExpressions;

namespace ShagOxServer.Application.Common.Validators;
public class ContactValidator : IContactValidator
{
    public UserContactType Detect(string value)
    {
        if (TryDetect(value, out var type))
            return type;

        throw new Exception("Invalid contact");
    }

    public bool TryDetect(string value, out UserContactType type)
    {
        if (IsEmail(value))
        {
            type = UserContactType.Email;
            return true;
        }

        if (IsPhone(value))
        {
            type = UserContactType.Phone;
            return true;
        }

        if (IsUserName(value))
        {
            type = UserContactType.UserName;
            return true;
        }

        type = default;
        return false;
    }


    private bool IsEmail(string value)
    {
        return Regex.IsMatch(
            value,
            @"^[^@\s]+@[^@\s]+\.[^@\s]+$"
        );
    }

    private bool IsPhone(string value)
    {
        return Regex.IsMatch(
            value,
            @"^\+?[0-9]{10,15}$"
        );
    }

    private bool IsUserName(string value)
    {
        return Regex.IsMatch(
            value,
            @"^[\p{L}\p{N}_]{3,20}$"
        );
    }
}