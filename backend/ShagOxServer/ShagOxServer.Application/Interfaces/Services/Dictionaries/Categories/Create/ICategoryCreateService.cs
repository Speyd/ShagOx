using ShagOxServer.Application.DTOs.Base.Responses;
using ShagOxServer.Application.DTOs.Dictionaries.Categories.Create;
using ShagOxServer.Application.Interfaces.Services.Base;

namespace ShagOxServer.Application.Interfaces.Services.Dictionaries.Categories.Create;
public interface ICategoryCreateService
    : ICreateService<
        CreateResponse,
        CategoryCreateRequest
        >
{
}