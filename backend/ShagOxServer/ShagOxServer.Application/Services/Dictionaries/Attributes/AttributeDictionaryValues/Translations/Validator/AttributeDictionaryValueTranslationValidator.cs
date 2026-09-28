using ShagOxServer.Application.Interfaces.Repositories.Base;
using ShagOxServer.Application.Interfaces.Repositories.Dictionaries.Attributes.AttributeDictionaryValues.Translations;
using ShagOxServer.Application.Resources.EntityNames;
using ShagOxServer.Application.Services.Base.Translations;
using ShagOxServer.Domain.Entities.Dictionaries.Attributes.Translations;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Services.Dictionaries.Attributes.AttributeDictionaryValues.Translations.Validator;
public class AttributeDictionaryValueTranslationValidator
    : BaseTranslationValidator<AttributeDictionaryValueTranslation>
{
    private readonly IAttributeDictionaryValueTranslationExistsRepository _attributeExistsRepository;


    public AttributeDictionaryValueTranslationValidator(
        IRepository<AttributeDictionaryValueTranslation> attributeRepository,
        IAttributeDictionaryValueTranslationExistsRepository attributeExistsRepository
    ) : base(attributeRepository, attributeExistsRepository)
    {
        _attributeExistsRepository = attributeExistsRepository;
    }


    public async Task<Result<bool>> ExistsByNameAsync(
        string name)
    {
        if (!await _attributeExistsRepository
            .ExistsByNameAsync(name))
        {
            return Result<bool>.NotFound(
                EntityNamesResources.AttributeDictionaryValueTranslation);
        }

        return Result<bool>.Success(true);
    }

    public async Task<Result<bool>> NotExistsByNameAsync(
        string name)
    {
        if (await _attributeExistsRepository
            .ExistsByNameAsync(name))
        {
            return Result<bool>.AlreadyExists(
                EntityNamesResources.AttributeDictionaryValueTranslation);
        }

        return Result<bool>.Success(true);
    }
}