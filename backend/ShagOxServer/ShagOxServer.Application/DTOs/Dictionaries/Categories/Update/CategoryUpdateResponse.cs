using System;
using System.Collections.Generic;
using System.Text;

namespace ShagOxServer.Application.DTOs.Dictionaries.Categories.Update;
public sealed record CategoryUpdateResponse
(
    DateTime TimeUpdate,
    int CountUpdatedProperty
);
