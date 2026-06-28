using ShagOxServer.Application.Interfaces.Common.Validators;
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
}