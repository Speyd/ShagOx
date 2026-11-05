using ShagOxServer.Application.DTOs.Base.Responses;
using ShagOxServer.Application.Interfaces.Services.Base.Delete;
using ShagOxServer.Domain.Entities.Dictionaries;

namespace ShagOxServer.Application.Interfaces.Services.Dictionaries.Categories.Delete;
public interface ICategoryDeleteService
    : IDeleteService<DeleteResponse>,
    ISoftDeleteService<Category, DeleteResponse>
{
}
