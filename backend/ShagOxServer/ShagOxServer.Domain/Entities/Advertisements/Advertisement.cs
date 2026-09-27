using ShagOxServer.Domain.Base;
using ShagOxServer.Domain.Entities.Account;
using ShagOxServer.Domain.Entities.Baskets;
using ShagOxServer.Domain.Entities.Dictionaries;
using ShagOxServer.Domain.Entities.Specification;
using ShagOxServer.Domain.Entities.Specification.Pictures;
using System.Text.Json;

namespace ShagOxServer.Domain.Entities.Advertisements;
public class Advertisement 
    : BaseEntity
{
    public string Title { get; set; } = "";
    public string Description { get; set; } = "";

    /// <summary>
    /// Popularity score used for ranking products in search results and recommendations.
    /// </summary>
    public int Popularity { get; set; } = 0;

    public long StatusId { get; set; }
    public Status Status { get; set; } = null!;

    public long ConditionId { get; set; }
    public Condition Condition { get; set; } = null!;

    public long CurrencyId { get; set; }
    /// <summary>
    /// Currency in which the product price is specified.
    /// </summary>
    public Currency Currency { get; set; } = null!;

    public long CategoryId { get; set; }
    public Category Category { get; set; } = null!;

    /// <summary>
    /// Collection of product images displayed in the product gallery.
    /// </summary>
    public ICollection<Image> Images { get; set; }
        = [];

    public ICollection<Favorite> Favorites { get; set; }
        = [];

    public long SellerId { get; set; }
    /// <summary>
    /// User who published the product listing.
    /// </summary>
    public User Seller { get; set; } = null!;


    public long? BuyerId { get; set; }
    /// <summary>
    /// User who purchased the product. Null if the product has not been sold.
    /// </summary>
    public User? Buyer { get; set; } = null;

    /// <summary>
    /// Date and time when the product was marked as sold.
    /// Null if the product is still available.
    /// </summary>
    public DateTime? SoldAt { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Flexible JSON storage for category-specific product attributes.
    /// </summary>
    public JsonDocument Attributes { get; set; } = null!;

    public ICollection<AdvertisementVariant> Variants { get; set; }
        = [];

    public ICollection<BasketItem> BasketItems { get; set; }
        = [];


    public override string ToString()
    {
        return Title ?? string.Empty;
    }
}