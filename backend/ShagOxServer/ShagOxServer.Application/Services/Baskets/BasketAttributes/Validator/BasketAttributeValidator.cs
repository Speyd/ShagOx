using ShagOxServer.Application.Interfaces.Repositories.Base;
using ShagOxServer.Application.Interfaces.Repositories.Baskets.BasketAttributes;
using ShagOxServer.Application.Interfaces.Repositories.Dictionaries.AttributeDefinitions;
using ShagOxServer.Application.Services.Base;
using ShagOxServer.Domain.Entities.Baskets;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Services.Baskets.BasketAttributes.Validator;
public class BasketAttributeValidator
    : BaseValidator<BasketAttribute>
{
    private readonly IBasketAttributeExistsRepository _attributeExistsRepository;

    public BasketAttributeValidator(
        IRepository<BasketAttribute> attributeRepository,
        IBasketAttributeExistsRepository attributeExistsRepository,
        IAttributeDefinitionExistsRepository attributeDefenitionExistsRepository
    ) : base(attributeRepository, attributeExistsRepository)
    {
        _attributeExistsRepository = attributeExistsRepository;
    }


    public async Task<Result<bool>> ExistsAsync(
      int attributeDefinitionId,
      int order)
    {
        if (!await _attributeExistsRepository
            .ExistsAsync(attributeDefinitionId, order))
        {
            return Result<bool>
                .NotFound(typeof(BasketAttribute));
        }

        return Result<bool>.Success(true);
    }

    public async Task<Result<bool>> NotExistsAsync(
        int attributeDefinitionId,
        int order)
    {
        if (await _attributeExistsRepository
           .ExistsAsync(attributeDefinitionId, order))
        {
            return Result<bool>
                .NotFound(typeof(BasketAttribute));
        }

        return Result<bool>.Success(true);
    }
}