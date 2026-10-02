using System.Globalization;

namespace ShagOxServer.Application.Resources.EntityNames.Extensions;
public static class EntityNameExtensions
{
    public static string GetLocalizedName<T>()
    {
        return EntityNamesResources.ResourceManager.GetString(
            typeof(T).Name,
            CultureInfo.CurrentUICulture)
            ?? typeof(T).Name;
    }
}