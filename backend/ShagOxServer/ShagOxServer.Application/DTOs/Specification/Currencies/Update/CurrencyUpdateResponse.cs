using System;
using System.Collections.Generic;
using System.Text;

namespace ShagOxServer.Application.DTOs.Specification.Currencies.Update;
public sealed record CurrencyUpdateResponse
(
    DateTime TimeUpdate,
    int CountUpdatedProperty
);
