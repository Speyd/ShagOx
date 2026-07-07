using System;
using System.Collections.Generic;
using System.Text;

namespace ShagOxServer.Infrastructure.Interfaces.Advertisements.Advertisement;
public interface IAdvertisementExistsRepository
{
    Task<bool> ExistsById(int Id);

    Task<bool> IsOwnerAsync(int adId, int userId);
}
