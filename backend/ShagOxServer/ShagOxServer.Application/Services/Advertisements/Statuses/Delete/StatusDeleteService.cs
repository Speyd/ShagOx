using Microsoft.Extensions.Logging;
using ShagOxServer.Application.DTOs.Base.Responses;
using ShagOxServer.Application.Interfaces.Persistences;
using ShagOxServer.Application.Interfaces.Repositories.Base;
using ShagOxServer.Application.Interfaces.Services.Advertisements.Statuses.Delete;
using ShagOxServer.Application.Resources.EntityErrorResourcess;
using ShagOxServer.Application.Services.Advertisements.Statuses.Validator;
using ShagOxServer.Domain.Entities.Advertisements;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Services.Advertisements.Statuses.Delete;
public class StatusDeleteService
    : IStatusDeleteService
{
    private readonly IRepository<Status> _statusRepository;
    private readonly StatusValidator _statusValidator;

    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<StatusDeleteService> _logger;


    public StatusDeleteService(
        IRepository<Status> statusRepository,
        StatusValidator statusValidator,
        IUnitOfWork unitOfWork,
        ILogger<StatusDeleteService> logger)
    {
        _statusRepository = statusRepository;
        _statusValidator = statusValidator;
        _unitOfWork = unitOfWork;
        _logger = logger;
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
        catch(Exception ex)
        {
            await _unitOfWork.RollbackAsync();

            _logger.LogError(
                ex,
                "Failed to delete status. Id: {Id}",
                id);

            return Result<DeleteResponse>
                     .Fail(EntityErrorResources.AdvertStatusDeleteFailed);
        }

        return Result<DeleteResponse>.Success(
           new DeleteResponse(
               status.Value!.Id,
               DateTime.UtcNow
           )
       );
    }
}