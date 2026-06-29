using System;
using System.Collections.Generic;
using System.Text;

namespace ShagOxServer.Application.DTOs.Dictionaries.AttributeDefinitions.Create;
public sealed record AttributeDefinitionCreateResponse
(
    int Id,
    DateTime CreatedAt
);