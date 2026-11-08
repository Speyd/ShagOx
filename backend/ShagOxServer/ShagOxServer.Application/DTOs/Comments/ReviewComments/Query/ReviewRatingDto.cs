namespace ShagOxServer.Application.DTOs.Comments.ReviewComments.Query;
public sealed record ReviewRatingDto
(
    long AdvertisementId,
    double Average,
    int Count
);
