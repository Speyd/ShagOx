using ShagOxServer.Domain.Base.Owner;
using ShagOxServer.Domain.Entities.Account;
using ShagOxServer.Domain.Entities.Deliveries;
using ShagOxServer.Domain.Entities.Deliveries.Enum;
using ShagOxServer.Domain.Entities.Orders.Enum;
using ShagOxServer.Domain.Entities.Specification;

namespace ShagOxServer.Domain.Entities.Orders;
public class Order
    : OwnedEntity<User>
{
    public string OrderNumber { get; set; } = null!;//

    public string FirstName { get; set; } = null!;//
    public string LastName { get; set; } = null!;//

    public string Phone { get; set; } = null!;//
    public string Email { get; set; } = null!;//


    public ICollection<OrderItem> OrderItems { get; set; }//code
        = [];

    public string? Comment { get; set; }//

    public decimal Subtotal { get; set; }//code

    public decimal DeliveryPrice { get; set; }//code

    public decimal TotalPrice { get; set; }//code
    public decimal TotalWeight { get; set; }//code


    public long CurrencyId { get; set; }//
    public Currency Currency { get; set; } = null!;


    public DeliveryType DeliveryType { get; set; }//

    public long? DeliveryProviderId { get; set; }//
    public DeliveryProvider? DeliveryProvider { get; set; }

    // City
    public string? DeliveryCityExternalId { get; set; }//из фронта запрос на getCities в деливери и передается сюда

    // Delivery point (warehouse / parcel locker)
    public string? DeliveryPointExternalId { get; set; }// из фронта запрос на getCities в деливери и передается сюда
    public string? DeliveryPointNumber { get; set; }// из фронта запрос на getCities в деливери и передается сюда
    public string? DeliveryPointName { get; set; }// из фронта запрос на getCities в деливери и передается сюда
    public string? DeliveryPointAddress { get; set; }// из фронта запрос на getCities в деливери и передается сюда

    public DeliveryPointType? DeliveryPointType { get; set; }//



    public string? StreetExternalId { get; set; }// из фронта запрос на getCities в деливери и передается сюда
    public string? BuildingNumber { get; set; }//
    public string? FlatNumber { get; set; }//




    public string? DeliveryTrackingNumber { get; set; }

    public string? DeliveryTrackingUrl { get; set; }

    public DateTime? ShippedAt { get; set; }

    public DateTime? DeliveredAt { get; set; }

    public DateTime? EstimatedDeliveryAt { get; set; }




    public PaymentMethod PaymentMethod { get; set; }

    public PaymentStatus PaymentStatus { get; set; }

    public DateTime? PaidAt { get; set; }

    public string? PaymentTransactionId { get; set; }

    public string? PaymentProvider { get; set; }



    public OrderStatus OrderStatus { get; set; }



    public DateTime? ConfirmedAt { get; set; }
    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public DateTime? CancelledAt { get; set; }



    public override string ToString()
    {
        return $"OwnerId: {OwnerId}";
    }
}
