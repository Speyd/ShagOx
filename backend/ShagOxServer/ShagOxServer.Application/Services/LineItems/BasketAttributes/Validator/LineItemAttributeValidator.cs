using ShagOxServer.Application.Interfaces.Repositories.Base;
using ShagOxServer.Application.Interfaces.Repositories.Base.Query;
using ShagOxServer.Application.Interfaces.Repositories.LineItems.LineItemAttributes;
using ShagOxServer.Application.Interfaces.Services.Caches;
using ShagOxServer.Application.Resources.EntityNames;
using ShagOxServer.Application.Services.Base.Validator;
using ShagOxServer.Domain.Entities.Dictionaries.Attributes;
using ShagOxServer.Domain.Entities.LineItems;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Services.LineItems.LineItemAttributes.Validator;
public class LineItemAttributeValidator
    : BaseValidator<LineItemAttribute>
{
    private readonly ILineItemAttributeExistsRepository _attributeExistsRepository;


    public LineItemAttributeValidator(
        IRepository<LineItemAttribute> repository,
        IQueryRepository<LineItemAttribute> attributeRepository,
        ILineItemAttributeExistsRepository attributeExistsRepository,
        IDataCacheService cache
    ) 
        : base(repository, attributeRepository, attributeExistsRepository, cache)
    {
        _attributeExistsRepository = attributeExistsRepository;
    }


    public async Task<Result<bool>> ExistsAsync(
        AttributeDefinition attribute,
        int order,
        CancellationToken cancellationToken = default)
    {
        if (!await _attributeExistsRepository
            .ExistsAsync(attribute.CategoryId, 
                attribute.Id, order, cancellationToken))
        {
            return Result<bool>.NotFound(
                EntityNamesResources.LineItemAttribute);
        }

        return Result<bool>.Success(true);
    }

    public async Task<Result<bool>> NotExistsAsync(
        AttributeDefinition attribute,
        int order,
        CancellationToken cancellationToken = default)
    {
        if (await _attributeExistsRepository
                .ExistsAsync(attribute.CategoryId, 
                    attribute.Id, order, cancellationToken))
        {
            return Result<bool>.AlreadyExists(
                EntityNamesResources.LineItemAttribute);
        }

        return Result<bool>.Success(true);
    }
}
