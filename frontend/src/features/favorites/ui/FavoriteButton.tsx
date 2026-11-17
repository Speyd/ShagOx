import IconButton from "@/shared/ui/buttons/icon-button";
import styles from "./FavoriteButton.module.css";
import { Heart } from "lucide-react";
import { useToggleFavorite } from "../model/hooks/useToggleFavorite";

type FavoriteButtonProps = {
  advertisementId: number;
  defaultIsFavorite?: boolean;
  variant?: "default" | "card";
  className?: string;
};

export default function FavoriteButton({
  advertisementId,
  defaultIsFavorite,
  variant = "default",
  className,
}: FavoriteButtonProps) {
  const { isFavorite, isPending, toggleFavorite } =
    useToggleFavorite(advertisementId, defaultIsFavorite);

  function handleClick(e: React.MouseEvent) {
    e.preventDefault();
    e.stopPropagation();
    toggleFavorite();
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
