using ShagOxServer.Application.DTOs.Advertisements.Statuses.Translations;
using ShagOxServer.Application.Interfaces.Repositories.Advertisements.Statuses.Translations;
using ShagOxServer.Application.Interfaces.Services.Advertisements.Statuses.Translations.Query;
using ShagOxServer.Application.Services.Advertisements.Statuses.Translations.Mapping;
using ShagOxServer.Domain.Filters.Advertisements.Translations;
using ShagOxServer.SharedKernel.Abstractions.Paginations;
using ShagOxServer.SharedKernel.Abstractions.Results;
using ShagOxServer.SharedKernel.Abstractions.Results.Extensions;

namespace ShagOxServer.Application.Services.Advertisements.Statuses.Translations.Query;
public class StatusTranslationQueryService
    : IStatusTranslationQueryService
{
    private readonly IStatusTranslationQueryRepository _statusRepository;


    public StatusTranslationQueryService(
        IStatusTranslationQueryRepository statusRepository)
    {
        _statusRepository = statusRepository;
    }


    public async Task<Result<StatusTranslationDto>> GetByIdAsync(
        int id)
    {
        var status = await _statusRepository
            .GetByIdAsync(id);

        return status.ToResult(StatusTranslationMapper.ToDto);
    }

    public async Task<Result<PagedResult<StatusTranslationDto>>> GetPagedAsync(
        PaginationParams pagination)
    {
        var statuses = await _statusRepository
            .GetPagedAsync(pagination);

        return statuses.ToResultPaged(StatusTranslationMapper.ToDto);
    }

    public async Task<Result<PagedResult<StatusTranslationDto>>> Search(
        StatusTranslationSearchFilter filter,
        PaginationParams pagination)
    {
        var statuses = await _statusRepository
            .Search(filter, pagination);

        return statuses.ToResultPaged(StatusTranslationMapper.ToDto);
    }
}