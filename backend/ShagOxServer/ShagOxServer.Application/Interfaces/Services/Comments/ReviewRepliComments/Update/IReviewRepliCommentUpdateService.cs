using ShagOxServer.Application.DTOs.Base.Responses;
using ShagOxServer.Application.DTOs.Comments.ReviewRepliComments.Update;
using ShagOxServer.Application.Interfaces.Services.Base;

namespace ShagOxServer.Application.Interfaces.Services.Comments.ReviewRepliComments.Update;
public interface IReviewRepliCommentUpdateService
    : IUpdateService<UpdateResponse,
        ReviewRepliCommentUpdateRequest>
{
}
