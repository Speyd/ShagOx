import { useMutation } from "@tanstack/react-query";
import { toast } from "sonner";
import { adminUpdateAdvertisement } from "../../api/api";
import type { UpdateAdvertisementRequest } from "../types";

export default function useAdminUpdateAdvertisement() {
  return useMutation({
    mutationFn: ({ id, data }: UpdateAdvertisementRequest) =>
      adminUpdateAdvertisement(id, data),
    onSuccess: () => toast.success("Ви успішно оновили оголошення!"),
    onError: () => {
      toast.error("Не вдалося оновити оголошення. Спробуйте ще раз.");
    },
  });
}
