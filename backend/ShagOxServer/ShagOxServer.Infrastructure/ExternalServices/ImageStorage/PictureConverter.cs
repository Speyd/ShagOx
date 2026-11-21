using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Webp;

namespace ShagOxServer.Infrastructure.ExternalServices.ImageStorage;
public static class PictureConverter
{
    public static async Task<Stream> Convert(
        Stream input,
        string format,
        CancellationToken cancellationToken = default)
    {
        return (format switch
        {
            "webp" => await ConvertToWebPAsync(
                input, cancellationToken),

            _ => await ConvertToWebPAsync(
                input, cancellationToken)
        });
    }

    public static async Task<Stream> ConvertToWebPAsync(
        Stream input,
        CancellationToken cancellationToken = default)
    {
        using var image = await Image.LoadAsync(input, cancellationToken);

        var output = new MemoryStream();

        await image.SaveAsWebpAsync(
            output,
            new WebpEncoder
            {
                Quality = 80
            },
            cancellationToken);

        output.Position = 0;

        return output;
    }
}
