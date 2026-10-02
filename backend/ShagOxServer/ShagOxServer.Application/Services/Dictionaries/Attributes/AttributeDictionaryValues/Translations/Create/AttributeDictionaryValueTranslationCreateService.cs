using Microsoft.Extensions.Logging;
using ShagOxServer.Application.DTOs.Base.Responses;
using ShagOxServer.Application.DTOs.Dictionaries.Attributes.AttributeDictionaryValues.Translations.Create;
using ShagOxServer.Application.Interfaces.Persistences;
using ShagOxServer.Application.Interfaces.Repositories.Base;
using ShagOxServer.Application.Interfaces.Services.Dictionaries.Attributes.AttributeDictionaryValues.Translations.Create;
using ShagOxServer.Application.Resources.EntityErrors;
using ShagOxServer.Application.Services.Dictionaries.Attributes.AttributeDictionaryValues.Translations.Validator;
using ShagOxServer.Domain.Entities.Dictionaries.Attributes.Translations;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Services.Dictionaries.Attributes.AttributeDictionaryValues.Translations.Create;
public class AttributeDictionaryValueTranslationCreateService
    : IAttributeDictionaryValueTranslationCreateService
{
    private readonly IRepository<AttributeDictionaryValueTranslation> _attributeRepository;
    private readonly AttributeDictionaryValueTranslationValidator _attributeValidator;

    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<AttributeDictionaryValueTranslationCreateService> _logger;


    public AttributeDictionaryValueTranslationCreateService(
        IRepository<AttributeDictionaryValueTranslation> attributeRepository,
        AttributeDictionaryValueTranslationValidator attributeValidator,
        IUnitOfWork unitOfWork,
        ILogger<AttributeDictionaryValueTranslationCreateService> logger)
    {
        _attributeRepository = attributeRepository;
        _attributeValidator = attributeValidator;

        _unitOfWork = unitOfWork;
        _logger = logger;
    }


    public async Task<Result<CreateResponse>> CreateAsync(
        AttributeDictionaryValueTranslationCreateRequest request)
    {
        var codeValidation = await _attributeValidator
            .NotExistsAsync(request.TranslatableId, request.Language);

        if (!codeValidation.IsSuccess)
            return Result<CreateResponse>.Fail(codeValidation.Error);


        var attribute = AttributeDictionaryValueTranslationCreater
            .Create(request);

        await _unitOfWork.BeginTransactionAsync();
        try
        {
            _attributeRepository.Add(attribute);

            await _unitOfWork.CommitAsync();
        }
        catch (Exception ex)
        {
            await _unitOfWork.RollbackAsync();

            _logger.LogError(
               ex,
               "Failed to create attribute dictionary value translation. " +
               "TranslatableId: {TranslatableId}",
               request.TranslatableId);

            return Result<CreateResponse>.Fail(
                EntityErrorResources.AttributeDictionaryValueTranslationCreateFailed);
        }

        return Result<CreateResponse>.Success(
            new CreateResponse(
                attribute.Id,
                DateTime.UtcNow
        ));
    }
}