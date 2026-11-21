import { useQuery } from "@tanstack/react-query";
import { getMyOrders, getMySales, getOrder } from "../api/api";
import type { Order } from "./types";

/** Як часто перевіряти статус, поки замовлення очікує оплату. */
const PAYMENT_POLL_INTERVAL = 5_000;

export const orderQueryKey = (id: number) => ["orders", id] as const;

function isAwaitingOnlinePayment(order: Order | undefined) {
  return (
    order?.paymentMethod === "Online" &&
    order.paymentStatus === "Pending" &&
    order.orderStatus === "Pending"
  );
}

export function useMyOrders() {
  return useQuery({
    queryKey: ["orders", "me"],
    queryFn: getMyOrders,
  });
}

export function useMySales(enabled = true) {
  return useQuery({
    queryKey: ["orders", "sales"],
    queryFn: getMySales,
    enabled,
  });
}

export function useOrder(id: number) {
  return useQuery({
    queryKey: orderQueryKey(id),
    queryFn: () => getOrder(id),
    enabled: id > 0,
    // Статус оплати змінює вебхук банку, тож поки чекаємо оплату —
    // періодично перевіряємо (лише коли вкладка активна).
    refetchInterval: (query) =>
      isAwaitingOnlinePayment(query.state.data) ? PAYMENT_POLL_INTERVAL : false,
    refetchOnWindowFocus: true,
  });
}
