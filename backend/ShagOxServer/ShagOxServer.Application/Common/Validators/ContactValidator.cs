using ShagOxServer.Application.Common.Validators.Enum;
using ShagOxServer.Application.Interfaces.Services.Common.Validators;
using System;
using System.Text.RegularExpressions;

namespace ShagOxServer.Application.Common.Validators;


public class ContactValidator : IContactValidator
{
    public UserContactType Detect(string value)
    {
        if (IsEmail(value))
            return UserContactType.Email;

        if (IsPhone(value))
            return UserContactType.Phone;

        if (IsUserName(value))
            return UserContactType.UserName;

        throw new Exception("Invalid contact");
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