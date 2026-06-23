using System;
using System.Collections.Generic;
using System.Text;

namespace ShagOxServer.Application.DTOs.Auth.Register;
public sealed record RegisterRequest(
    string EmailOrPhone,
    string Password
);