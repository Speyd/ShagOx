using ShagOxServer.Application.Common.Results;
using ShagOxServer.Application.DTOs.Advertisements;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShagOxServer.Application.Interfaces.Advertisements.Query;
public interface IAdvertisementQueryService
{
    public Task<Result<AdvertisementDto>> GetByIdAsync(int id);

    public Task<Result<List<AdvertisementDto>>> GetByCategoryAsync(int categoryId);

    public Task<Result<List<AdvertisementDto>>> GetAllAsync(int page, int pageSize);

    public Task<Result<List<AdvertisementDto>>> SearchAsync(string query);
}
