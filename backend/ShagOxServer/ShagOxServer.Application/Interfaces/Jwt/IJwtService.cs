using ShagOxServer.Domain.Entities.Account;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShagOxServer.Application.Interfaces.Jwt;

public interface IJwtService
{
    public string GenerateToken(User user);
}
