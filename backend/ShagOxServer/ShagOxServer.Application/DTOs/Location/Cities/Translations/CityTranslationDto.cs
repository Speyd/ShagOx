using ShagOxServer.Application.DTOs.Base;

namespace ShagOxServer.Application.DTOs.Location.Cities.Translations;
public sealed record CityTranslationDto
(
    long Id,
    string Name
) : BaseDto(Id);