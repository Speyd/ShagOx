using ShagOxServer.Application.DTOs.Base.Responses;
using ShagOxServer.Application.DTOs.Comments.ReviewComments.Update;
using ShagOxServer.Application.Interfaces.Services.Base;

namespace ShagOxServer.Application.Interfaces.Services.Comments.ReviewComments.Update;
public interface IReviewCommentUpdateService
    : IUpdateService<UpdateResponse, 
        ReviewCommentUpdateRequest>
{
}
