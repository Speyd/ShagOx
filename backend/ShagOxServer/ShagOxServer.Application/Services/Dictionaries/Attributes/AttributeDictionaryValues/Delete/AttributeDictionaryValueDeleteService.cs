using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using ShagOxServer.Application.DTOs.Base.Responses;
using ShagOxServer.Application.Interfaces.Persistences;
using ShagOxServer.Application.Interfaces.Repositories.Base;
using ShagOxServer.Application.Interfaces.Services.Dictionaries.Attributes.AttributeDictionaryValues.Delete;
using ShagOxServer.Application.Resources.EntityErrors;
using ShagOxServer.Application.Services.Caches.Invalidations.Dictionaries.Attributes;
using ShagOxServer.Application.Services.Dictionaries.Attributes.AttributeDictionaryValues.Mapping;
using ShagOxServer.Application.Services.Dictionaries.Attributes.AttributeDictionaryValues.Validator;
using ShagOxServer.Domain.Entities.Dictionaries.Attributes;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Services.Dictionaries.Attributes.AttributeDictionaryValues.Delete;
public class AttributeDictionaryValueDeleteService
    : IAttributeDictionaryValueDeleteService
{
    private readonly IRepository<AttributeDictionaryValue> _valueRepository;
    private readonly AttributeDictionaryValueValidator _valueValidator;

    private readonly AttributeDictionaryValueInvalidationService _valueInvalidation;

    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<AttributeDictionaryValueDeleteService> _logger;


    public AttributeDictionaryValueDeleteService(
        IRepository<AttributeDictionaryValue> valueRepository,
        AttributeDictionaryValueValidator valueValidator,
        AttributeDictionaryValueInvalidationService valueInvalidation,
        IUnitOfWork unitOfWork,
        ILogger<AttributeDictionaryValueDeleteService> logger)
    {
        _valueRepository = valueRepository;
        _valueValidator = valueValidator;
        _valueInvalidation = valueInvalidation;
        _unitOfWork = unitOfWork;
        _logger = logger;
    }


    public async Task<Result<DeleteResponse>> DeleteAsync(
        long id)
    {
        var value = await _valueValidator
            .GetByIdAsync(id);

        if (!value.IsSuccess)
            return Result<DeleteResponse>.Fail(value.Error);

        await _unitOfWork.BeginTransactionAsync();

        try
        {
            _valueRepository.Delete(value.Value!);

            await _unitOfWork.CommitAsync();
        }
        catch (Exception ex)
        {
            await _unitOfWork.RollbackAsync();

            _logger.LogError(
               ex,
               "Failed to delete attribute dictionary value. Id: {Id}",
               id);

            return Result<DeleteResponse>.Fail(
                EntityErrorResources.AttributeDictionaryValueDeleteFailed);
        }

        await _valueInvalidation.InvalidateDeleteAsync(
            AttributeDictionaryValueCacheMapper.ToInfo(value.Value!));

        return Result<DeleteResponse>.Success(
           new DeleteResponse(
               value.Value!.Id,
               DateTime.UtcNow
           )
       );
    }
}