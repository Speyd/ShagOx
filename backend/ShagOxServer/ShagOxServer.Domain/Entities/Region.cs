using ShagOxServer.Domain.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShagOxServer.Domain.Entities;

public class Region : BaseEntity
{
    public string Name { get; set; } = "";
    public List<City> Cities { get; set; } = new List<City>();

    public override string ToString()
    {
        return Name;
    }
}
