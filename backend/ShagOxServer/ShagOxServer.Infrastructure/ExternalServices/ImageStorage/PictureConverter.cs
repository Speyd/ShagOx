using SkiaSharp;

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
        cancellationToken.ThrowIfCancellationRequested();

        using var bitmap = SKBitmap.Decode(input)
            ?? throw new InvalidOperationException(
                "Unable to decode image.");

        using var image = SKImage.FromBitmap(bitmap);

        using var data = image.Encode(
            SKEncodedImageFormat.Webp,
            80);

        if (data is null)
            throw new InvalidOperationException(
                "Unable to encode image to WebP.");

        var output = new MemoryStream();

        try
        {
            await data.AsStream().CopyToAsync(
                output,
                cancellationToken);

            output.Position = 0;

            return output;
        }
        catch
        {
            await output.DisposeAsync();
            throw;
        }
    }
}
