using ShagOxServer.Application.DTOs.Dictionaries.AttributeDefinitions.Delete;
using ShagOxServer.Application.Interfaces.Persistences;
using ShagOxServer.Application.Interfaces.Repositories.Dictionaries.AttributeDefinitions;
using ShagOxServer.Application.Interfaces.Services.Dictionaries.AttributeDefinitions.Delete;
using ShagOxServer.Application.Services.Dictionaries.AttributeDefinitions.Validator;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Services.Dictionaries.AttributeDefinitions.Delete;
public class AttributeDefinitionDeleteService : IAttributeDefinitionDeleteService
{
    private readonly IAttributeDefinitionRepository _attributeRepository;
    private readonly AttributeDefinitionValidator _attributeValidator;

    private readonly IUnitOfWork _unitOfWork;


    public AttributeDefinitionDeleteService(
        IAttributeDefinitionRepository attributeRepository,
        AttributeDefinitionValidator attributeValidator,
        IUnitOfWork unitOfWork)
    {
        _attributeRepository = attributeRepository;
        _attributeValidator = attributeValidator;
        _unitOfWork = unitOfWork;
    }


    public async Task<Result<AttributeDefinitionDeleteResponse>> DeleteAsync(
        int id)
    {
        var attribute = await _attributeValidator.GetByIdAsync(id);
        if (!attribute.IsSuccess)
            return Result<AttributeDefinitionDeleteResponse>.Fail(attribute.Error ?? "");

        await _unitOfWork.BeginTransactionAsync();

        try
        {
            _attributeRepository.Add(attribute.Value!);

            await _unitOfWork.CommitAsync();
        }
        catch
        {
            await _unitOfWork.RollbackAsync();
            throw;
        }

        return Result<AttributeDefinitionDeleteResponse>.Success(
           new AttributeDefinitionDeleteResponse(
               attribute.Value!.Id,
               DateTime.UtcNow
           )
       );
    }
}