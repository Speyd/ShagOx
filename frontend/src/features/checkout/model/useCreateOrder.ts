import { useMutation, useQueryClient } from "@tanstack/react-query";
import { createOrder } from "@/entities/order/api/api";
import type { OrderCreateRequest } from "@/entities/order/model/types";
import { deleteItem } from "@/features/basket/api/api";

type CreateOrderParams = {
  request: OrderCreateRequest;
  basketItemIds: number[];
};

export function useCreateOrder() {
  const queryClient = useQueryClient();

  return useMutation({
    mutationFn: async ({ request, basketItemIds }: CreateOrderParams) => {
      const order = await createOrder(request);

      await Promise.allSettled(basketItemIds.map((id) => deleteItem(id)));

      return order;
    },
    onSuccess: () => {
      sessionStorage.removeItem("checkoutItemIds");
      queryClient.invalidateQueries({ queryKey: ["basket"] });
      queryClient.invalidateQueries({ queryKey: ["orders"] });
    },
  });
}
