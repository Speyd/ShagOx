using ShagOxServer.Application.DTOs.Specification.Pictures.Create;

namespace ShagOxServer.Application.DTOs.Specification.Pictures.Core.PictureOrdering;
public sealed record SyncPictureResponce(
    List<PictureCreateResponse> LoadedPicture,
    List<string> DeletePublicIds,
    List<int> OriginalPictureOrders,
    bool IsSuccess
);
