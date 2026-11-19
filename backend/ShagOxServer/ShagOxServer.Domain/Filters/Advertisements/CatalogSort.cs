namespace ShagOxServer.Domain.Filters.Advertisements;

/// <summary>Ordering of the public catalog (GET /api/advertisements/catalog?sort=...).</summary>
public enum CatalogSort
{
    /// <summary>Existing catalog order (by popularity).</summary>
    Default = 0,

    /// <summary>Newest first.</summary>
    Newest = 1,

    /// <summary>Only discounted products (previous price above the current one), newest first.</summary>
    Discount = 2
}
