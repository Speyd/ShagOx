import { useMutation, useQueryClient } from "@tanstack/react-query";
import { deleteFavorite } from "../../api/favoritesApi";
import { toast } from "sonner";

export function useDeleteFavorite() {
  const queryClient = useQueryClient();

  return useMutation({
    mutationFn: deleteFavorite,
    onSuccess: () => {
      queryClient.invalidateQueries({
        queryKey: ["favorites"],
      });
      toast.success("Оголошення видалено з обраного!");
    },
    onError: () => {
      toast.error(
        "Не вдалося видалити оголошення з обраного. Спробуйте ще раз.",
      );
    },
  });
}
