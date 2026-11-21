import type { Order, OrderStatus } from "../model/types";

export type OrderRole = "buyer" | "seller";

export type OrderAction = "cancel" | "confirm" | "reject" | "ship" | "complete";

/**
 * Хто переглядає замовлення. Бекенд віддає замовлення лише покупцю та
 * продавцю, тож якщо користувач не продавець — він покупець.
 */
export function getOrderRole(
  order: Order,
  userId: number | null | undefined,
): OrderRole | null {
  if (userId == null) return null;
  return order.sellerId === userId ? "seller" : "buyer";
}

// Ті самі правила, що в OrderStatusService на бекенді.
const BUYER_CANCELLABLE: OrderStatus[] = ["Pending", "Confirmed"];
const SELLER_REJECTABLE: OrderStatus[] = ["Pending", "Confirmed"];
const COMPLETABLE: OrderStatus[] = [
  "ReadyForShipment",
  "Shipped",
  "InTransit",
  "Delivered",
];

export function getAvailableActions(
  order: Order,
  role: OrderRole | null,
): OrderAction[] {
  const status = order.orderStatus;
  const actions: OrderAction[] = [];

  if (role === "buyer") {
    if (BUYER_CANCELLABLE.includes(status)) actions.push("cancel");
    if (COMPLETABLE.includes(status)) actions.push("complete");
  }

  if (role === "seller") {
    const awaitsOnlinePayment =
      order.paymentMethod === "Online" && order.paymentStatus !== "Paid";

    if (status === "Pending" && !awaitsOnlinePayment) actions.push("confirm");
    if (status === "Confirmed") actions.push("ship");
    if (COMPLETABLE.includes(status)) actions.push("complete");
    if (SELLER_REJECTABLE.includes(status) && !awaitsOnlinePayment) {
      actions.push("reject");
    }
  }

  return actions;
}

/** Чи оплачене онлайн-замовлення (тоді скасування = повернення коштів). */
export function isPaidOnline(order: Order) {
  return order.paymentMethod === "Online" && order.paymentStatus === "Paid";
}
