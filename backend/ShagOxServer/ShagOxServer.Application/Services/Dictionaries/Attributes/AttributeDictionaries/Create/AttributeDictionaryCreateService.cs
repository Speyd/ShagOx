using Microsoft.Extensions.Logging;
using ShagOxServer.Application.DTOs.Base.Responses;
using ShagOxServer.Application.DTOs.Dictionaries.Attributes.AttributeDictionaries.Create;
using ShagOxServer.Application.Interfaces.Persistences;
using ShagOxServer.Application.Interfaces.Repositories.Base;
using ShagOxServer.Application.Interfaces.Services.Dictionaries.Attributes.AttributeDictionaries.Create;
using ShagOxServer.Application.Resources.EntityErrorResourcess;
using ShagOxServer.Application.Services.Dictionaries.Attributes.AttributeDictionaries.Validator;
using ShagOxServer.Domain.Entities.Dictionaries.Attributes;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Services.Dictionaries.Attributes.AttributeDictionaries.Create;
public class AttributeDictionaryCreateService
    : IAttributeDictionaryCreateService
{
    private readonly IRepository<AttributeDictionary> _dictionaryRepository;
    private readonly AttributeDictionaryValidator _dictionaryValidator;

    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<AttributeDictionaryCreateService> _logger;


    public AttributeDictionaryCreateService(
        IRepository<AttributeDictionary> dictionaryRepository,
        AttributeDictionaryValidator dictionaryValidator,
        IUnitOfWork unitOfWork,
        ILogger<AttributeDictionaryCreateService> logger)
    {
        _dictionaryRepository = dictionaryRepository;
        _dictionaryValidator = dictionaryValidator;
        _unitOfWork = unitOfWork;
        _logger = logger;
    }


    public async Task<Result<CreateResponse>> CreateAsync(
        AttributeDictionaryCreateRequest request)
    {
        var codeExists = await _dictionaryValidator
            .NotExistsByCodeAsync(request.Code);

        if (!codeExists.IsSuccess)
            return Result<CreateResponse>.Fail(codeExists.Error);


        var dictionary = AttributeDictionaryCreater.Create(request);

        await _unitOfWork.BeginTransactionAsync();

        try
        {
            _dictionaryRepository.Add(dictionary);

            await _unitOfWork.CommitAsync();
        }
        catch (Exception ex)
        {
            await _unitOfWork.RollbackAsync();

            _logger.LogError(
                ex,
                "Failed to create attribute dictionary. " +
                "Code: {Code}",
                request.Code);

            return Result<CreateResponse>.Fail(
                EntityErrorResources.AttributeDictionaryCreateFailed);
        }

        return Result<CreateResponse>.Success(
            new CreateResponse(
                dictionary.Id,
                DateTime.UtcNow
        ));
    }
}