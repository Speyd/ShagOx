import { useMutation } from "@tanstack/react-query";
import { toast } from "sonner";

import axios from "axios";
import { updateAdvertisement } from "../../api/api";
import type { UpdateAdvertisementRequestDto } from "../types";

type UpdateAdvertisementRequest = {
  id: number;
  data: UpdateAdvertisementRequestDto;
};

export default function useUpdateAdvertisement() {
  return useMutation({
    mutationFn: ({ id, data }: UpdateAdvertisementRequest) =>
      updateAdvertisement(id, data),
    onSuccess: () => toast.success("Ви успішно оновили оголошення!"),
    onError: (error) => {
      if (axios.isAxiosError(error) && error.response?.status === 403) {
        toast.error("Ви не є власником цього оголошення.");
        return;
      }

      toast.error("Не вдалося оновити оголошення. Спробуйте ще раз.");
    },
  });
}
