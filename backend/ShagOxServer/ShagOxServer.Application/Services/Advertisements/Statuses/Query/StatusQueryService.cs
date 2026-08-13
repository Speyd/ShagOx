using ShagOxServer.Application.DTOs.Advertisements.Statuses;
using ShagOxServer.Application.Interfaces.Repositories.Advertisements.Statuses;
using ShagOxServer.Application.Interfaces.Services.Advertisements.Statuses.Query;
using ShagOxServer.Application.Services.Advertisements.Statuses.Mapping;
using ShagOxServer.Domain.Filters.Advertisements;
using ShagOxServer.SharedKernel.Abstractions.Paginations;
using ShagOxServer.SharedKernel.Abstractions.Results;
using ShagOxServer.SharedKernel.Abstractions.Results.Extensions;

namespace ShagOxServer.Application.Services.Advertisements.Statuses.Query;
public class StatusQueryService 
    : IStatusQueryService
{
    private readonly IStatusQueryRepository _statusRepository;


    public StatusQueryService(
        IStatusQueryRepository statusRepository)
    {
        _statusRepository = statusRepository;
    }


    public async Task<Result<StatusDto>> GetByIdAsync(
        int id)
    {
        var status = await _statusRepository
            .GetByIdAsync(id);

        return status.ToResult(StatusMapper.ToDto);
    }

    public async Task<Result<PagedResult<StatusDto>>> GetPagedAsync(
        PaginationParams pagination)
    {
        var statuses = await _statusRepository
            .GetPagedAsync(pagination);

        return statuses.ToResultPaged(StatusMapper.ToDto);
    }

    public async Task<Result<PagedResult<StatusDto>>> Search(
        StatusSearchFilter filter,
        PaginationParams pagination)
    {
        var statuses = await _statusRepository
            .Search(filter, pagination);

        return statuses.ToResultPaged(StatusMapper.ToDto);
    }
}