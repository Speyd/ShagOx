using System.Security.Cryptography;
using System.Text;
using System.Text.Json;

namespace ShagOxServer.Application.Common.Helpers;
public static class CacheKeyHelper
{
    public static string GetSearchHash<TFilter>(
        TFilter filter,
        int page,
        int pageSize)
    {
        var data = new
        {
            Filter = filter,
            page,
            pageSize
        };

        var json = JsonSerializer.Serialize(data);

        return Convert.ToHexString(
            SHA256.HashData(
                Encoding.UTF8.GetBytes(json)));
    }
}