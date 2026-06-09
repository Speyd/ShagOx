using System;
using System.Collections.Generic;
using System.Text;

namespace ShagOxServer.Domain.Entities.Dictionaries;
public class Category
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public ProductType ProductType { get; set; }

    public List<AttributeDefinition> Attributes { get; set; }
        = new List<AttributeDefinition>();
}