using System;
using System.Collections.Generic;
using System.Text;

namespace ShagOxServer.Application.DTOs.Specification.Currencies.Create;
public sealed record CurrencyCreateResponse
(
    int Id,
    DateTime CreatedAt
);