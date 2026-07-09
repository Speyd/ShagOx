namespace ShagOxServer.Application.DTOs.Common.ImageLoaders.Delete;
public sealed record ImageLoaderDeleteResponse
(
    string PublicId,
    DateTime DeleteTime
);