using ShagOxServer.Application.DTOs.Common.Responses;

namespace ShagOxServer.Application.DTOs.Specification.Images.Create;
public sealed record ImageCreateResponse
    : CreateResponse
{
    public string PublicId { get; set; }

    public ImageCreateResponse(
        int Id,
        string publicId,
        DateTime CreatedAt
    )
        : base(Id, CreatedAt)
    {
        PublicId = publicId;
    }
}