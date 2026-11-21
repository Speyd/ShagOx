import { useMutation, useQueryClient } from "@tanstack/react-query";
import { toast } from "sonner";
import { getErrorMessage } from "@/shared/api/getErrorMessage";
import { SELLER_ADVERTISEMENTS_KEY } from "@/entities/advertisement/model/hooks/useGetSellerAdvertisements";
import { createAdvertisement } from "../../api/api";

export function useCreateAdvertisement() {
  const queryClient = useQueryClient();

  return useMutation({
    mutationFn: createAdvertisement,
    onSuccess: () => {
      toast.success("Оголошення успішно опубліковано!");
      queryClient.invalidateQueries({ queryKey: ["advertisements"] });
      queryClient.invalidateQueries({ queryKey: ["catalog-advertisements"] });
      queryClient.invalidateQueries({ queryKey: [SELLER_ADVERTISEMENTS_KEY] });
    },
    onError: (error) =>
      toast.error(
        getErrorMessage(
          error,
          "Не вдалося опублікувати оголошення. Спробуйте ще раз.",
        ),
      ),
  });
}
