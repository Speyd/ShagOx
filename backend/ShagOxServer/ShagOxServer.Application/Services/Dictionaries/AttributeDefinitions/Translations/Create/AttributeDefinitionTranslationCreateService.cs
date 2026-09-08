using ShagOxServer.Application.DTOs.Base.Responses;
using ShagOxServer.Application.DTOs.Dictionaries.AttributeDefinitions.Translations.Create;
using ShagOxServer.Application.Interfaces.Persistences;
using ShagOxServer.Application.Interfaces.Repositories.Base;
using ShagOxServer.Application.Interfaces.Services.Dictionaries.AttributeDefinitions.Translations.Create;
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


    public AttributeDefinitionTranslationCreateService(
        IRepository<AttributeDefinitionTranslation> attributeRepository,
        AttributeDefinitionTranslationValidator attributeValidator,
        IUnitOfWork unitOfWork)
    {
        _attributeRepository = attributeRepository;
        _attributeValidator = attributeValidator;

        _unitOfWork = unitOfWork;
    }


    public async Task<Result<CreateResponse>> CreateAsync(
        AttributeDefinitionTranslationCreateRequest request)
    {
        var codeValidation = await _attributeValidator
            .NotExistsAsync(request.AttributeId, request.Language);

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
        catch
        {
            await _unitOfWork.RollbackAsync();
            throw;
        }

        return Result<CreateResponse>.Success(
            new CreateResponse(
                attribute.Id,
                DateTime.UtcNow
        ));
    }
}