using ShagOxServer.Domain.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShagOxServer.Domain.Entities;

public class Region : BaseEntity
{
    public string Name { get; set; } = "";

    public override string ToString()
    {
        return Name;
    }
}
