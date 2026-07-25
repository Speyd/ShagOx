import { useMutation, useQueryClient } from "@tanstack/react-query";
import { toast } from "sonner";
import axios from "axios";
import { deleteAdvertisement } from "../../api/api";

export function useDeleteAdvertisement() {
  const queryClient = useQueryClient();

  return useMutation({
    mutationFn: deleteAdvertisement,
    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: ["advertisements"] });
      toast.success("Оголошення успішно видалено!");
    },
    onError: (error) => {
      if (axios.isAxiosError(error) && error.response?.status === 403) {
        toast.error("Ви не є власником цього оголошення.");
        return;
      }

      toast.error("Не вдалося видалити оголошення. Спробуйте ще раз.");
    },
  });
}
