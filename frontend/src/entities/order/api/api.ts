import { api } from "@/shared/api/api";
import type { PaginatedResponse } from "@/shared/lib/types/paginatedResponse";
import type {
  Order,
  OrderCreateRequest,
  OrderCreateResponse,
  OrderPaymentResponse,
  OrderStatusResponse,
} from "../model/types";

export async function createOrder(
  request: OrderCreateRequest,
): Promise<OrderCreateResponse> {
  const { data } = await api.post<OrderCreateResponse>("/orders", request);
  return data;
}

export async function getMyOrders(): Promise<Order[]> {
  const { data } = await api.get<PaginatedResponse<Order>>("/orders/me", {
    params: { page: 1, pageSize: 50 },
  });
  return data.items;
}

export async function getOrder(id: number): Promise<Order> {
  const { data } = await api.get<Order>(`/orders/${id}`);
  return data;
}

export async function payOrder(id: number): Promise<OrderPaymentResponse> {
  const { data } = await api.post<OrderPaymentResponse>(
    `/orders/${id}/payment`,
  );
  return data;
}

export async function getMySales(): Promise<Order[]> {
  const { data } = await api.get<PaginatedResponse<Order>>("/orders/sales", {
    params: { page: 1, pageSize: 50 },
  });
  return data.items;
}

async function changeStatus(id: number, action: string, body?: unknown) {
  const { data } = await api.post<OrderStatusResponse>(
    `/orders/${id}/${action}`,
    body,
  );
  return data;
}

export const cancelOrder = (id: number) => changeStatus(id, "cancel");

export const confirmOrder = (id: number) => changeStatus(id, "confirm");

export const rejectOrder = (id: number) => changeStatus(id, "reject");

export const shipOrder = (id: number, trackingNumber: string | null) =>
  changeStatus(id, "ship", { trackingNumber });

export const completeOrder = (id: number) => changeStatus(id, "complete");
