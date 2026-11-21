import { useMutation, useQueryClient } from "@tanstack/react-query";
import { toast } from "sonner";
import { getErrorMessage } from "@/shared/api/getErrorMessage";
import type { CreateAdvertisementPayload } from "@/features/advertisement/create-advertisement";
import { adminCreateAdvertisement } from "./api";

export function useAdminCreateAdvertisement() {
  const queryClient = useQueryClient();

  return useMutation({
    mutationFn: ({
      sellerId,
      payload,
    }: {
      sellerId: number;
      payload: CreateAdvertisementPayload;
    }) => adminCreateAdvertisement(sellerId, payload),
    onSuccess: () => {
      queryClient.invalidateQueries();
      toast.success("Оголошення створено");
    },
    onError: (error) =>
      toast.error(getErrorMessage(error, "Не вдалося створити оголошення")),
  });
}
