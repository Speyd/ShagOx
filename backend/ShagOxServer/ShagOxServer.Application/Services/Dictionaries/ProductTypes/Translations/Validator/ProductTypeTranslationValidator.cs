using ShagOxServer.Application.Interfaces.Repositories.Base;
using ShagOxServer.Application.Interfaces.Repositories.Base.Query;
using ShagOxServer.Application.Interfaces.Repositories.Dictionaries.ProductTypes.Translations;
using ShagOxServer.Application.Services.Base.Translations;
using ShagOxServer.Domain.Entities.Dictionaries;
using ShagOxServer.Domain.Entities.Dictionaries.Translations;

namespace ShagOxServer.Application.Services.Dictionaries.ProductTypes.Translations.Validator;
public class ProductTypeTranslationValidator
    : BaseTranslationValidator<ProductType,
        ProductTypeTranslation>
{
    public ProductTypeTranslationValidator(
        IQueryRepository<ProductTypeTranslation> typeRepository,
        IProductTypeTranslationExistsRepository typeExistsRepository
    ) : base(typeRepository, typeExistsRepository)
    {
    }
}
