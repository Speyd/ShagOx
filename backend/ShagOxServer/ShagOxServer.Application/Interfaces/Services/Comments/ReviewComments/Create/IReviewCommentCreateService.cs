using ShagOxServer.Application.DTOs.Base.Responses;
using ShagOxServer.Application.DTOs.Comments.ReviewComments.Create;
using ShagOxServer.Application.Interfaces.Services.Base.Create;

namespace ShagOxServer.Application.Interfaces.Services.Comments.ReviewComments.Create;
public interface IReviewCommentCreateService
    : ICreateOwnerService<
        CreateResponse,
        ReviewCommentCreateRequest
        >
{
}
