import { useMutation, useQueryClient } from "@tanstack/react-query";
import { toast } from "sonner";
import axios from "axios";
import { SELLER_ADVERTISEMENTS_KEY } from "@/entities/advertisement/model/hooks/useGetSellerAdvertisements";
import { deleteAdvertisement } from "../../api/api";

export function useDeleteAdvertisement() {
  const queryClient = useQueryClient();

  return useMutation({
    mutationFn: deleteAdvertisement,
    onSuccess: (_, id) => {
      queryClient.invalidateQueries({ queryKey: [SELLER_ADVERTISEMENTS_KEY] });
      queryClient.invalidateQueries({ queryKey: ["advertisements"] });
      queryClient.invalidateQueries({ queryKey: ["catalog-advertisements"] });
      queryClient.removeQueries({ queryKey: ["advertisement", id] });
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
