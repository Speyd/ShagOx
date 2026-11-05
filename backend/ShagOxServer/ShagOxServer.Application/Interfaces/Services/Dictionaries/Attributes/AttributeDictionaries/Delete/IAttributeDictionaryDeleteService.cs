using ShagOxServer.Application.DTOs.Base.Responses;
using ShagOxServer.Application.Interfaces.Services.Base.Delete;
using ShagOxServer.Domain.Entities.Dictionaries.Attributes;

namespace ShagOxServer.Application.Interfaces.Services.Dictionaries.Attributes.AttributeDictionaries.Delete;
public interface IAttributeDictionaryDeleteService
    : IDeleteService<DeleteResponse>,
    ISoftDeleteService<AttributeDictionary, DeleteResponse>
{
}
