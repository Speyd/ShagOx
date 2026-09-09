using ShagOxServer.Application.Interfaces.Repositories.Base;
using ShagOxServer.Application.Interfaces.Repositories.Dictionaries.ProductTypes.Translations;
using ShagOxServer.Application.Services.Base.Translations;
using ShagOxServer.Domain.Entities.Dictionaries.Translations;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Services.Dictionaries.ProductTypes.Translations.Validator;
public class ProductTypeTranslationValidator
    : BaseTranslationValidator<ProductTypeTranslation>
{
    private readonly IProductTypeTranslationExistsRepository _typeExistsRepository;


    public ProductTypeTranslationValidator(
        IRepository<ProductTypeTranslation> typeRepository,
        IProductTypeTranslationExistsRepository typeExistsRepository
    ) : base(typeRepository, typeExistsRepository)
    {
        _typeExistsRepository = typeExistsRepository;
    }


    public async Task<Result<bool>> ExistsByNameAsync(
        string name)
    {
        if (!await _typeExistsRepository
            .ExistsByNameAsync(name))
        {
            return Result<bool>
                .NotFound(typeof(ProductTypeTranslation));
        }

        return Result<bool>.Success(true);
    }

    public async Task<Result<bool>> NotExistsByNameAsync(
        string name)
    {
        if (await _typeExistsRepository
            .ExistsByNameAsync(name))
        {
            return Result<bool>
                .AlreadyExists(typeof(ProductTypeTranslation));
        }

        return Result<bool>.Success(true);
    }
}