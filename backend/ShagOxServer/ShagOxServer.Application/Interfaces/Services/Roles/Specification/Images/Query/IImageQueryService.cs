using ShagOxServer.Application.DTOs.Specification.Images;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Interfaces.Services.Roles.Specification.Images.Query;
public interface IImageQueryService
{
    Task<Result<ImageDto>> GetByIdAsync(int id);
}
