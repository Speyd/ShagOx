namespace ShagOxServer.Api.Common;
public static class ProjectPath
{
    public static string Root =>
        Path.GetFullPath(
            Path.Combine(
                AppContext.BaseDirectory,
                "..",
                "..",
                "..",
                "..",
                "..",
                ".."));
}