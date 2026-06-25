using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace ShagOxServer.Application.DTOs.Advertisements.Update;
public sealed record AdvertisementUpdateResponse 
(
    DateTime TimeUpdate,
    int CountUpdatedProperty
);