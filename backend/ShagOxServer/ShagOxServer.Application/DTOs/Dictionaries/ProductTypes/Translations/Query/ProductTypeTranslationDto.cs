using ShagOxServer.Application.DTOs.Base;
using ShagOxServer.Domain.Base.Enum;

namespace ShagOxServer.Application.DTOs.Dictionaries.ProductTypes.Translations.Query;
public sealed record ProductTypeTranslationDto
(
    long Id,
    EntityStatus EntityStatus,
    string Name
) : BaseDto(Id, EntityStatus);
