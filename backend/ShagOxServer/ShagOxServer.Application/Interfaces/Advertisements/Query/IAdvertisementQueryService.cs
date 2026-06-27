using ShagOxServer.Application.Common.Results;
using ShagOxServer.Application.DTOs.Advertisements;

namespace ShagOxServer.Application.Interfaces.Advertisements.Query;
public interface IAdvertisementQueryService
{
    public Task<Result<UserDto>> GetByIdAsync(int id);

    public Task<Result<List<UserDto>>> GetByCategoryAsync(int categoryId);

    public Task<Result<List<UserDto>>> GetAllAsync(int page, int pageSize);

    public Task<Result<List<UserDto>>> SearchAsync(string query);
}
