using ShagOxServer.SharedKernel.Results;
using ShagOxServer.Application.DTOs.Specification.Images;

namespace ShagOxServer.Application.Interfaces.Specification.Images.Query;
public interface IImageQueryService
{
    Task<Result<ImageDto>> GetByIdAsync(int id);
}
