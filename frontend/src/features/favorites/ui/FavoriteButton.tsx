import IconButton from "@/shared/ui/IconButton";
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

  const favorite = favorites?.find(
    (x) => x.advertisement.id === advertisementId,
  );

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
      disabled={addMutation.isPending}
      className={styles.icon}
    >
      <Heart
        size={24}
        strokeWidth={2}
        fill={favorite ? "#ef4444" : "none"}
        color={favorite ? "#ef4444" : "#374151"}
      />
    </IconButton>
  );
}
