using ShagOxServer.Application.DTOs.Dictionaries.AttributeDefinitions.Delete;
using ShagOxServer.Application.Interfaces.Dictionaries.AttributeDefinitions.Delete;
using ShagOxServer.Infrastructure.Interfaces.Dictionaries.AttributeDefinitions;
using ShagOxServer.SharedKernel.Results;

namespace ShagOxServer.Application.Services.Dictionaries.AttributeDefinitions.Delete;
public class AttributeDefinitionDeleteService : IAttributeDefinitionDeleteService
{
    private readonly IAttributeDefinitionRepository _repository;

    public AttributeDefinitionDeleteService(
        IAttributeDefinitionRepository attributeRepository)
    {
        _repository = attributeRepository;
    }

    public async Task<Result<AttributeDefinitionDeleteResponse>> DeleteAttributeDefinitionAsync(
        int id)
    {
        var attribute = await _repository.GetByIdAsync(id);
        if (attribute is null)
            return Result<AttributeDefinitionDeleteResponse>.NotFound("Attribute Definition");

        await _repository.DeleteAsync(attribute);

        return Result<AttributeDefinitionDeleteResponse>.Success(
           new AttributeDefinitionDeleteResponse(
               attribute.Id,
               DateTime.UtcNow
           )
       );
    }
}
