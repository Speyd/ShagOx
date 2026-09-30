using System;
using System.Collections.Generic;
using System.Text;

namespace ShagOxServer.Application.Common.Settings.Caches;
public sealed class CacheSettings
{
    public TimeSpan KeyExpiration { get; set; }
}