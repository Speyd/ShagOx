import { useMutation, useQueryClient } from "@tanstack/react-query";
import { deleteAdvertisement } from "../model/api";
import { toast } from "sonner";
import axios from "axios";

export function useDeleteAdvertisement() {
  const queryClient = useQueryClient();

  return useMutation({
    mutationFn: deleteAdvertisement,
    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: ["advertisements"] });
      toast.success("Advertisement deleted!");
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
