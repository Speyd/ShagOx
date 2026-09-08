using ShagOxServer.Application.DTOs.Base;

namespace ShagOxServer.Application.DTOs.Dictionaries.ProductTypes;
public sealed record ProductTypeDto
(
    int Id,
    string Name,
    string Description
) : BaseDto(Id);