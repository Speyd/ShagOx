using ShagOxServer.Application.DTOs.Base.Responses;
using ShagOxServer.Application.DTOs.Dictionaries.ProductTypes.Translations.Update;
using ShagOxServer.Application.Interfaces.Services.Base;

namespace ShagOxServer.Application.Interfaces.Services.Dictionaries.ProductTypes.Translations.Update;
public interface IProductTypeTranslationUpdateService
    : IUpdateService<UpdateResponse,
        ProductTypeTranslationUpdateRequest>
{
}