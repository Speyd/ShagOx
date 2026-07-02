using ShagOxServer.Application.DTOs.Location.Regions.Delete;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Interfaces.Location.Regions.Delete;
public interface IRegionDeleteService
{
    Task<Result<RegionDeleteResponse>> DeleteRegionAsync(
       int id );
}
