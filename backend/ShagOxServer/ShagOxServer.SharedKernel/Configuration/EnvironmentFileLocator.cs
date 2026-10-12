namespace ShagOxServer.SharedKernel.Configuration;
public static class EnvironmentFileLocator
{
    public static string Find(
        string fileName = ".env")
    {
        var directory = new DirectoryInfo(
            Directory.GetCurrentDirectory());

        while (directory is not null)
        {
            var path = Path.Combine(directory.FullName, fileName);

            if (File.Exists(path))
                return path;

            directory = directory.Parent;
        }

        throw new FileNotFoundException(
            $"Could not find '{fileName}' in the current directory " +
            "or any parent directory.");
    }
}
