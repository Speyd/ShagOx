using ShagOxServer.Application.Common.Results;
using ShagOxServer.Application.DTOs.Advertisements;

namespace ShagOxServer.Application.Interfaces.Advertisements.Query;
public interface IAdvertisementQueryService
{
    Task<Result<AdvertisementDto>> GetByIdAsync(int id);

    Task<Result<AdvertisementDto>> GetSellerAdvertisementsAsync(int userId);

    Task<Result<AdvertisementDto>> GetPurchasedAdvertisementsAsync(int userId);

    Task<Result<List<AdvertisementDto>>> GetByCategoryAsync(int categoryId);

    Task<Result<List<AdvertisementDto>>> GetAllAsync(int page, int pageSize);

    Task<Result<List<AdvertisementDto>>> SearchAsync(string query);
}
