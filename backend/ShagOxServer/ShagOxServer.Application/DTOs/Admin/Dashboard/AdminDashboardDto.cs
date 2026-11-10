namespace ShagOxServer.Application.DTOs.Admin.Dashboard;
public sealed record AdminDashboardDto
(
    DashboardTotalsDto Totals,
    List<DashboardGrowthDto> Growth,
    List<DashboardCategoryDto> AdvertisementsByCategory,
    List<DashboardGradeDto> Grades,
    List<DashboardUserDto> LatestUsers,
    List<DashboardAdvertisementDto> LatestAdvertisements,
    List<DashboardSellerDto> TopSellers,
    List<DashboardLowStockDto> LowStock
);

public sealed record DashboardTotalsDto
(
    int Users,
    int UsersLast30Days,
    int Advertisements,
    int AdvertisementsLast30Days,
    int SoldAdvertisements,
    int TotalStock,
    int Reviews,
    double AverageRating,
    int Categories,
    int ProductTypes,
    int AttributeDictionaries,
    int Cities
);

public sealed record DashboardGrowthDto
(
    string Month,
    int Users,
    int Advertisements
);

public sealed record DashboardCategoryDto
(
    long? CategoryId,
    string Name,
    int Count
);

public sealed record DashboardGradeDto
(
    int Grade,
    int Count
);

public sealed record DashboardUserDto
(
    long Id,
    string UserName,
    string? Email,
    string? Phone,
    string? AvatarUrl,
    DateTime RegisteredAt
);

public sealed record DashboardAdvertisementDto
(
    long Id,
    string Title,
    string? PictureUrl,
    decimal? Price,
    string CurrencySymbol,
    DateTime? SoldAt
);

public sealed record DashboardSellerDto
(
    long Id,
    string UserName,
    string? AvatarUrl,
    int AdvertisementsCount
);

public sealed record DashboardLowStockDto
(
    long Id,
    string Title,
    string? PictureUrl,
    string? SellerUserName,
    int Stock
);
