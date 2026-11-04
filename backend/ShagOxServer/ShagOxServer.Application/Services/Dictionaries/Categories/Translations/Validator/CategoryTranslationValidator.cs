using ShagOxServer.Application.Interfaces.Repositories.Base;
using ShagOxServer.Application.Interfaces.Repositories.Base.Query;
using ShagOxServer.Application.Interfaces.Repositories.Dictionaries.Categories.Translations;
using ShagOxServer.Application.Interfaces.Services.Caches;
using ShagOxServer.Application.Services.Base.Translations;
using ShagOxServer.Domain.Entities.Dictionaries;
using ShagOxServer.Domain.Entities.Dictionaries.Translations;

namespace ShagOxServer.Application.Services.Dictionaries.Categories.Translations.Validator;
public class CategoryTranslationValidator
    : BaseTranslationValidator<Category, CategoryTranslation>
{
    public CategoryTranslationValidator(
        IRepository<CategoryTranslation> repository,
        IQueryRepository<CategoryTranslation> categoryRepository,
        ICategoryTranslationExistsRepository categoryExistsRepository,
        IDataCacheService cache
    ) 
        : base(repository, 
            categoryRepository, 
            categoryExistsRepository, 
            cache)
    {
    }
}
