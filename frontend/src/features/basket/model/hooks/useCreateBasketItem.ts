import { useMutation, useQueryClient } from "@tanstack/react-query";
import type { BasketItemCreateRequest } from "../types";
import { createItem } from "../../api/api";
import { toast } from "sonner";

export const useCreateBasketItem = () => {
  const queryClient = useQueryClient();

  return useMutation({
    mutationFn: (data: BasketItemCreateRequest) => createItem(data),
    onSuccess: () => {
      toast.success("Товар додано до кошика!");
      queryClient.invalidateQueries({ queryKey: ["basket"] });
    },
    onError: () => {
      toast.error("Не вдалося додати товар до кошика");
    },
  });
};
