using Microsoft.Extensions.Logging;
using ShagOxServer.Application.DTOs.Base.Responses;
using ShagOxServer.Application.DTOs.Dictionaries.Attributes.AttributeDefinitions.Translations.Create;
using ShagOxServer.Application.Interfaces.Persistences;
using ShagOxServer.Application.Interfaces.Repositories.Base;
using ShagOxServer.Application.Interfaces.Services.Dictionaries.Attributes.AttributeDefinitions.Translations.Create;
using ShagOxServer.Application.Resources.EntityErrors;
using ShagOxServer.Application.Services.Base.Translations.Query.Mapping;
using ShagOxServer.Application.Services.Caches.Invalidations.Dictionaries.Attributes.Translation;
using ShagOxServer.Application.Services.Dictionaries.Attributes.AttributeDefinitions.Translations.Validator;
using ShagOxServer.Domain.Entities.Dictionaries.Attributes.Translations;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Services.Dictionaries.Attributes.AttributeDefinitions.Translations.Create;
public class AttributeDefinitionTranslationCreateService
    : IAttributeDefinitionTranslationCreateService
{
    private readonly IRepository<AttributeDefinitionTranslation> _attributeRepository;
    private readonly AttributeDefinitionTranslationValidator _attributeValidator;

    private readonly AttributeDefinitionTranslationInvalidationService _trnsInvalid;

    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<AttributeDefinitionTranslationCreateService> _logger;


    public AttributeDefinitionTranslationCreateService(
        IRepository<AttributeDefinitionTranslation> attributeRepository,
        AttributeDefinitionTranslationValidator attributeValidator,
        AttributeDefinitionTranslationInvalidationService trnsInvalid,
        IUnitOfWork unitOfWork,
        ILogger<AttributeDefinitionTranslationCreateService> logger)
    {
        _attributeRepository = attributeRepository;
        _attributeValidator = attributeValidator;
        _trnsInvalid = trnsInvalid;

        _unitOfWork = unitOfWork;
        _logger = logger;
    }


    public async Task<Result<CreateResponse>> CreateAsync(
        AttributeDefinitionTranslationCreateRequest request)
    {
        var codeValidation = await _attributeValidator
            .NotExistsAsync(request.TranslatableId, request.Language);

        if (!codeValidation.IsSuccess)
            return Result<CreateResponse>.Fail(codeValidation.Error);


        var attribute = AttributeDefinitionTranslationCreater
            .Create(request);

        await _unitOfWork.BeginTransactionAsync();
        try
        {
            _attributeRepository.Add(attribute);

            await _unitOfWork.CommitAsync();
        }
        catch(Exception ex)
        {
            await _unitOfWork.RollbackAsync();

            _logger.LogError(
               ex,
               "Failed to create attribute definition translation. " +
               "TranslatableId: {TranslatableId}",
               request.TranslatableId);

            return Result<CreateResponse>.Fail(
                EntityErrorResources.AttributeDefinitionTranslationCreateFailed);
        }

        await _trnsInvalid.InvalidateCreateAsync(
            BaseTranslationCacheMapper.ToInfo(attribute));

        return Result<CreateResponse>.Success(
            new CreateResponse(
                attribute.Id,
                DateTime.UtcNow
        ));
    }
}