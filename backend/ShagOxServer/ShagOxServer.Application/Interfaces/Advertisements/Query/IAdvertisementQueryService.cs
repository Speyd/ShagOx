using ShagOxServer.Application.DTOs.Advertisements;
using ShagOxServer.SharedKernel.Abstractions.Paginations;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Interfaces.Advertisements.Query;
public interface IAdvertisementQueryService
{
    Task<Result<AdvertisementDto>> GetByIdAsync(int id);

    Task<Result<List<AdvertisementDto>>> GetSellerAdvertisementsAsync(
        int userId,
        PaginationParams pagination);

    Task<Result<List<AdvertisementDto>>> GetPurchasedAdvertisementsAsync(
        int userId,
        PaginationParams pagination);

    Task<Result<List<AdvertisementDto>>> GetAllAsync(
        PaginationParams pagination);

    Task<Result<List<AdvertisementDto>>> GetByCategoryAsync(
        int categoryId,
        PaginationParams pagination);

    Task<Result<List<AdvertisementDto>>> SearchByTitle(
        string title,
        PaginationParams pagination);

    Task<Result<List<AdvertisementDto>>> SearchByDescription(
        string query,
        PaginationParams pagination);
}
