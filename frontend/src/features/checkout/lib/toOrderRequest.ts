import type { BasketItem } from "@/features/basket/model/types";
import type { DeliveryPointType } from "@/entities/delivery/model/types";
import type {
  DeliveryType,
  OrderCreateRequest,
} from "@/entities/order/model/types";
import type { CheckoutFormData, DeliveryMethod } from "../model/checkoutSchema";

const DELIVERY_TYPES: Record<DeliveryMethod, DeliveryType> = {
  branch: "DeliveryPoint",
  locker: "DeliveryPoint",
  courier: "HomeDelivery",
  pickup: "Pickup",
};

const POINT_TYPES: Record<DeliveryMethod, DeliveryPointType | null> = {
  branch: "Branch",
  locker: "ParcelLocker",
  courier: null,
  pickup: null,
};

export function toOrderRequest(
  data: CheckoutFormData,
  items: BasketItem[],
  deliveryProviderId: number | null,
  currencyId: number,
): OrderCreateRequest {
  const isPickup = data.deliveryMethod === "pickup";
  const isCourier = data.deliveryMethod === "courier";
  const isPoint = !isPickup && !isCourier;

  return {
    firstName: data.firstName,
    lastName: data.lastName,
    phone: data.phone,
    email: data.email,
    comment: data.comment || null,
    currencyId,
    orderItems: items.map((item) => ({
      parentId: 0,
      advertisementId: item.advertisementId,
      advertisementVariantId: item.advertisementVariantId,
      quantity: item.quantity,
    })),
    deliveryType: DELIVERY_TYPES[data.deliveryMethod],
    deliveryPointType: POINT_TYPES[data.deliveryMethod],
    deliveryProviderId: isPickup ? null : deliveryProviderId,
    recipientCityDto: isPickup ? null : data.city,
    deliveryPointDto: isPoint ? data.point : null,
    recipientStreetDto: isCourier ? data.street : null,
    recipientBuildingNumber: isCourier ? data.building : null,
    recipientFlatNumber: isCourier && data.flat ? data.flat : null,
    // Бекенд розрізняє готівку й картку при отриманні; у формі це один варіант.
    paymentMethod:
      data.paymentMethod === "Online" ? "Online" : "CashOnDelivery",
  };
}
