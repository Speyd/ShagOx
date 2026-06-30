using System;
using System.Collections.Generic;
using System.Text;

namespace ShagOxServer.Application.DTOs.Specification.Currencies.Create;
public sealed record CurrencyCreateRequest
(
    string Code,
    string Symbol,
    string Name
);
