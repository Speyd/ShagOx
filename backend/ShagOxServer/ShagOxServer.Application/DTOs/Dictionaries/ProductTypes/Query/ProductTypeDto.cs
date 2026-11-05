using ShagOxServer.Application.DTOs.Base;
using ShagOxServer.Domain.Base.Enum;

namespace ShagOxServer.Application.DTOs.Dictionaries.ProductTypes.Query;
public sealed record ProductTypeDto
(
    long Id,
    EntityStatus EntityStatus,
    string Name,
    string Description,
    string? Label
) : BaseDto(Id, EntityStatus);
