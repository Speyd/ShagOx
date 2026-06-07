using System;
using System.Collections.Generic;
using System.Text;

namespace ShagOxServer.Domain.Base;
public abstract class BaseEntity
{
    public int Id { get; set; }

    public abstract override string ToString();
}
