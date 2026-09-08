using ShagOxServer.Application.DTOs.Base.Responses;
using ShagOxServer.Application.DTOs.Dictionaries.AttributeDefinitions.Translations.Update;
using ShagOxServer.Application.Interfaces.Persistences;
using ShagOxServer.Application.Interfaces.Repositories.Base;
using ShagOxServer.Application.Interfaces.Services.Dictionaries.AttributeDefinitions.Translations.Update;
using ShagOxServer.Application.Services.Base.Translations;
using ShagOxServer.Application.Services.Dictionaries.AttributeDefinitions.Translations.Validator;
using ShagOxServer.Application.Services.Dictionaries.AttributeDefinitions.Validator;
using ShagOxServer.Domain.Entities.Dictionaries;
using ShagOxServer.Domain.Entities.Dictionaries.Translations;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Services.Dictionaries.AttributeDefinitions.Translations.Update;
public class AttributeDefinitionTranslationUpdateService
    : BaseTranslationUpdateSerivce<AttributeDefinition, AttributeDefinitionTranslation>,
    IAttributeDefinitionTranslationUpdateService
{
    private readonly IRepository<AttributeDefinitionTranslation> _attributeRepository;
    private readonly AttributeDefinitionTranslationValidator _attributeTranslationValidator;

    private readonly IUnitOfWork _unitOfWork;


    public AttributeDefinitionTranslationUpdateService(
        IRepository<AttributeDefinitionTranslation> attributeRepository,
        AttributeDefinitionTranslationValidator attributeTranslationValidator,
        AttributeDefinitionValidator attributeValidator,
        IUnitOfWork unitOfWork
    ) : base(attributeValidator, attributeTranslationValidator)
    {
        _attributeRepository = attributeRepository;
        _attributeTranslationValidator = attributeTranslationValidator;
        _unitOfWork = unitOfWork;
    }


    public async Task<Result<UpdateResponse>> UpdateAsync(
        int statusTranslationId,
        AttributeDefinitionTranslationUpdateRequest request)
    {
        var attribute = await _attributeTranslationValidator
            .GetByIdAsync(statusTranslationId);

        if (!attribute.IsSuccess)
            return Result<UpdateResponse>.Fail(attribute.Error);


        var validation = await
             ValidateUpdatesAsync(attribute.Value!, request);

        if (!validation.IsSuccess)
            return Result<UpdateResponse>.Fail(validation.Error);


        var updatedCount = AttributeDefinitionTranslationUpdater
            .ApplyUpdates(attribute.Value!, request);

        var result = new UpdateResponse(
            updatedCount,
            DateTime.UtcNow
        );

        if (updatedCount == 0)
            return Result<UpdateResponse>.Success(result);

        await _unitOfWork.BeginTransactionAsync();
        try
        {
            _attributeRepository.Update(attribute.Value!);

            await _unitOfWork.CommitAsync();
        }
        catch
        {
            await _unitOfWork.RollbackAsync();
            throw;
        }

        return Result<UpdateResponse>.Success(result);
    }
}