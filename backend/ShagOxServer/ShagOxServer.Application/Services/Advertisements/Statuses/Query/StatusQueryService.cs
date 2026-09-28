using ShagOxServer.Application.DTOs.Advertisements.Statuses;
using ShagOxServer.Application.Interfaces.Repositories.Advertisements.Statuses;
using ShagOxServer.Application.Interfaces.Services.Advertisements.Statuses.Query;
using ShagOxServer.Application.Services.Advertisements.Statuses.Mapping;
using ShagOxServer.Application.Services.Base;
using ShagOxServer.Domain.Entities.Advertisements;
using ShagOxServer.Domain.Filters.Advertisements;

namespace ShagOxServer.Application.Services.Advertisements.Statuses.Query;
public class StatusQueryService 
    : BaseQueryService<
        StatusDto,
        Status,
        StatusSearchFilter
        >,
    IStatusQueryService
{
    public StatusQueryService(
        IStatusQueryRepository statusRepository
    )
        : base(statusRepository)
    {
    }


    protected override async Task<StatusDto> ApplyMapperAsync(
        Status entity)
    {
        return StatusMapper.ToDto(entity);
    }
}