using Microsoft.Extensions.Logging;
using ShagOxServer.Application.DTOs.Base.Responses;
using ShagOxServer.Application.Interfaces.Persistences;
using ShagOxServer.Application.Interfaces.Repositories.Base;
using ShagOxServer.Application.Interfaces.Services.Dictionaries.AttributeDefinitions.Translations.Delete;
using ShagOxServer.Application.Resources.EntityErrorResourcess;
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
    private readonly ILogger<AttributeDefinitionTranslationDeleteService> _logger;


    public AttributeDefinitionTranslationDeleteService(
        IRepository<AttributeDefinitionTranslation> attributeRepository,
        AttributeDefinitionTranslationValidator attributeValidator,
        IUnitOfWork unitOfWork,
        ILogger<AttributeDefinitionTranslationDeleteService> logger)
    {
        _attributeRepository = attributeRepository;
        _attributeValidator = attributeValidator;
        _unitOfWork = unitOfWork;
        _logger = logger;
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
        catch(Exception ex)
        {
            await _unitOfWork.RollbackAsync();

            _logger.LogError(
               ex,
               "Failed to delete attribute definition translation. Id: {Id}",
               id);

            return Result<DeleteResponse>.Fail(
                EntityErrorResources.AttributeDefinitionTranslationDeleteFailed);
        }

        return Result<DeleteResponse>.Success(
           new DeleteResponse(
               region.Value!.Id,
               DateTime.UtcNow
           )
       );
    }
}