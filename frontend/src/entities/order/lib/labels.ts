import type {
  DeliveryType,
  Order,
  OrderStatus,
  PaymentMethod,
} from "../model/types";

export const ORDER_STATUS_LABELS: Record<OrderStatus, string> = {
  Pending: "Очікує підтвердження",
  Confirmed: "Підтверджено",
  Processing: "В обробці",
  ReadyForShipment: "Готується до відправки",
  Shipped: "Відправлено",
  InTransit: "В дорозі",
  Delivered: "Доставлено",
  Completed: "Виконано",
  Cancelled: "Скасовано",
  Rejected: "Відхилено",
  Refunded: "Кошти повернено",
};

export const ORDER_STATUS_COLORS: Record<OrderStatus, string> = {
  Pending: "yellow",
  Confirmed: "blue",
  Processing: "blue",
  ReadyForShipment: "blue",
  Shipped: "indigo",
  InTransit: "indigo",
  Delivered: "green",
  Completed: "green",
  Cancelled: "gray",
  Rejected: "red",
  Refunded: "gray",
};

export const PAYMENT_METHOD_LABELS: Record<PaymentMethod, string> = {
  CashOnDelivery: "Готівкою при отриманні",
  CardOnDelivery: "Карткою при отриманні",
  Online: "Онлайн-оплата",
};

export const DELIVERY_TYPE_LABELS: Record<DeliveryType, string> = {
  DeliveryPoint: "Доставка у відділення",
  HomeDelivery: "Кур'єрська доставка",
  Pickup: "Самовивіз",
};

export function getDeliveryAddress(order: Order) {
  if (order.deliveryType === "Pickup") return "Самовивіз з магазину";
  if (order.deliveryType === "HomeDelivery") return "Кур'єром за вказаною адресою";

  return order.deliveryPointName ?? order.deliveryPointAddress ?? "";
}

export function formatMoney(amount: number, symbol: string) {
  return `${amount.toLocaleString("uk-UA", { maximumFractionDigits: 2 })} ${symbol}`;
}

export function formatOrderDate(date: string) {
  return new Date(date).toLocaleDateString("uk-UA", {
    day: "2-digit",
    month: "long",
    year: "numeric",
  });
}
