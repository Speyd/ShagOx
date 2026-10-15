import { useMutation, useQueryClient } from "@tanstack/react-query";
import type { BasketItemCreateRequest } from "../types";
import { createBasket, createItem } from "../../api/api";
import { toast } from "sonner";
import axios from "axios";

export const useCreateBasketItem = () => {
  const queryClient = useQueryClient();

  return useMutation({
    mutationFn: async (data: BasketItemCreateRequest) => {
      // Якщо basketId відсутній або дорівнює 0 — спочатку отримуємо/створюємо кошик
      if (!data.basketId) {
        const basket = await createBasket();
        data.basketId = basket.id;
      }
      return createItem(data);
    },
    onSuccess: () => {
      toast.success("Товар додано до кошика!");
      queryClient.invalidateQueries({ queryKey: ["basket"] });
    },
    onError: (error) => {
      if (axios.isAxiosError(error) && error.response?.status === 409) {
        toast.error("Товар уже є в кошику.");
        return;
      }
      toast.error("Не вдалося додати товар до кошика");
    },
  });
};