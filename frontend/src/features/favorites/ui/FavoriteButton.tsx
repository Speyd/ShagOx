import IconButton from "@/shared/ui/icon-button";
import styles from "./FavoriteButton.module.css";
import { Heart } from "lucide-react";
import { useAddToFavorites } from "../model/hooks/useAddToFavorites";
import { useAuthStore } from "@/features/auth/store/useAuthStore";
import { useDeleteFavorite } from "../model/hooks/useDeleteFavorite";
import { useGetFavorites } from "../model/hooks/useGetFavorites";
import { toast } from "sonner";

type FavoriteButtonProps = { advertisementId: number };

export default function FavoriteButton({
  advertisementId,
}: FavoriteButtonProps) {
  const user = useAuthStore((state) => state.user);

  const addMutation = useAddToFavorites();
  const deleteMutation = useDeleteFavorite();

  const { data: favorites } = useGetFavorites();
  const favorite = favorites?.items?.find(
    (x) => x.advertisement?.id === advertisementId,
  );

  const isPending = addMutation.isPending || deleteMutation.isPending;

  function handleClick() {
    if (!user) {
      toast.error(
        "Ви повинні бути зареєстровані, щоб додавати оголошення в обране.",
      );
      return;
    }
    if (favorite) {
      deleteMutation.mutate(favorite.id);
    } else {
      addMutation.mutate({
        advertisementId,
        userId: user.id,
      });
    }
  }
  return (
    <IconButton
      onClick={handleClick}
      disabled={isPending}
      className={styles.icon}
    >
      <Heart
        size={24}
        strokeWidth={2}
        fill={favorite ? "var(--color-base)" : "none"}
        color={favorite ? "var(--color-base)" : "#374151"}
      />
    </IconButton>
  );
}
