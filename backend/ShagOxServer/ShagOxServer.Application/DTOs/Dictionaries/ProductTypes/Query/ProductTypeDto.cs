using ShagOxServer.Application.DTOs.Base;

namespace ShagOxServer.Application.DTOs.Dictionaries.ProductTypes.Query;
public sealed record ProductTypeDto
(
    long Id,
    string Name,
    string Description,
    string? Lable
) : BaseDto(Id);