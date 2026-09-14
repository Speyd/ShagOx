using System;
using System.Collections.Generic;
using System.Text;

namespace ShagOxServer.Application.Services.Verifications.Enum;
public enum VerificationCodeResult
{
    Success = 0,
    NotFound = 1,
    Expired = 2,
    Invalid = 3,
    AttemptsExceeded = 4,
    AlreadyUsed = 5
}