import type { Order } from "../model/types";
import { ORDER_STATUS_COLORS, ORDER_STATUS_LABELS } from "./labels";

/**
 * Стан оплати замовлення з точки зору користувача.
 *
 * - on-delivery — оплата при отриманні, дій не потрібно;
 * - paid        — оплачено онлайн;
 * - refunded    — кошти повернено;
 * - awaiting    — чекаємо оплату; expiresAt — до коли діє поточне посилання
 *                 (null — посилання прострочене або ще не створене);
 * - failed      — оплату не отримано, замовлення скасовано;
 *                 retryUntil — до коли можна оплатити повторно (null — вже не можна);
 * - cancelled   — замовлення скасоване не через оплату, платити нічого.
 */
export type OrderPaymentState =
  | { kind: "on-delivery" }
  | { kind: "paid"; paidAt: Date | null }
  | { kind: "refunded" }
  | { kind: "awaiting"; expiresAt: Date | null }
  | { kind: "failed"; retryUntil: Date | null }
  | { kind: "cancelled" };

const CLOSED_STATUSES: Order["orderStatus"][] = [
  "Cancelled",
  "Rejected",
  "Refunded",
];

function toDate(value: string | null | undefined) {
  return value ? new Date(value) : null;
}

function isFuture(date: Date | null, now: number): date is Date {
  return date !== null && date.getTime() > now;
}

export function getOrderPaymentState(
  order: Order,
  now: number = Date.now(),
): OrderPaymentState {
  if (order.paymentMethod !== "Online") return { kind: "on-delivery" };

  switch (order.paymentStatus) {
    case "Paid":
      return { kind: "paid", paidAt: toDate(order.paidAt) };

    case "Refunded":
      return { kind: "refunded" };

    case "Failed": {
      const deadline = toDate(order.paymentDeadline);
      return {
        kind: "failed",
        retryUntil: isFuture(deadline, now) ? deadline : null,
      };
    }

    case "Pending":
    default: {
      // Замовлення скасоване не через оплату — платити вже нічого.
      if (CLOSED_STATUSES.includes(order.orderStatus)) {
        return { kind: "cancelled" };
      }

      const expiresAt = toDate(order.paymentExpiresAt);
      return {
        kind: "awaiting",
        expiresAt: isFuture(expiresAt, now) ? expiresAt : null,
      };
    }
  }
}

/**
 * Статус для бейджа: для неоплачених онлайн-замовлень показуємо стан оплати,
 * бо «Очікує підтвердження» чи «Скасовано» не пояснюють, що сталося.
 */
export function getOrderDisplayStatus(order: Order) {
  const payment = getOrderPaymentState(order);

  if (payment.kind === "awaiting") {
    return { label: "Очікує оплати", color: "orange" };
  }

  if (payment.kind === "failed") {
    return { label: "Не оплачено", color: "red" };
  }

  // Для самовивозу «відправки» немає — замовлення чекає на покупця.
  if (
    order.orderStatus === "ReadyForShipment" &&
    order.deliveryType === "Pickup"
  ) {
    return { label: "Готове до видачі", color: "indigo" };
  }

  return {
    label: ORDER_STATUS_LABELS[order.orderStatus],
    color: ORDER_STATUS_COLORS[order.orderStatus],
  };
}

export function formatDateTime(date: Date) {
  return date.toLocaleString("uk-UA", {
    day: "numeric",
    month: "long",
    hour: "2-digit",
    minute: "2-digit",
  });
}

/** 14:32 або 1:02:05 */
export function formatCountdown(ms: number) {
  const totalSeconds = Math.max(0, Math.ceil(ms / 1000));
  const hours = Math.floor(totalSeconds / 3600);
  const minutes = Math.floor((totalSeconds % 3600) / 60);
  const seconds = totalSeconds % 60;
  const pad = (n: number) => String(n).padStart(2, "0");

  return hours > 0
    ? `${hours}:${pad(minutes)}:${pad(seconds)}`
    : `${pad(minutes)}:${pad(seconds)}`;
}
