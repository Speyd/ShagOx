using ShagOxServer.Application.DTOs.Specification.Pictures.Core.Create.Response;
using ShagOxServer.Application.DTOs.Specification.Pictures.Core.Update.Response;

namespace ShagOxServer.Application.DTOs.Specification.Pictures.Core.PictureOrdering;
public sealed record PictureSyncResponce<TPicture>(
    List<PictureCreateResponse> LoadedPicture,
    List<string> DeletePublicIds,
    Dictionary<TPicture, PictureUpdateResponse> UpdatePictures,
    List<int> OriginalPictureOrders,
    bool IsSuccess
) where TPicture : notnull;
