import { useMutation, useQueryClient } from "@tanstack/react-query";
import { deleteItem } from "../../api/api";
import { toast } from "sonner";
import axios from "axios";

export const useDeleteBasketItem = () => {
  const queryClient = useQueryClient();

  return useMutation({
    mutationFn: (id: number) => deleteItem(id),
    onSuccess: () => {
      toast.success("Товар видалено з кошика!");
      queryClient.invalidateQueries({ queryKey: ["basket"] });
    },
    onError: (error) => {
      if (axios.isAxiosError(error) && error.response?.status === 403) {
        toast.error(
          "Сервер заборонив видалення товару. Потрібно виправити перевірку власника кошика.",
        );
        return;
      }

      toast.error("Не вдалося видалити товар з кошика");
    },
  });
};
