using ShagOxServer.Application.DTOs.Base;

namespace ShagOxServer.Application.DTOs.Dictionaries.ProductTypes.Translations;
public sealed record ProductTypeDto
(
    int Id,
    string Name
) : BaseDto(Id);