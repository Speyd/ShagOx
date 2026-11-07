using ShagOxServer.Application.DTOs.Base.Responses;
using ShagOxServer.Application.Interfaces.Services.Base.Delete;
using ShagOxServer.Domain.Entities.Dictionaries.Attributes;

namespace ShagOxServer.Application.Interfaces.Services.Dictionaries.Attributes.AttributeDictionaryValues.Delete;
public interface IAttributeDictionaryValueDeleteService
    : IDeleteService<DeleteResponse>,
    ISoftDeleteService<AttributeDictionaryValue, DeleteResponse>
{
}
