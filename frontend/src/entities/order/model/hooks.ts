import { useQuery } from "@tanstack/react-query";
import { getMyOrders, getOrder } from "../api/api";

export function useMyOrders() {
  return useQuery({
    queryKey: ["orders", "me"],
    queryFn: getMyOrders,
  });
}

export function useOrder(id: number) {
  return useQuery({
    queryKey: ["orders", id],
    queryFn: () => getOrder(id),
    enabled: id > 0,
  });
}
