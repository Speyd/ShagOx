using System;
using System.Collections.Generic;
using System.Text;

namespace ShagOxServer.Domain.Entities.Dictionaries;
public class AttributeDefinition
{
    public int Id { get; set; }

    public string Key { get; set; } = null!;
    public string Type { get; set; } = null!;
    public bool Required { get; set; }

    public int? Min { get; set; }
    public int? Max { get; set; }
}
