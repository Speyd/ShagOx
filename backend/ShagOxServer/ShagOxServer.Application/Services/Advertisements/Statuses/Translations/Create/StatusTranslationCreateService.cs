using ShagOxServer.Application.DTOs.Advertisements.Statuses.Translations.Create;
using ShagOxServer.Application.DTOs.Common.Responses;
using ShagOxServer.Application.Interfaces.Persistences;
using ShagOxServer.Application.Interfaces.Repositories.Base;
using ShagOxServer.Application.Interfaces.Services.Advertisements.Statuses.Translations.Create;
using ShagOxServer.Application.Services.Advertisements.Statuses.Translations.Validator;
using ShagOxServer.Domain.Entities.Advertisements.Translations;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Services.Advertisements.Statuses.Translations.Create;
public class StatusTranslationCreateService
    : IStatusTranslationCreateService
{
    private readonly IRepository<StatusTranslation> _statusRepository;
    private readonly StatusTranslationValidator _statusValidator;

    private readonly IUnitOfWork _unitOfWork;


    public StatusTranslationCreateService(
        IRepository<StatusTranslation> statusRepository,
        StatusTranslationValidator statusValidator,
        IUnitOfWork unitOfWork)
    {
        _statusRepository = statusRepository;
        _statusValidator = statusValidator;

        _unitOfWork = unitOfWork;
    }


    public async Task<Result<CreateResponse>> CreateAsync(
        StatusTranslationCreateRequest request)
    {
        var codeValidation = await _statusValidator
            .NotExistsAsync(request.StatusId, request.Language);

        if (!codeValidation.IsSuccess)
            return Result<CreateResponse>.Fail(codeValidation.Error);


        var status = StatusTranslationCreater.Create(request);

        await _unitOfWork.BeginTransactionAsync();
        try
        {
            _statusRepository.Add(status);

            await _unitOfWork.CommitAsync();
        }
        catch
        {
            await _unitOfWork.RollbackAsync();
            throw;
        }

        return Result<CreateResponse>.Success(
            new CreateResponse(
                status.Id,
                DateTime.UtcNow
        ));
    }
}