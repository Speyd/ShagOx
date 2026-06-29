using System;
using System.Collections.Generic;
using System.Text;

namespace ShagOxServer.Application.DTOs.Dictionaries.AttributeDefinitions.Update;
public sealed record AttributeDefinitionUpdateResponse
(
    int Id,
    DateTime CreatedAt
);