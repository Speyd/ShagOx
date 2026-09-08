using ShagOxServer.Application.Interfaces.Repositories.Base;
using ShagOxServer.Application.Interfaces.Repositories.Dictionaries.AttributeDefinitions;
using ShagOxServer.Application.Services.Base;
using ShagOxServer.Domain.Entities.Dictionaries;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Services.Dictionaries.AttributeDefinitions.Validator;
public class AttributeDefinitionValidator
    : BaseValidator<AttributeDefinition>
{
    private readonly IAttributeDefinitionExistsRepository _attributeExistsRepository;


    public AttributeDefinitionValidator(
        IRepository<AttributeDefinition> attributeRepository,
        IAttributeDefinitionExistsRepository attributeExistsRepository
    ) : base(attributeRepository, attributeExistsRepository)
    {
        _attributeExistsRepository = attributeExistsRepository;
    }


    public async Task<Result<bool>> ExistsByKeyAsync(
      string attributeName,
      int categoryId)
    {
        if (await _attributeExistsRepository
            .ExistsByCategoryAsync(attributeName, categoryId))
        {
            return Result<bool>
                .NotFound(typeof(AttributeDefinition));
        }       

        return Result<bool>.Success(true);
    }

    public async Task<Result<bool>> NotExistsByKeyAsync(
      string attributeName,
      int categoryId)
    {
        if (await _attributeExistsRepository
            .ExistsByCategoryAsync(attributeName, categoryId))
        {
            return Result<bool>
                .AlreadyExists(typeof(AttributeDefinition));
        }

        return Result<bool>.Success(true);
    }
}