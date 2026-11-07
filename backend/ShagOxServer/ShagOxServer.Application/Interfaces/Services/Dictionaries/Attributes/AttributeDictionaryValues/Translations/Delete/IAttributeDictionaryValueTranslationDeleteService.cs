using ShagOxServer.Application.DTOs.Base.Responses;
using ShagOxServer.Application.Interfaces.Services.Base.Delete;
using ShagOxServer.Domain.Entities.Dictionaries.Attributes.Translations;

namespace ShagOxServer.Application.Interfaces.Services.Dictionaries.Attributes.AttributeDictionaryValues.Translations.Delete;
public interface IAttributeDictionaryValueTranslationDeleteService
    : IDeleteService<DeleteResponse>,
    ISoftDeleteService<AttributeDictionaryValueTranslation, 
        DeleteResponse>
{
}
