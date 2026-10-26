import { useMutation, useQueryClient } from "@tanstack/react-query";
import { toast } from "sonner";
import {
  createAdvertisement,
  type CreateAdvertisementResponse,
} from "../../api/api";
import type { CreateAdvertisementPayload } from "../types";

export function useCreateAdvertisement() {
  const queryClient = useQueryClient();

  return useMutation<CreateAdvertisementResponse, Error, CreateAdvertisementPayload>({
    mutationFn: createAdvertisement,

    onSuccess: (data) => {
      queryClient.invalidateQueries({
        queryKey: ["advertisements"],
      });
      queryClient.invalidateQueries({
        queryKey: ["catalog-advertisements"],
      });
      toast.success("Товар успішно опубліковано!");
    },

    onError: (error) => {
      toast.error(
        error.message || "Не вдалося опублікувати товар. Перевірте заповнені поля.",
      );
    },
  });
}
