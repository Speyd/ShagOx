using ShagOxServer.Application.DTOs.Base.Responses;
using ShagOxServer.Application.Interfaces.Persistences;
using ShagOxServer.Application.Interfaces.Repositories.Base;
using ShagOxServer.Application.Interfaces.Services.Dictionaries.AttributeDefinitions.Translations.Delete;
using ShagOxServer.Application.Services.Dictionaries.AttributeDefinitions.Translations.Validator;
using ShagOxServer.Domain.Entities.Dictionaries.Translations;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Services.Dictionaries.AttributeDefinitions.Translations.Delete;
public class AttributeDefinitionTranslationDeleteService
    : IAttributeDefinitionTranslationDeleteService
{
    private readonly IRepository<AttributeDefinitionTranslation> _attributeRepository;
    private readonly AttributeDefinitionTranslationValidator _attributeValidator;

    private readonly IUnitOfWork _unitOfWork;


    public AttributeDefinitionTranslationDeleteService(
        IRepository<AttributeDefinitionTranslation> attributeRepository,
        AttributeDefinitionTranslationValidator attributeValidator,
        IUnitOfWork unitOfWork)
    {
        _attributeRepository = attributeRepository;
        _attributeValidator = attributeValidator;
        _unitOfWork = unitOfWork;
    }


    public async Task<Result<DeleteResponse>> DeleteAsync(
        int id)
    {
        var region = await _attributeValidator
            .GetByIdAsync(id);

        if (!region.IsSuccess)
            return Result<DeleteResponse>.Fail(region.Error);

        await _unitOfWork.BeginTransactionAsync();

        try
        {
            _attributeRepository.Delete(region.Value!);

            await _unitOfWork.CommitAsync();
        }
        catch
        {
            await _unitOfWork.RollbackAsync();
            throw;
        }

        return Result<DeleteResponse>.Success(
           new DeleteResponse(
               region.Value!.Id,
               DateTime.UtcNow
           )
       );
    }
}