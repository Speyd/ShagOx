using Microsoft.Extensions.Logging;
using ShagOxServer.Application.DTOs.Base.Responses;
using ShagOxServer.Application.DTOs.Dictionaries.Attributes.AttributeDictionaryValues.Update;
using ShagOxServer.Application.Interfaces.Persistences;
using ShagOxServer.Application.Interfaces.Repositories.Base;
using ShagOxServer.Application.Interfaces.Services.Dictionaries.Attributes.AttributeDictionaryValues.Update;
using ShagOxServer.Application.Resources.EntityErrors;
using ShagOxServer.Application.Services.Dictionaries.Attributes.AttributeDictionaries.Validator;
using ShagOxServer.Application.Services.Dictionaries.Attributes.AttributeDictionaryValues.Validator;
using ShagOxServer.Domain.Entities.Dictionaries.Attributes;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Services.Dictionaries.Attributes.AttributeDictionaryValues.Update;
public class AttributeDictionaryValueUpdateService
    : IAttributeDictionaryValueUpdateService
{
    private readonly IRepository<AttributeDictionaryValue> _valueRepository;
    private readonly AttributeDictionaryValueValidator _valueValidator;

    private readonly AttributeDictionaryValidator _dictionaryValidator;

    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<AttributeDictionaryValueUpdateService> _logger;


    public AttributeDictionaryValueUpdateService(
        IRepository<AttributeDictionaryValue> valueRepository,
        AttributeDictionaryValueValidator valueValidator,
        AttributeDictionaryValidator dictionaryValidator,
        IUnitOfWork unitOfWork,
        ILogger<AttributeDictionaryValueUpdateService> logger)
    {
        _valueRepository = valueRepository;
        _valueValidator = valueValidator;
        _dictionaryValidator = dictionaryValidator;
        _unitOfWork = unitOfWork;
        _logger = logger;
    }


    public async Task<Result<UpdateResponse>> UpdateAsync(
        long valueId,
        AttributeDictionaryValueUpdateRequest request)
    {
        var value = await _valueValidator
            .GetByIdAsync(valueId);
        if (!value.IsSuccess)
            return Result<UpdateResponse>.Fail(value.Error);

        var validation = await ValidateUpdatesAsync(value.Value!, request);
        if (!validation.IsSuccess)
            return Result<UpdateResponse>.Fail(validation.Error);

        var updatedCount = AttributeDictionaryValueUpdater
            .ApplyUpdates(value.Value!, request);

        var result = new UpdateResponse(
            updatedCount,
            DateTime.UtcNow
        );

        if (updatedCount == 0)
            return Result<UpdateResponse>.Success(result);

        await _unitOfWork.BeginTransactionAsync();

        try
        {
            _valueRepository.Update(value.Value!);

            await _unitOfWork.CommitAsync();
        }
        catch (Exception ex)
        {
            await _unitOfWork.RollbackAsync();

            _logger.LogError(
               ex,
               "Failed to update attribute dictionary value. " +
               "Id: {Id} | DictionaryId: {DictionaryId} | Code: {Code}",
               valueId,
               request.DictionaryId,
               request.Code);

            return Result<UpdateResponse>.Fail(
                EntityErrorResources.AttributeDictionaryValueUpdateFailed);
        }

        return Result<UpdateResponse>.Success(result);
    }

    private async Task<Result<bool>> ValidateUpdatesAsync(
        AttributeDictionaryValue value,
        AttributeDictionaryValueUpdateRequest request)
    {
        var code = request.Code ?? value.Code;
        var dictionaryId = request.DictionaryId ?? value.DictionaryId;

        if(dictionaryId != value.DictionaryId)
        {
            var dictionaryExists = await _dictionaryValidator
                .ExistsByIdAsync(dictionaryId);

            if (!dictionaryExists.IsSuccess)
                return Result<bool>.Fail(dictionaryExists.Error);
        }

        if (code != value.Code ||
            dictionaryId != value.DictionaryId)
        {
            var valueExists = await _valueValidator
                .NotExistsAsync(dictionaryId, code);

            if (!valueExists.IsSuccess)
                return Result<bool>.Fail(valueExists.Error);
        }

        return Result<bool>.Success(true);
    }
}