import { useMutation } from "@tanstack/react-query";
import { updateAdvertisement } from "../model/api";
import { toast } from "sonner";
import type { UpdateAdvertisementDto } from "../model/types";

type UpdateAdvertisementRequest = {
  id: number;
  data: UpdateAdvertisementDto;
};

export default function useUpdateAdvertisement() {
  return useMutation({
    mutationFn: ({ id, data }: UpdateAdvertisementRequest) =>
      updateAdvertisement(id, data),
    onSuccess: () => toast.success("Ви успішно оновили оголошення!"),
    onError: () => toast.error("Помилка серверу."),
  });
}
