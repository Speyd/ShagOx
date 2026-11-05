using ShagOxServer.Application.DTOs.Base.Responses;
using ShagOxServer.Application.Interfaces.Services.Base.Delete;
using ShagOxServer.Domain.Entities.Dictionaries.Translations;

namespace ShagOxServer.Application.Interfaces.Services.Dictionaries.ProductTypes.Translations.Delete;
public interface IProductTypeTranslationDeleteService
    : IDeleteService<DeleteResponse>,
    ISoftDeleteService<ProductTypeTranslation, DeleteResponse>
{
}
