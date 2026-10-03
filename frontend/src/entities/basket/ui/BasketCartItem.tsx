import {
  Checkbox,
  Group,
  Image,
  Text,
  ActionIcon,
  UnstyledButton,
} from "@mantine/core";
import { Plus, Minus, Trash2, Heart } from "lucide-react";
import { Link } from "react-router-dom";
import { toast } from "sonner";

import styles from "./BasketCartItem.module.css";
import type { BasketItem } from "@/features/basket/model/types";
import { useUpdateBasketItem } from "@/features/basket/model/hooks/useUpdateBasketItem";
import { useDeleteBasketItem } from "@/features/basket/model/hooks/useDeleteBacketItem";
import { useAddToFavorites } from "@/features/favorites/model/hooks/useAddToFavorites";
import { useDeleteFavorite } from "@/features/favorites/model/hooks/useDeleteFavorite";
import { useGetFavorites } from "@/features/favorites/model/hooks/useGetFavorites";
import { useAuthStore } from "@/features/auth";
import { getAdvertisementPrice } from "@/shared/lib/types/advertisements";

type BasketCartItemProps = {
  item: BasketItem;
  imageUrl?: string;
  isSelected?: boolean;
  onToggle?: () => void;
  showCheckbox?: boolean;
};

export default function BasketCartItem({
  item,
  imageUrl,
  isSelected = false,
  onToggle,
  showCheckbox = true,
}: BasketCartItemProps) {
  const { mutate: updateItem, isPending: isUpdating } = useUpdateBasketItem();
  const { mutate: deleteItem, isPending: isDeleting } = useDeleteBasketItem();
  const user = useAuthStore((state) => state.user);
  const { data: favorites } = useGetFavorites();
  const addFavorite = useAddToFavorites();
  const deleteFavorite = useDeleteFavorite();

  const advertisement = item.advertisement;
  const favorite = favorites?.items?.find(
    (favoriteItem) => favoriteItem.advertisement?.id === advertisement?.id,
  );
  const isFavoritePending =
    addFavorite.isPending || deleteFavorite.isPending;

  const price = advertisement ? getAdvertisementPrice(advertisement) : 0;
  const currencySymbol = "грн";

  const handleQuantityChange = (delta: number) => {
    const newQuantity = item.quantity + delta;
    if (newQuantity > 0) {
      updateItem({ id: item.id, data: { quantity: newQuantity } });
    }
  };

  const handleFavoriteToggle = () => {
    if (!user) {
      toast.error(
        "Ви повинні бути зареєстровані, щоб додавати оголошення в обране.",
      );
      return;
    }

    if (!advertisement) return;

    if (favorite) {
      deleteFavorite.mutate(favorite.id);
    } else {
      addFavorite.mutate({
        advertisementId: advertisement.id,
        userId: user.id,
      });
    }
  };

  return (
    <div className={styles.card}>
      {showCheckbox && (
        <Checkbox
          checked={isSelected}
          onChange={onToggle}
          size="sm"
          vars={() => ({
            root: {
              "--checkbox-color": "var(--color-primary)",
            },
          })}
          className={styles.checkbox}
        />
      )}

      <div className={styles.imageWrapper}>
        <Image
          src={imageUrl}
          alt={advertisement?.title || "Товар"}
          fit="contain"
          className={styles.image}
        />
      </div>

      <div className={styles.content}>
        <div className={styles.headerRow}>
          <div className={styles.infoGroup}>
            <Link
              to={`/advertisement/${advertisement?.id}`}
              className={styles.titleLink}
            >
              <Text fw={600} size="md" lineClamp={2}>
                {advertisement?.title}
              </Text>
            </Link>

            <Text size="xs" c="green" fw={500} mt={4}>
              • В наявності
            </Text>
          </div>

          <div className={styles.priceGroup}>
            <Text fw={700} size="lg" className={styles.price}>
              {(price * item.quantity).toLocaleString()} {currencySymbol}
            </Text>
            {item.quantity > 1 && (
              <Text size="xs" c="dimmed">
                {price.toLocaleString()} {currencySymbol} / шт
              </Text>
            )}
          </div>
        </div>

        <div className={styles.footerRow}>
          <Group gap="sm">
            {showCheckbox && (
              <>
                <UnstyledButton
                  className={styles.actionLink}
                  onClick={handleFavoriteToggle}
                  disabled={isFavoritePending}
                >
                  <Heart
                    size={20}
                    fill={favorite ? "#000000" : "none"}
                    className={`${styles.favoriteIcon} ${
                      favorite ? styles.favoriteIconActive : ""
                    }`}
                  />
                  <Text fz={12} fw={500} className={styles.actionText}>
                    {favorite ? "В обраному" : "Зберегти"}
                  </Text>
                </UnstyledButton>
                <Text size="xs" c={"var(--text-secondary)"}>
                  |
                </Text>
              </>
            )}

            <UnstyledButton
              className={`${styles.actionLink} ${styles.deleteLink}`}
              onClick={() => deleteItem(item.id)}
              disabled={isDeleting || isUpdating}
            >
              <Trash2 size={20} />
              <Text fz={12} fw={500} className={styles.actionText}>
                Видалити
              </Text>
            </UnstyledButton>
          </Group>

          <div className={styles.quantityControls}>
            <ActionIcon
              variant="default"
              size="sm"
              onClick={() => handleQuantityChange(-1)}
              disabled={item.quantity <= 1 || isUpdating || isDeleting}
              radius="md"
            >
              <Minus size={14} />
            </ActionIcon>

            <Text size="sm" fw={600} px={12}>
              {item.quantity}
            </Text>

            <ActionIcon
              variant="default"
              size="sm"
              onClick={() => handleQuantityChange(1)}
              disabled={isUpdating || isDeleting}
              radius="md"
            >
              <Plus size={14} />
            </ActionIcon>
          </div>
        </div>
      </div>
    </div>
  );
}
