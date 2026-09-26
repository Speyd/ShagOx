using Microsoft.Extensions.Logging;
using ShagOxServer.Application.DTOs.Base.Responses;
using ShagOxServer.Application.DTOs.Dictionaries.Attributes.AttributeDictionaries.Update;
using ShagOxServer.Application.Interfaces.Persistences;
using ShagOxServer.Application.Interfaces.Repositories.Base;
using ShagOxServer.Application.Interfaces.Services.Dictionaries.Attributes.AttributeDictionaries.Update;
using ShagOxServer.Application.Resources.EntityErrorResourcess;
using ShagOxServer.Application.Services.Dictionaries.Attributes.AttributeDictionaries.Validator;
using ShagOxServer.Domain.Entities.Dictionaries.Attributes;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Services.Dictionaries.Attributes.AttributeDictionaries.Update;
public class AttributeDictionaryUpdateService
    : IAttributeDictionaryUpdateService
{
    private readonly IRepository<AttributeDictionary> _dictionaryRepository;
    private readonly AttributeDictionaryValidator _dictionaryValidator;

    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<AttributeDictionaryUpdateService> _logger;


    public AttributeDictionaryUpdateService(
        IRepository<AttributeDictionary> dictionaryRepository,
        AttributeDictionaryValidator dictionaryValidator,
        IUnitOfWork unitOfWork,
        ILogger<AttributeDictionaryUpdateService> logger)
    {
        _dictionaryRepository = dictionaryRepository;
        _dictionaryValidator = dictionaryValidator;
        _unitOfWork = unitOfWork;
        _logger = logger;
    }


    public async Task<Result<UpdateResponse>> UpdateAsync(
        long dictionaryId,
        AttributeDictionaryUpdateRequest request)
    {
        var dictionary = await _dictionaryValidator
            .GetByIdAsync(dictionaryId);
        if (!dictionary.IsSuccess)
            return Result<UpdateResponse>.Fail(dictionary.Error);

        var validation = await ValidateUpdatesAsync(request);
        if (!validation.IsSuccess)
            return Result<UpdateResponse>.Fail(validation.Error);

        var updatedCount = AttributeDictionaryUpdater
            .ApplyUpdates(dictionary.Value!, request);

        var result = new UpdateResponse(
            updatedCount,
            DateTime.UtcNow
        );

        if (updatedCount == 0)
            return Result<UpdateResponse>.Success(result);

        await _unitOfWork.BeginTransactionAsync();

        try
        {
            _dictionaryRepository.Update(dictionary.Value!);

            await _unitOfWork.CommitAsync();
        }
        catch (Exception ex)
        {
            await _unitOfWork.RollbackAsync();

            _logger.LogError(
               ex,
               "Failed to update attribute dictionary. Id: {Id}",
               dictionaryId);

            return Result<UpdateResponse>.Fail(
                EntityErrorResources.AttributeDictionaryUpdateFailed);
        }

        return Result<UpdateResponse>.Success(result);
    }

    private async Task<Result<bool>> ValidateUpdatesAsync(
        AttributeDictionaryUpdateRequest request)
    {
        if(request.Code is not null)
        {
            var codeExists = await _dictionaryValidator
                .NotExistsByCodeAsync(request.Code);

            if(!codeExists.IsSuccess)
                return Result<bool>.Fail(codeExists.Error);
        }

        return Result<bool>.Success(true);
    }
}