import type { LineItemAdvertisement } from "@/features/basket/model/types";
import type { Currency } from "@/shared/lib/types/currency";
import type {
  DeliveryCity,
  DeliveryPoint,
  DeliveryPointType,
  DeliveryProvider,
  DeliveryStreet,
} from "@/entities/delivery/model/types";

export type OrderStatus =
  | "Pending"
  | "Confirmed"
  | "Processing"
  | "ReadyForShipment"
  | "Shipped"
  | "InTransit"
  | "Delivered"
  | "Completed"
  | "Cancelled"
  | "Rejected"
  | "Refunded";

export type PaymentMethod = "CashOnDelivery" | "CardOnDelivery" | "Online";

export type PaymentStatus = "Pending" | "Paid" | "Failed" | "Refunded";

export type DeliveryType = "DeliveryPoint" | "HomeDelivery" | "Pickup";

export type OrderItem = {
  id: number;
  orderId: number;
  advertisementId: number;
  advertisementVariantId: number | null;
  quantity: number;
  price: number;
  totalPrice: number;
  advertisement: LineItemAdvertisement;
};

export type Order = {
  id: number;
  orderNumber: string;
  firstName: string;
  lastName: string;
  phone: string;
  email: string;
  orderItems: OrderItem[];
  comment: string | null;
  subtotal: number;
  deliveryPrice: number;
  totalPrice: number;
  currency: Currency;
  deliveryType: DeliveryType;
  deliveryProvider: DeliveryProvider | null;
  deliveryPointName: string | null;
  deliveryPointAddress: string | null;
  deliveryPointType: DeliveryPointType | null;
  deliveryTrackingNumber: string | null;
  paymentMethod: PaymentMethod;
  paymentStatus: PaymentStatus;
  orderStatus: OrderStatus;
  createdAt: string;
};

export type OrderItemCreateRequest = {
  parentId: number;
  advertisementId: number;
  advertisementVariantId: number | null;
  quantity: number;
};

export type OrderCreateRequest = {
  firstName: string;
  lastName: string;
  phone: string;
  email: string;
  orderItems: OrderItemCreateRequest[];
  comment: string | null;
  currencyId: number;
  deliveryType: DeliveryType;
  deliveryProviderId: number | null;
  deliveryPointDto: DeliveryPoint | null;
  recipientCityDto: DeliveryCity | null;
  recipientStreetDto: DeliveryStreet | null;
  recipientBuildingNumber: string | null;
  recipientFlatNumber: string | null;
  deliveryPointType: DeliveryPointType | null;
  paymentMethod: PaymentMethod;
};

export type OrderCreateResponse = {
  id: number;
  createdAt: string;
};
