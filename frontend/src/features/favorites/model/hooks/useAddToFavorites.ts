import { useMutation, useQueryClient } from "@tanstack/react-query";
import { addToFavorites } from "../../api/favoritesApi";
import { toast } from "sonner";

export function useAddToFavorites() {
  const queryClient = useQueryClient();

  return useMutation({
    mutationFn: addToFavorites,
    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: ["favorites"] });
      toast.success("Оголошення додано в обране!");
    },
    onError: () => {
      toast.error("Не вдалося додати оголошення в обране. Спробуйте ще раз.");
    },
  });
}
