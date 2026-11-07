using ShagOxServer.Application.DTOs.Base.Responses;
using ShagOxServer.Application.DTOs.Dictionaries.Categories.Update;
using ShagOxServer.Application.Interfaces.Services.Base.Update;

namespace ShagOxServer.Application.Interfaces.Services.Dictionaries.Categories.Update;
public interface ICategoryUpdateService
    : IUpdateService<UpdateResponse, CategoryUpdateRequest>,
    IEntityStatusUpdateService
{
}
