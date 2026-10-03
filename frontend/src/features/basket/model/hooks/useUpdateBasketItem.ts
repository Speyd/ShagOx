import { useMutation, useQueryClient } from "@tanstack/react-query";
import { updateItem } from "../../api/api";
import { toast } from "sonner";
import axios from "axios";
import type { BasketItemUpdateRequest } from "../types";

type UpdateBasketItemParams = {
  id: number;
  data: BasketItemUpdateRequest;
};

export const useUpdateBasketItem = () => {
  const queryClient = useQueryClient();

  return useMutation({
    mutationFn: ({ id, data }: UpdateBasketItemParams) => updateItem(id, data),
    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: ["basket"] });
    },
    onError: (error) => {
      if (axios.isAxiosError(error) && error.response?.status === 403) {
        toast.error(
          "Сервер заборонив зміну товару. Потрібно виправити перевірку власника кошика.",
        );
        return;
      }

      toast.error("Не вдалося оновити кількість товару");
    },
  });
};
