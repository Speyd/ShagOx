using ShagOxServer.Application.DTOs.Base.Responses;
using ShagOxServer.Application.Interfaces.Services.Base.Delete;
using ShagOxServer.Domain.Entities.Dictionaries.Translations;

namespace ShagOxServer.Application.Interfaces.Services.Dictionaries.Categories.Translations.Delete;
public interface ICategoryTranslationDeleteService
    : IDeleteService<DeleteResponse>,
    ISoftDeleteService<CategoryTranslation, DeleteResponse>
{
}
