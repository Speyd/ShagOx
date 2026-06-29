using System;
using System.Collections.Generic;
using System.Text;

namespace ShagOxServer.Application.DTOs.Dictionaries.Categories.Create;
public sealed record CategoryCreateResponse
(
    int Id,
    DateTime CreatedAt
);