using System;
using System.Collections.Generic;
using System.Text;

namespace ShagOxServer.Application.DTOs.Specification.Conditions.Delete;
public sealed record ConditionDeleteResponse
(
    int Id,
    DateTime DeleteTime
);