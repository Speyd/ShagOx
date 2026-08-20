namespace ShagOxServer.Application.DTOs.Common.ImageLoaders.Delete;
public sealed record PictureLoaderDeleteResponse
(
    string PublicId,
    DateTime DeleteTime
);