import { useMutation, useQueryClient } from "@tanstack/react-query";

import { toast } from "sonner";
import { createAdvertisement } from "../../api/api";

export function useCreateAdvertisement() {
  const queryClient = useQueryClient();

  return useMutation({
    mutationFn: createAdvertisement,

    onSuccess: () => {
      queryClient.invalidateQueries({
        queryKey: ["advertisements"],
      });
      toast.success("Оголошення успішно створено!");
    },

    onError: () => {
      toast.error("Не вдалося створити оголошення. Спробуйте ще раз.");
    },
  });
}
