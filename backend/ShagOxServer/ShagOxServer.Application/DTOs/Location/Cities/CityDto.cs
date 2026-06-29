using System;
using System.Collections.Generic;
using System.Text;

namespace ShagOxServer.Application.DTOs.Location.Cities;
public sealed record CityDto
(
    string Name,
    int RegionId,
    string NameRegion
);
