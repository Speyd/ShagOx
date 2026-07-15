using ShagOxServer.Application.DTOs.Location.Regions.Delete;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Interfaces.Services.Location.Regions.Delete;
public interface IRegionDeleteService
{
    Task<Result<RegionDeleteResponse>> DeleteAsync(
       int id );
}