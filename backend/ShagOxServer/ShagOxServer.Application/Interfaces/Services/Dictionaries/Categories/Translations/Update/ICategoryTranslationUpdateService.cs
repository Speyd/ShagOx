using ShagOxServer.Application.DTOs.Base.Responses;
using ShagOxServer.Application.DTOs.Dictionaries.Categories.Translations.Update;
using ShagOxServer.Application.Interfaces.Services.Base;

namespace ShagOxServer.Application.Interfaces.Services.Dictionaries.Categories.Translations.Update;
public interface ICategoryTranslationUpdateService
    : IUpdateService<UpdateResponse,
        CategoryTranslationUpdateRequest>
{
}