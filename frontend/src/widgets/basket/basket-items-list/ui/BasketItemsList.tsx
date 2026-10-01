import BasketCartItem from "@/entities/basket";
import styles from "./BasketItemsList.module.css";
import type { BasketItem } from "@/features/basket/model/types";

type BasketItemsListProps = {
  items: BasketItem[];
  selectedIds?: Set<number>;
  onToggleItem?: (id: number) => void;
  isLoading?: boolean;
  className?: string;
};

export default function BasketItemsList({
  items,
  selectedIds,
  onToggleItem,
  isLoading = false,
  className,
}: BasketItemsListProps) {
  if (isLoading) {
    return <div className={styles.loading}>Завантаження товарів...</div>;
  }

  if (!items.length) {
    return <div className={styles.empty}>У кошику немає товарів</div>;
  }

  const isSelectable = Boolean(selectedIds && onToggleItem);

  return (
    <div className={`${styles.list} ${className || ""}`}>
      {items.map((item) => (
        <BasketCartItem
          key={item.id}
          item={item}
          imageUrl={item.advertisement?.images?.[0]?.url}
          isSelected={selectedIds?.has(item.id)}
          onToggle={isSelectable ? () => onToggleItem?.(item.id) : undefined}
          showCheckbox={isSelectable}
        />
      ))}
    </div>
  );
}
