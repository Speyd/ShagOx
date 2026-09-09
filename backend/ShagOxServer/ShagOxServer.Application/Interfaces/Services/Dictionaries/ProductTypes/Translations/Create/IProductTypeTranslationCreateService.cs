using ShagOxServer.Application.DTOs.Base.Responses;
using ShagOxServer.Application.DTOs.Dictionaries.ProductTypes.Translations.Create;
using ShagOxServer.Application.Interfaces.Services.Base;

namespace ShagOxServer.Application.Interfaces.Services.Dictionaries.ProductTypes.Translations.Create;
public interface IProductTypeTranslationCreateService
    : ICreateService<
        CreateResponse,
        ProductTypeTranslationCreateRequest
        >
{
}