using ShagOxServer.Application.DTOs.Dictionaries.AttributeDefinitions.Delete;
using ShagOxServer.Application.Interfaces.Persistences;
using ShagOxServer.Application.Interfaces.Repositories.Dictionaries.AttributeDefinitions;
using ShagOxServer.Application.Interfaces.Services.Dictionaries.AttributeDefinitions.Delete;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Services.Dictionaries.AttributeDefinitions.Delete;
public class AttributeDefinitionDeleteService : IAttributeDefinitionDeleteService
{
    private readonly IAttributeDefinitionRepository _repository;
    private readonly IUnitOfWork _unitOfWork;


    public AttributeDefinitionDeleteService(
        IAttributeDefinitionRepository attributeRepository,
        IUnitOfWork unitOfWork)
    {
        _repository = attributeRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<AttributeDefinitionDeleteResponse>> DeleteAttributeDefinitionAsync(
        int id)
    {
        var attribute = await _repository.GetByIdAsync(id);
        if (attribute is null)
            return Result<AttributeDefinitionDeleteResponse>.NotFound("Attribute Definition");

        try
        {
            _repository.Add(attribute);

            await _unitOfWork.CommitAsync();
        }
        catch
        {
            await _unitOfWork.RollbackAsync();
            throw;
        }

        return Result<AttributeDefinitionDeleteResponse>.Success(
           new AttributeDefinitionDeleteResponse(
               attribute.Id,
               DateTime.UtcNow
           )
       );
    }
}
