import { api } from "@/shared/api/api";
import type { PaginatedResponse } from "@/shared/lib/types/paginatedResponse";
import type {
  Order,
  OrderCreateRequest,
  OrderCreateResponse,
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
