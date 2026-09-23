using Microsoft.Extensions.Logging;
using ShagOxServer.Application.DTOs.Advertisements.Statuses.Translations.Create;
using ShagOxServer.Application.DTOs.Base.Responses;
using ShagOxServer.Application.Interfaces.Persistences;
using ShagOxServer.Application.Interfaces.Repositories.Base;
using ShagOxServer.Application.Interfaces.Services.Advertisements.Statuses.Translations.Create;
using ShagOxServer.Application.Services.Advertisements.Statuses.Translations.Validator;
using ShagOxServer.Domain.Entities.Advertisements;
using ShagOxServer.Domain.Entities.Advertisements.Translations;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Services.Advertisements.Statuses.Translations.Create;
public class StatusTranslationCreateService
    : IStatusTranslationCreateService
{
    private readonly IRepository<StatusTranslation> _statusRepository;
    private readonly StatusTranslationValidator _statusValidator;

    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<StatusTranslationCreateService> _logger;

    public StatusTranslationCreateService(
        IRepository<StatusTranslation> statusRepository,
        StatusTranslationValidator statusValidator,
        IUnitOfWork unitOfWork,
        ILogger<StatusTranslationCreateService> logger)
    {
        _statusRepository = statusRepository;
        _statusValidator = statusValidator;

        _unitOfWork = unitOfWork;
        _logger = logger;
    }


    public async Task<Result<CreateResponse>> CreateAsync(
        StatusTranslationCreateRequest request)
    {
        var codeValidation = await _statusValidator
            .NotExistsAsync(request.TranslatableId, request.Language);

        if (!codeValidation.IsSuccess)
            return Result<CreateResponse>.Fail(codeValidation.Error);


        var status = StatusTranslationCreater.Create(request);

        await _unitOfWork.BeginTransactionAsync();
        try
        {
            _statusRepository.Add(status);

            await _unitOfWork.CommitAsync();
        }
        catch(Exception ex)
        {
            await _unitOfWork.RollbackAsync();

            _logger.LogError(
                ex,
                "Failed to create status translation. " +
                "TranslatableId: {TranslatableId}",
                request.TranslatableId);

            return Result<CreateResponse>
                .Fail("Failed to create status translation.");
        }

        return Result<CreateResponse>.Success(
            new CreateResponse(
                status.Id,
                DateTime.UtcNow
        ));
    }
}