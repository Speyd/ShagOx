using ShagOxServer.Application.DTOs.Advertisements;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Interfaces.Advertisements.Query;
public interface IAdvertisementQueryService
{
    Task<Result<AdvertisementDto>> GetByIdAsync(int id);

    Task<Result<List<AdvertisementDto>>> GetSellerAdvertisementsAsync(
        int userId,
        int page,
        int pageSize);

    Task<Result<List<AdvertisementDto>>> GetPurchasedAdvertisementsAsync(
        int userId,
        int page,
        int pageSize);

    Task<Result<List<AdvertisementDto>>> GetAllAsync(int page, int pageSize);

    Task<Result<List<AdvertisementDto>>> GetByCategoryAsync(
        int categoryId,
        int page,
        int pageSize);

    Task<Result<List<AdvertisementDto>>> SearchByTitle(
        string title,
        int page,
        int pageSize);

    Task<Result<List<AdvertisementDto>>> SearchByDescription(
        string query,
        int page,
        int pageSize);
}
