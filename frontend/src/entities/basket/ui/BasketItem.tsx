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

import styles from "./BasketItem.module.css";
import type { BasketItem } from "@/features/basket/model/types";
import { useUpdateBasketItem } from "@/features/basket/model/hooks/useUpdateBasketItem";
import { useDeleteBasketItem } from "@/features/basket/model/hooks/useDeleteBacketItem";
import { useToggleBasketFavorite } from "@/features/basket/model/hooks/useToggleBasketFavorite";
import { useBasketDrawer } from "@/features/basket/model/useBasketDrawer";
import {
  getBasketItemPrice,
  getBasketItemStock,
  getBasketItemVariant,
} from "@/features/basket/model/types";
import {
  formatAttributeValue,
  getVariantAttributeText,
} from "@/shared/lib/types/advertisements";

type BasketItemProps = {
  item: BasketItem;
  imageUrl?: string;
  isSelected?: boolean;
  onToggle?: () => void;
  showCheckbox?: boolean;
  onClose?: () => void;
};

export default function BasketItem({
  item,
  imageUrl,
  isSelected = false,
  onToggle,
  showCheckbox = true,
}: BasketItemProps) {
  const { mutate: updateItem, isPending: isUpdating } = useUpdateBasketItem();
  const { mutate: deleteItem, isPending: isDeleting } = useDeleteBasketItem();
  const advertisement = item.advertisement;
  const { closeBasket } = useBasketDrawer();
  const {
    favorite,
    isPending: isFavoritePending,
    toggleFavorite,
  } = useToggleBasketFavorite(advertisement?.id);

  const price = getBasketItemPrice(item);
  const variant = getBasketItemVariant(item);
  const stock = getBasketItemStock(item);
  const attributes = new Map<string, string>();

  Object.entries(advertisement?.attributes ?? {}).forEach(([key, value]) => {
    const text = formatAttributeValue(value);
    if (text) attributes.set(key, text);
  });

  variant?.attributes.forEach((attribute) => {
    const text = getVariantAttributeText(attribute);
    if (text) attributes.set(attribute.attributeKey, text);
  });

  const currencySymbol = item.advertisement?.currency?.symbol ?? "";

  const handleAdvertisementClick = () => {
    closeBasket();
  };

  const handleQuantityChange = (delta: number) => {
    const newQuantity = item.quantity + delta;
    if (newQuantity > 0 && newQuantity <= stock) {
      updateItem({ id: item.id, data: { quantity: newQuantity } });
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
              onClick={handleAdvertisementClick}
            >
              <Text fw={600} size="md" lineClamp={2}>
                {advertisement?.title}
              </Text>
            </Link>

            <Text size="xs" c={stock > 0 ? "green" : "red"} fw={500} mt={4}>
              • {stock > 0 ? `В наявності (${stock})` : "Немає в наявності"}
            </Text>
            <div className={styles.attributes}>
              {[...attributes.entries()].map(([key, value]) => (
                <Text
                  key={key}
                  size="xs"
                  c="dimmed"
                  fw={500}
                  className={styles.attribute}
                >
                  {key}: {value}
                </Text>
              ))}
            </div>
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
                  onClick={toggleFavorite}
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
              disabled={isUpdating || isDeleting || item.quantity >= stock}
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
