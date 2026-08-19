using ShagOxServer.Application.DTOs.Common.Responses;
using ShagOxServer.Application.Interfaces.Persistences;
using ShagOxServer.Application.Interfaces.Repositories.Base;
using ShagOxServer.Application.Interfaces.Services.Advertisements.Statuses.Translations.Delete;
using ShagOxServer.Application.Services.Advertisements.Statuses.Translations.Validator;
using ShagOxServer.Domain.Entities.Advertisements.Translations;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Services.Advertisements.Statuses.Translations.Delete;
public class StatusTranslationDeleteService
    : IStatusTranslationDeleteService
{
    private readonly IRepository<StatusTranslation> _statusRepository;
    private readonly StatusTranslationValidator _statusValidator;

    private readonly IUnitOfWork _unitOfWork;


    public StatusTranslationDeleteService(
        IRepository<StatusTranslation> statusRepository,
        StatusTranslationValidator statusValidator,
        IUnitOfWork unitOfWork)
    {
        _statusRepository = statusRepository;
        _statusValidator = statusValidator;
        _unitOfWork = unitOfWork;
    }


    public async Task<Result<DeleteResponse>> DeleteAsync(
        int id)
    {
        var status = await _statusValidator
            .GetByIdAsync(id);

        if (!status.IsSuccess)
            return Result<DeleteResponse>.Fail(status.Error);

        await _unitOfWork.BeginTransactionAsync();

        try
        {
            _statusRepository.Delete(status.Value!);

            await _unitOfWork.CommitAsync();
        }
        catch
        {
            await _unitOfWork.RollbackAsync();
            throw;
        }

        return Result<DeleteResponse>.Success(
           new DeleteResponse(
               status.Value!.Id,
               DateTime.UtcNow
           )
       );
    }
}