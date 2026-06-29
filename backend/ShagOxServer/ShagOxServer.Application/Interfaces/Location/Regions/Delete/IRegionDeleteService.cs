using ShagOxServer.Application.Common.Results;
using ShagOxServer.Application.DTOs.Location.Regions.Delete;

namespace ShagOxServer.Application.Interfaces.Location.Regions.Delete;
public interface IRegionDeleteService
{
    Task<Result<RegionDeleteResponse>> DeleteRegionAsync(
       int id );
}
