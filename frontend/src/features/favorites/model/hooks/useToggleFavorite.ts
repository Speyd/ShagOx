import { toast } from "sonner";
import { useAuthStore } from "@/features/auth";
import { useAddToFavorites } from "./useAddToFavorites";
import { useDeleteFavorite } from "./useDeleteFavorite";
import { useFavoriteIds } from "./useGetFavorites";

export function useToggleFavorite(
  advertisementId?: number,
  fallbackIsFavorite = false,
) {
  const user = useAuthStore((state) => state.user);
  const { data: favoriteIds, isLoading } = useFavoriteIds();
  const addFavorite = useAddToFavorites();
  const deleteFavorite = useDeleteFavorite();

  const favoriteId =
    advertisementId === undefined ? undefined : favoriteIds?.get(advertisementId);
  const isPending = addFavorite.isPending || deleteFavorite.isPending;

  const toggleFavorite = () => {
    if (!user) {
      toast.error(
        "Ви повинні бути зареєстровані, щоб додавати оголошення в обране.",
      );
      return;
    }

    if (advertisementId === undefined || isPending || isLoading) return;

    if (favoriteId !== undefined) {
      deleteFavorite.mutate(favoriteId);
      return;
    }

    addFavorite.mutate({ advertisementId, userId: user.id });
  };

  return {
    isFavorite: favoriteIds ? favoriteId !== undefined : fallbackIsFavorite,
    isPending: isPending || isLoading,
    toggleFavorite,
  };
}
