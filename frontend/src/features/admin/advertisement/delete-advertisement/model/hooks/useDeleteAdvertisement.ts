import { useMutation, useQueryClient } from "@tanstack/react-query";
import { toast } from "sonner";
import { adminDeleteAdvertisement } from "../../api/api";

export function useAdminDeleteAdvertisement() {
  const queryClient = useQueryClient();

  return useMutation({
    mutationFn: adminDeleteAdvertisement,
    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: ["advertisements"] });
      toast.success("Оголошення успішно видалено!");
    },
    onError: () => {
      toast.error("Не вдалося видалити оголошення. Спробуйте ще раз.");
    },
  });
}
