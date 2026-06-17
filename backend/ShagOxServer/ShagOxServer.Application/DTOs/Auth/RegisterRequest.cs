using System;
using System.Collections.Generic;
using System.Text;

namespace ShagOxServer.Application.DTOs.Auth;
public sealed record RegisterRequest(
    string EmailOrPhone,
    string Password
);