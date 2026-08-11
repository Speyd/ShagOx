using ShagOxServer.Application.Interfaces.Repositories.Advertisements.Statuses;
using ShagOxServer.Application.Interfaces.Repositories.Base;
using ShagOxServer.Domain.Entities.Advertisements;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Services.Advertisements.Statuses.Validator;
public class StatusValidator
{
    private readonly IRepository<Status> _statusRepository;
    private readonly IStatusExistsRepository _statusExistsRepository;


    public StatusValidator(
        IRepository<Status> statusRepository,
        IStatusExistsRepository statusExistsRepository)
    {
        _statusRepository = statusRepository;
        _statusExistsRepository = statusExistsRepository;
    }


    public async Task<Result<Status>> GetByIdAsync(
        int statusId)
    {
        var status = await _statusRepository.GetByIdAsync(statusId);
        if (status is null)
            return Result<Status>.NotFound("Status");

        return Result<Status>.Success(status);
    }

    public async Task<Result<bool>> ExistsByIdAsync(
        int statusId)
    {
        if (!await _statusExistsRepository.ExistsByIdAsync(statusId))
            return Result<bool>.NotFound("Status");

        return Result<bool>.Success(true);
    }

    public async Task<Result<bool>> NotExistsByIdAsync(
        int statusId)
    {
        if (await _statusExistsRepository.ExistsByIdAsync(statusId))
            return Result<bool>.AlreadyExists("Status");

        return Result<bool>.Success(true);
    }

    public async Task<Result<bool>> ExistsByCodeAsync(
        string code)
    {
        if (!await _statusExistsRepository.ExistsByCodeAsync(code))
            return Result<bool>.NotFound("Status");

        return Result<bool>.Success(true);
    }

    public async Task<Result<bool>> NotExistsByCodeAsync(
        string code)
    {
        if (await _statusExistsRepository.ExistsByCodeAsync(code))
            return Result<bool>.AlreadyExists("Status");

        return Result<bool>.Success(true);
    }

    public async Task<Result<bool>> ExistsByNameAsync(
        string name)
    {
        if (!await _statusExistsRepository.ExistsByNameAsync(name))
            return Result<bool>.NotFound("Status");

        return Result<bool>.Success(true);
    }

    public async Task<Result<bool>> NotExistsByNameAsync(
        string name)
    {
        if (await _statusExistsRepository.ExistsByNameAsync(name))
            return Result<bool>.AlreadyExists("Status");

        return Result<bool>.Success(true);
    }
}