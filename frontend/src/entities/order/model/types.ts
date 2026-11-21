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
  deliveryTrackingUrl: string | null;
  paymentMethod: PaymentMethod;
  paymentStatus: PaymentStatus;
  paidAt: string | null;
  /** Коли закінчується діюче посилання на оплату (null — посилання немає). */
  paymentExpiresAt: string | null;
  /** До якого моменту замовлення ще можна оплатити онлайн (null — не можна). */
  paymentDeadline: string | null;
  orderStatus: OrderStatus;
  createdAt: string;
  confirmedAt: string | null;
  shippedAt: string | null;
  deliveredAt: string | null;
  cancelledAt: string | null;
  /** Продавець замовлення (null — старе замовлення з товарами кількох продавців). */
  sellerId: number | null;
};

export type OrderStatusResponse = {
  id: number;
  orderStatus: OrderStatus;
  changedAt: string;
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

export type OrderPaymentResponse = {
  paymentUrl: string;
  expiresAt: string;
};

export type OrderCreateResponse = {
  id: number;
  paymentUrl?: string;
  createdAt: string;
};
