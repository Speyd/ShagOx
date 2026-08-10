using ShagOxServer.Application.DTOs.Common.Responses;
using ShagOxServer.Application.DTOs.Dictionaries.Categories.Update;
using ShagOxServer.Application.Interfaces.Services.Base;

namespace ShagOxServer.Application.Interfaces.Services.Dictionaries.Categories.Update;
public interface ICategoryUpdateService
    : IUpdateService<UpdateResponse, CategoryUpdateRequest>
{
}