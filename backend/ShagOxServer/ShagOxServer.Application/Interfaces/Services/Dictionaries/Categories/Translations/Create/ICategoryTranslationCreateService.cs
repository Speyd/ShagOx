using ShagOxServer.Application.DTOs.Base.Responses;
using ShagOxServer.Application.DTOs.Dictionaries.Categories.Translations.Create;
using ShagOxServer.Application.Interfaces.Services.Base;

namespace ShagOxServer.Application.Interfaces.Services.Dictionaries.Categories.Translations.Create;
public interface ICategoryTranslationCreateService
    : ICreateService<
        CreateResponse,
        CategoryTranslationCreateRequest
        >
{
}