using ShagOxServer.Application.Interfaces.Repositories.Base;
using ShagOxServer.Application.Interfaces.Repositories.Dictionaries.AttributeDefinitions.Translations;
using ShagOxServer.Application.Resources.EntityNames;
using ShagOxServer.Application.Services.Base.Translations;
using ShagOxServer.Domain.Entities.Dictionaries.Translations;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Services.Dictionaries.AttributeDefinitions.Translations.Validator;
public class AttributeDefinitionTranslationValidator
    : BaseTranslationValidator<AttributeDefinitionTranslation>
{
    private readonly IAttributeDefinitionTranslationExistsRepository _attributeExistsRepository;


    public AttributeDefinitionTranslationValidator(
        IRepository<AttributeDefinitionTranslation> attributeRepository,
        IAttributeDefinitionTranslationExistsRepository attributeExistsRepository
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
                EntityNamesResources.AttributeDefinitionTranslation);
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
                EntityNamesResources.AttributeDefinitionTranslation);
        }

        return Result<bool>.Success(true);
    }
}