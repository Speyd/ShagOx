using ShagOxServer.Application.DTOs.Common.Responses;
using ShagOxServer.Application.Interfaces.Persistences;
using ShagOxServer.Application.Interfaces.Repositories.Base;
using ShagOxServer.Application.Interfaces.Services.Dictionaries.AttributeDefinitions.Delete;
using ShagOxServer.Application.Services.Dictionaries.AttributeDefinitions.Validator;
using ShagOxServer.Domain.Entities.Dictionaries;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Services.Dictionaries.AttributeDefinitions.Delete;
public class AttributeDefinitionDeleteService 
    : IAttributeDefinitionDeleteService
{
    private readonly IRepository<AttributeDefinition> _attributeRepository;
    private readonly AttributeDefinitionValidator _attributeValidator;

    private readonly IUnitOfWork _unitOfWork;


    public AttributeDefinitionDeleteService(
        IRepository<AttributeDefinition> attributeRepository,
        AttributeDefinitionValidator attributeValidator,
        IUnitOfWork unitOfWork)
    {
        _attributeRepository = attributeRepository;
        _attributeValidator = attributeValidator;
        _unitOfWork = unitOfWork;
    }


    public async Task<Result<DeleteResponse>> DeleteAsync(
        int id)
    {
        var attribute = await _attributeValidator
            .GetByIdAsync(id);

        if (!attribute.IsSuccess)
            return Result<DeleteResponse>.Fail(attribute.Error);

        await _unitOfWork.BeginTransactionAsync();

        try
        {
            _attributeRepository.Delete(attribute.Value!);

            await _unitOfWork.CommitAsync();
        }
        catch
        {
            await _unitOfWork.RollbackAsync();
            throw;
        }

        return Result<DeleteResponse>.Success(
           new DeleteResponse(
               attribute.Value!.Id,
               DateTime.UtcNow
           )
       );
    }
}