using ShagOxServer.Application.DTOs.Base.Responses;
using ShagOxServer.Application.DTOs.Comments.ReviewRepliComments.Create;
using ShagOxServer.Application.Interfaces.Services.Base.Create;

namespace ShagOxServer.Application.Interfaces.Services.Comments.ReviewRepliComments.Create;
public interface IReviewRepliCommentCreateService
    : ICreateOwnerService<
        CreateResponse,
        ReviewRepliCommentCreateRequest
        >
{
}
