
using ShagOxServer.Domain.Base;
using ShagOxServer.Domain.Entities.Account;
using ShagOxServer.Domain.Entities.Dictionaries;
using ShagOxServer.Domain.Entities.Specification;

namespace ShagOxServer.Domain.Entities;

public class Advertisement : BaseEntity
{
    public string Title { get; set; } = "";
    public string Description { get; set; } = "";

    /// <summary>
    /// Popularity score used for ranking products in search results and recommendations.
    /// </summary>
    public int Popularity { get; set; } = 0;

    /// <summary>
    /// Current product price in the specified currency.
    /// </summary>
    public int Price { get; set; }
    /// <summary>
    /// Previous product price used for displaying discounts and price changes.
    /// </summary>
    public int PreviousPrice { get; set; }

    public int CurrencyId { get; set; }
    /// <summary>
    /// Currency in which the product price is specified.
    /// </summary>
    public Currency? Currency { get; set; } = null;

    public int CategoryId { get; set; }
    public Category? Category { get; set; } = null;

    /// <summary>
    /// Collection of product images displayed in the product gallery.
    /// </summary>
    public List<Image> Images { get; set; } = [];

    /// <summary>
    /// User who published the product listing.
    /// </summary>
    public required User Seller { get; set; }

    /// <summary>
    /// User who purchased the product. Null if the product has not been sold.
    /// </summary>
    public User? Buyer { get; set; } = null;

    /// <summary>
    /// Date and time when the product was marked as sold.
    /// Null if the product is still available.
    /// </summary>
    public DateTime? SoldAt { get; set; }

    public DateTime CreatedAt { get; set; }

    /// <summary>
    /// Flexible JSON storage for category-specific product attributes.
    /// </summary>
    public string Properties { get; set; } = "{}";

    public override string ToString()
    {
        throw new NotImplementedException();
    }
}
