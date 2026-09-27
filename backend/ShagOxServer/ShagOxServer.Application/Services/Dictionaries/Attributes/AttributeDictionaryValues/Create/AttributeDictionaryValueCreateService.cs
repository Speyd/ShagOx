using Microsoft.Extensions.Logging;
using ShagOxServer.Application.DTOs.Base.Responses;
using ShagOxServer.Application.DTOs.Dictionaries.Attributes.AttributeDictionaryValues.Create;
using ShagOxServer.Application.Interfaces.Persistences;
using ShagOxServer.Application.Interfaces.Repositories.Base;
using ShagOxServer.Application.Interfaces.Services.Dictionaries.Attributes.AttributeDictionaryValues.Create;
using ShagOxServer.Application.Resources.EntityErrors;
using ShagOxServer.Application.Services.Dictionaries.Attributes.AttributeDictionaryValues.Validator;
using ShagOxServer.Domain.Entities.Dictionaries.Attributes;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Services.Dictionaries.Attributes.AttributeDictionaryValues.Create;
public class AttributeDictionaryValueCreateService
    : IAttributeDictionaryValueCreateService
{
    private readonly IRepository<AttributeDictionaryValue> _valueRepository;
    private readonly AttributeDictionaryValueValidator _valueValidator;

    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<AttributeDictionaryValueCreateService> _logger;


    public AttributeDictionaryValueCreateService(
        IRepository<AttributeDictionaryValue> valueRepository,
        AttributeDictionaryValueValidator valueValidator,
        IUnitOfWork unitOfWork,
        ILogger<AttributeDictionaryValueCreateService> logger)
    {
        _valueRepository = valueRepository;
        _valueValidator = valueValidator;
        _unitOfWork = unitOfWork;
        _logger = logger;
    }


    public async Task<Result<CreateResponse>> CreateAsync(
        AttributeDictionaryValueCreateRequest request)
    {
        var valueExists = await _valueValidator
            .NotExistsAsync(request.DictionaryId, request.Code);

        if (!valueExists.IsSuccess)
            return Result<CreateResponse>.Fail(valueExists.Error);


        var value = AttributeDictionaryValueCreater
            .Create(request);

        await _unitOfWork.BeginTransactionAsync();

        try
        {
            _valueRepository.Add(value);

            await _unitOfWork.CommitAsync();
        }
        catch (Exception ex)
        {
            await _unitOfWork.RollbackAsync();

            _logger.LogError(
                ex,
                "Failed to create attribute dictionary value. " +
                "DictionaryId: {DictionaryId} | Code: {Code}",
                request.DictionaryId,
                request.Code);

            return Result<CreateResponse>.Fail(
                EntityErrorResources.AttributeDictionaryValueCreateFailed);
        }

        return Result<CreateResponse>.Success(
            new CreateResponse(
                value.Id,
                DateTime.UtcNow
        ));
    }
}