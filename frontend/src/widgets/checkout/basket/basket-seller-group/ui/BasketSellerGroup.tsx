import { Text } from "@mantine/core";
import { Store } from "lucide-react";
import Button from "@/shared/ui/buttons/button";
import { useUserDisplayName } from "@/entities/user/model/hooks/useUserDisplayName";
import { getBasketTotal, type BasketItem } from "@/features/basket/model/types";
import BasketItemsList from "@/widgets/checkout/basket/basket-items-list";
import styles from "./BasketSellerGroup.module.css";

type BasketSellerGroupProps = {
  sellerId: number;
  items: BasketItem[];
  selectedIds: Set<number>;
  onToggleItem: (id: number) => void;
  onCheckout: (items: BasketItem[]) => void;
  /** Показувати кнопку оформлення групи (коли продавців кілька). */
  showCheckout: boolean;
};

/** Товари одного продавця в кошику — оформлюються окремим замовленням. */
export default function BasketSellerGroup({
  sellerId,
  items,
  selectedIds,
  onToggleItem,
  onCheckout,
  showCheckout,
}: BasketSellerGroupProps) {
  const sellerName = useUserDisplayName(sellerId) ?? "Продавець";

  const selected = items.filter((item) => selectedIds.has(item.id));
  const total = getBasketTotal(selected);

  return (
    <section className={styles.group}>
      <header className={styles.header}>
        <div className={styles.seller}>
          <Store size={18} />
          <Text fw={600} fz={15}>
            {sellerName}
          </Text>
        </div>

        {showCheckout && (
          <div className={styles.checkout}>
            {selected.length > 0 && (
              <Text fz={14} c="dimmed">
                {total.amount.toLocaleString("uk-UA")} {total.symbol}
              </Text>
            )}
            <Button
              size="sm"
              disabled={selected.length === 0}
              onClick={() => onCheckout(selected)}
            >
              Оформити
            </Button>
          </div>
        )}
      </header>

      <BasketItemsList
        items={items}
        selectedIds={selectedIds}
        onToggleItem={onToggleItem}
      />
    </section>
  );
}
