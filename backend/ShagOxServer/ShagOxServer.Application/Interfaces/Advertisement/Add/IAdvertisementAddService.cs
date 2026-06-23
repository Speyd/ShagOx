using ShagOxServer.Application.DTOs.Advertisement.Add;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShagOxServer.Application.Interfaces.Advertisement.Add;
public interface IAdvertisementAddService
{
    Task<AdvertisementAddResponse> AddAdvertisement(
        AdvertisementAddRequest request);
}
