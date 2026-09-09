using ShagOxServer.Application.DTOs.Base;

namespace ShagOxServer.Application.DTOs.Dictionaries.ProductTypes.Translations;
public sealed record ProductTypeTranslationDto
(
    int Id,
    string Name
) : BaseDto(Id);