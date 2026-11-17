import { useMutation, useQueryClient } from "@tanstack/react-query";
import { toast } from "sonner";
import axios from "axios";
import { getErrorMessage } from "@/shared/api/getErrorMessage";
import { SELLER_ADVERTISEMENTS_KEY } from "@/entities/advertisement/model/hooks/useGetSellerAdvertisements";
import { updateAdvertisement } from "../../api/api";
import type { UpdateAdvertisementRequestDto } from "../types";

type UpdateAdvertisementRequest = {
  id: number;
  data: UpdateAdvertisementRequestDto;
};

export default function useUpdateAdvertisement() {
  const queryClient = useQueryClient();

  return useMutation({
    mutationFn: ({ id, data }: UpdateAdvertisementRequest) =>
      updateAdvertisement(id, data),
    onSuccess: (_, { id }) => {
      toast.success("Зміни збережено");
      queryClient.invalidateQueries({ queryKey: ["advertisement", id] });
      queryClient.invalidateQueries({ queryKey: [SELLER_ADVERTISEMENTS_KEY] });
      queryClient.invalidateQueries({ queryKey: ["catalog-advertisements"] });
    },
    onError: (error) => {
      if (axios.isAxiosError(error) && error.response?.status === 403) {
        toast.error("Ви не є власником цього оголошення.");
        return;
      }

      toast.error(
        getErrorMessage(error, "Не вдалося оновити оголошення. Спробуйте ще раз."),
      );
    },
  });
}
