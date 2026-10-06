import { useAuthStore } from "@/features/auth";
import { useAddToFavorites } from "@/features/favorites/model/hooks/useAddToFavorites";
import { useDeleteFavorite } from "@/features/favorites/model/hooks/useDeleteFavorite";
import { useGetFavorites } from "@/features/favorites/model/hooks/useGetFavorites";
import { toast } from "sonner";

export function useToggleBasketFavorite(advertisementId?: number) {
  const user = useAuthStore((state) => state.user);
  const { data: favorites } = useGetFavorites();
  const addFavorite = useAddToFavorites();
  const deleteFavorite = useDeleteFavorite();

  const favorite = favorites?.items?.find(
    (favoriteItem) => favoriteItem.advertisement?.id === advertisementId,
  );
  const isPending = addFavorite.isPending || deleteFavorite.isPending;

  const toggleFavorite = () => {
    if (!user) {
      toast.error(
        "Ви повинні бути зареєстровані, щоб додавати оголошення в обране.",
      );
      return;
    }

    if (!advertisementId || isPending) return;

    if (favorite) {
      deleteFavorite.mutate(favorite.id);
      return;
    }

    addFavorite.mutate({
      advertisementId,
      userId: user.id,
    });
  };

  return { favorite, isPending, toggleFavorite };
}
