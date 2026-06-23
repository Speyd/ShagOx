using ShagOxServer.Domain.Entities.Account;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShagOxServer.Application.Interfaces;

public interface IJwtService
{
    public string GenerateToken(User user);
}
