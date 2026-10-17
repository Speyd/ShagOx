using ShagOxServer.Domain.Entities.Comments;
using ShagOxServer.Domain.Entities.Specification.Pictures;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShagOxServer.Application.Services.Caches.Keys.Specification.Pictures.Review;
public static class ReviewCommentCache
{
    public static readonly string Prefix =
       CacheKeys.Prefix<ReviewComment>();


    public static string ByAdvertisement(
       long advertId)
           => $"{Prefix}:advertisement:{advertId}";
}
