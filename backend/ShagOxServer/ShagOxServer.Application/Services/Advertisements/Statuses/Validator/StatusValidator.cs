using ShagOxServer.Application.Interfaces.Repositories.Advertisements.Statuses;
using ShagOxServer.Application.Interfaces.Repositories.Base;
using ShagOxServer.Application.Services.Base;
using ShagOxServer.Domain.Entities.Advertisements;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Services.Advertisements.Statuses.Validator;
public class StatusValidator
    : BaseValidator<Status>
{
    private readonly IStatusExistsRepository _statusExistsRepository;


    public StatusValidator(
        IRepository<Status> statusRepository,
        IStatusExistsRepository statusExistsRepository
    ) : base(statusRepository, statusExistsRepository)
    {
        _statusExistsRepository = statusExistsRepository;
    }


    public async Task<Result<bool>> ExistsByCodeAsync(
        string code)
    {
        if (!await _statusExistsRepository
            .ExistsByCodeAsync(code))
        {
            return Result<bool>
                .NotFound(typeof(Status));
        }

        return Result<bool>.Success(true);
    }

    public async Task<Result<bool>> NotExistsByCodeAsync(
        string code)
    {
        if (await _statusExistsRepository
            .ExistsByCodeAsync(code))
        {
            return Result<bool>
                .AlreadyExists(typeof(Status));
        }

        return Result<bool>.Success(true);
    }
}