import IconButton from "@/shared/ui/icon-button";
import styles from "./FavoriteButton.module.css";
import { Heart } from "lucide-react";
import { useAddToFavorites } from "../model/hooks/useAddToFavorites";
import { useDeleteFavorite } from "../model/hooks/useDeleteFavorite";
import { useGetFavorites } from "../model/hooks/useGetFavorites";
import { toast } from "sonner";
import { useAuthStore } from "@/features/auth";

type FavoriteButtonProps = {
  advertisementId: number;
  variant?: "default" | "card";
  className?: string;
};

export default function FavoriteButton({
  advertisementId,
  variant = "default",
  className,
}: FavoriteButtonProps) {
  const user = useAuthStore((state) => state.user);

  const addMutation = useAddToFavorites();
  const deleteMutation = useDeleteFavorite();

  const { data: favorites, isLoading: isFavoritesLoading } = useGetFavorites();

  // Шукаємо, чи є оголошення в обраному (без useState!)
  const favorite = favorites?.items?.find(
    (x) => x.advertisement?.id === advertisementId,
  );
  const isFavorite = Boolean(favorite);

  const isPending =
    isFavoritesLoading || addMutation.isPending || deleteMutation.isPending;

  function handleClick(e: React.MouseEvent) {
    e.preventDefault();
    e.stopPropagation();

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

  if (variant === "card") {
    return (
      <button
        onClick={handleClick}
        disabled={isPending}
        className={`${styles.cardButton} ${className ?? ""}`}
        aria-label="В обране"
      >
        <Heart
          size={18}
          strokeWidth={2}
          fill={isFavorite ? "#ef4444" : "none"}
          color={isFavorite ? "#ef4444" : "#475569"}
        />
      </button>
    );
  }

  return (
    <IconButton
      type="button"
      onClick={handleClick}
      disabled={isPending}
      className={className ?? styles.icon}
    >
      <Heart
        size={24}
        strokeWidth={2}
        fill={isFavorite ? "var(--color-base)" : "none"}
        color={isFavorite ? "var(--color-base)" : "#374151"}
      />
    </IconButton>
  );
}
