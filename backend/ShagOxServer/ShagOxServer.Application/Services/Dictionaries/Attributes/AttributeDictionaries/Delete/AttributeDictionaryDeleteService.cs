using Microsoft.Extensions.Logging;
using ShagOxServer.Application.DTOs.Base.Responses;
using ShagOxServer.Application.Interfaces.Persistences;
using ShagOxServer.Application.Interfaces.Repositories.Base;
using ShagOxServer.Application.Interfaces.Services.Dictionaries.Attributes.AttributeDictionaries.Delete;
using ShagOxServer.Application.Resources.EntityErrors;
using ShagOxServer.Application.Services.Caches.Invalidations.Dictionaries.Attributes;
using ShagOxServer.Application.Services.Dictionaries.Attributes.AttributeDictionaries.Validator;
using ShagOxServer.Domain.Entities.Dictionaries.Attributes;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Services.Dictionaries.Attributes.AttributeDictionaries.Delete;
public class AttributeDictionaryDeleteService
    : IAttributeDictionaryDeleteService
{
    private readonly IRepository<AttributeDictionary> _dictionaryRepository;
    private readonly AttributeDictionaryValidator _dictionaryValidator;

    private readonly AttributeDictionaryInvalidationService _dictInvalid;

    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<AttributeDictionaryDeleteService> _logger;


    public AttributeDictionaryDeleteService(
        IRepository<AttributeDictionary> dictionaryRepository,
        AttributeDictionaryValidator dictionaryValidator,
        AttributeDictionaryInvalidationService dictInvalid,
        IUnitOfWork unitOfWork,
        ILogger<AttributeDictionaryDeleteService> logger)
    {
        _dictionaryRepository = dictionaryRepository;
        _dictionaryValidator = dictionaryValidator;
        _dictInvalid = dictInvalid;
        _unitOfWork = unitOfWork;
        _logger = logger;
    }


    public async Task<Result<DeleteResponse>> DeleteAsync(
        long id)
    {
        var dictionary = await _dictionaryValidator
            .GetByIdAsync(id);

        if (!dictionary.IsSuccess)
            return Result<DeleteResponse>.Fail(dictionary.Error);

        await _unitOfWork.BeginTransactionAsync();

        try
        {
            _dictionaryRepository.Delete(dictionary.Value!);

            await _unitOfWork.CommitAsync();
        }
        catch (Exception ex)
        {
            await _unitOfWork.RollbackAsync();

            _logger.LogError(
               ex,
               "Failed to delete attribute dictionary. Id: {Id}",
               id);

            return Result<DeleteResponse>.Fail(
                EntityErrorResources.AttributeDictionaryDeleteFailed);
        }

        await _dictInvalid.InvalidateDeleteAsync(id);

        return Result<DeleteResponse>.Success(
           new DeleteResponse(
               dictionary.Value!.Id,
               DateTime.UtcNow
           )
       );
    }
}