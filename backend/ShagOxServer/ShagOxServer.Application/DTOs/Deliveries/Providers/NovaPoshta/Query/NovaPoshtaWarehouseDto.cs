namespace ShagOxServer.Application.DTOs.Deliveries.Providers.NovaPoshta.Query;
public sealed record NovaPoshtaWarehouseDto
(
    string Ref,
    string Description,
    string Number,
    string ShortAddress,
    string TypeOfWarehouse,
    string CategoryOfWarehouse
);
