import {
  Checkbox,
  Group,
  Image,
  Text,
  ActionIcon,
  UnstyledButton,
} from "@mantine/core";
import { Plus, Minus, Trash2, Bookmark } from "lucide-react";
import { Link } from "react-router-dom";

import styles from "./BasketCartItem.module.css";
import type { BasketItem } from "@/features/basket/model/types";
import { useUpdateBasketItem } from "@/features/basket/model/hooks/useUpdateBasketItem";
import { useDeleteBasketItem } from "@/features/basket/model/hooks/useDeleteBacketItem";
import { useGetImage } from "@/shared/api/images/useGetImage";
import { getAdvertisementPrice } from "@/shared/lib/types/advertisements";

type BasketCartItemProps = {
  item: BasketItem;
  imageUrl?: string;
  isSelected: boolean;
  onToggle: () => void;
};

export default function BasketCartItem({
  item,
  imageUrl,
  isSelected,
  onToggle,
}: BasketCartItemProps) {
  const { mutate: updateItem, isPending: isUpdating } = useUpdateBasketItem();
  const { mutate: deleteItem, isPending: isDeleting } = useDeleteBasketItem();

  const advertisement = item.advertisement;
  const price = advertisement ? getAdvertisementPrice(advertisement) : 0;
  const currencySymbol = "грн";

  const firstImageId = advertisement?.imageIds?.[0];

  const { data: imageData } = useGetImage(firstImageId);

  const displayImage =
    imageUrl || imageData?.url || imageData || "/placeholder-image.png";

  const handleQuantityChange = (delta: number) => {
    const newQuantity = item.quantity + delta;
    if (newQuantity > 0) {
      updateItem({ id: item.id, data: { quantity: newQuantity } });
    }
  };

  return (
    <div className={styles.card}>
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

      <div className={styles.imageWrapper}>
        <Image
          src={displayImage}
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
          <Group gap="xl">
            <UnstyledButton className={styles.actionLink}>
              <Bookmark size={15} />
              <span>Зберегти на потім</span>
            </UnstyledButton>

            <Text size="xs" c="gray.4">
              |
            </Text>

            <UnstyledButton
              className={`${styles.actionLink} ${styles.deleteLink}`}
              onClick={() => deleteItem(item.id)}
              disabled={isDeleting || isUpdating}
            >
              <Trash2 size={15} />
              <span>Видалити</span>
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
