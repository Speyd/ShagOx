namespace ShagOxServer.Application.DTOs.Auth.Users.Statistics.Seller;
public sealed record TopAdvertisementDto
(
    long Id,
    string Title,
    string? PictureUrl,
    int SoldUnits,
    int Favorites,
    int Reviews,
    double? AverageGrade
);
