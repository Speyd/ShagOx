using Microsoft.Extensions.Logging;
using ShagOxServer.Application.DTOs.Base.Responses;
using ShagOxServer.Application.Interfaces.Persistences;
using ShagOxServer.Application.Interfaces.Repositories.Base;
using ShagOxServer.Application.Interfaces.Services.Advertisements.Statuses.Delete;
using ShagOxServer.Application.Resources.EntityErrors;
using ShagOxServer.Application.Services.Advertisements.Statuses.Mapping;
using ShagOxServer.Application.Services.Advertisements.Statuses.Validator;
using ShagOxServer.Application.Services.Base.Delete;
using ShagOxServer.Application.Services.Caches.Invalidations.Advertisements;
using ShagOxServer.Domain.Entities.Advertisements;
using ShagOxServer.SharedKernel.Abstractions.Results;
using System.Net.NetworkInformation;

namespace ShagOxServer.Application.Services.Advertisements.Statuses.Delete;
public class StatusDeleteService
    : BaseDeleteService<Status>
    , IStatusDeleteService
{
    private readonly IRepository<Status> _statusRepository;
    private readonly StatusValidator _statusValidator;

    private readonly StatusInvalidationService _statusInvalid;

    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<StatusDeleteService> _logger;


    public StatusDeleteService(
        IRepository<Status> statusRepository,
        StatusValidator statusValidator,
        StatusInvalidationService statusInvalid,
        IUnitOfWork unitOfWork,
        ILogger<StatusDeleteService> logger
    )
        : base(statusValidator, unitOfWork, logger)
    {
        _statusRepository = statusRepository;
        _statusValidator = statusValidator;
        _statusInvalid = statusInvalid;
         _unitOfWork = unitOfWork;
        _logger = logger;
    }


    public async Task<Result<DeleteResponse>> DeleteAsync(
        long id,
        CancellationToken cancellationToken = default)
    {
        var status = await _statusValidator
            .GetByIdAsync(id, cancellationToken);

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

        await InvalidateCache(status.Value!);

        return Result<DeleteResponse>.Success(
           new DeleteResponse(
               status.Value!.Id,
               DateTime.UtcNow
           )
       );
    }

    public override async Task InvalidateCache(
        Status entity)
    {
        await _statusInvalid.InvalidateDeleteAsync(
           StatusCacheMapper.ToInfo(entity));
    }
}
