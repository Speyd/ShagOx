using ShagOxServer.Application.Common.Helpers;
using ShagOxServer.Domain.Entities.Deliveries.Enum;

namespace ShagOxServer.Application.Services.Caches.Keys.Deliveries.Providers.NovaPoshta;
public static class NovaPoshtaCache
{
    public static readonly string Prefix = "NovaPoshta";

    public static string CityBySearch(
      string citySearch,
      int page,
      int pageSize)
          => $"{Prefix}:city:{GetHash(citySearch, page, pageSize)}";

    public static string StreetBySearch(
      string streetExternalId,
      string streetSearch,
      int page,
      int pageSize)
          => $"{Prefix}:street:{GetHash((streetExternalId, streetSearch), page, pageSize)}";

    public static string PointBySearch(
       string cityExternalId,
        string? pointSearch,
        DeliveryPointType deliveryPointType,
        int page,
        int pageSize)
    {
        var filter = (cityExternalId, pointSearch, deliveryPointType);

        return $"{Prefix}:point:{GetHash(filter, page, pageSize)}";
    }

    public static string WarehouseTypes()
        => $"{Prefix}:warehouses";


    private static string GetHash(
        object filter,
        int page,
        int pageSize)
    {
        return CacheKeyHelper.GetSearchHash(
            filter,
            page,
            pageSize);
    }
}
