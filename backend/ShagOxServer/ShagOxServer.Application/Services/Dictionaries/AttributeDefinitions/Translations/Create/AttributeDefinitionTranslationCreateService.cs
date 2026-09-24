using Microsoft.Extensions.Logging;
using ShagOxServer.Application.DTOs.Base.Responses;
using ShagOxServer.Application.DTOs.Dictionaries.AttributeDefinitions.Translations.Create;
using ShagOxServer.Application.Interfaces.Persistences;
using ShagOxServer.Application.Interfaces.Repositories.Base;
using ShagOxServer.Application.Interfaces.Services.Dictionaries.AttributeDefinitions.Translations.Create;
using ShagOxServer.Application.Resources.EntityErrorResourcess;
using ShagOxServer.Application.Services.Dictionaries.AttributeDefinitions.Translations.Validator;
using ShagOxServer.Domain.Entities.Dictionaries.Translations;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Services.Dictionaries.AttributeDefinitions.Translations.Create;
public class AttributeDefinitionTranslationCreateService
    : IAttributeDefinitionTranslationCreateService
{
    private readonly IRepository<AttributeDefinitionTranslation> _attributeRepository;
    private readonly AttributeDefinitionTranslationValidator _attributeValidator;

    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<AttributeDefinitionTranslationCreateService> _logger;


    public AttributeDefinitionTranslationCreateService(
        IRepository<AttributeDefinitionTranslation> attributeRepository,
        AttributeDefinitionTranslationValidator attributeValidator,
        IUnitOfWork unitOfWork,
        ILogger<AttributeDefinitionTranslationCreateService> logger)
    {
        _attributeRepository = attributeRepository;
        _attributeValidator = attributeValidator;

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

        return Result<CreateResponse>.Success(
            new CreateResponse(
                attribute.Id,
                DateTime.UtcNow
        ));
    }
}