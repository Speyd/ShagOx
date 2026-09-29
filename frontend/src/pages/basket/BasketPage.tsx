import { useState, useEffect, useMemo } from "react";
import { Title, Checkbox, Group, UnstyledButton, Text } from "@mantine/core";
import { Trash2 } from "lucide-react";
import { useGetBasket } from "@/features/basket/model/hooks/useGetBasket";
import { getAdvertisementPrice } from "@/shared/lib/types/advertisements";
import BasketItemsList from "@/widgets/basket/basket-items-list";
import BasketSummary from "@/widgets/basket/basket-summary";
import styles from "./BasketPage.module.css";

import TrustGuarantees from "@/widgets/basket/basket-trust-guarantees";
import BasketFreeShipping from "@/widgets/basket/basket-free-shipping";
import { useDeleteBasketItem } from "@/features/basket/model/hooks/useDeleteBacketItem";
import Container from "@/shared/ui/container";

export default function BasketPage() {
  const { data: basket, isLoading, isError } = useGetBasket();
  const { mutate: deleteItem } = useDeleteBasketItem();

  const items = useMemo(() => basket?.basketItems || [], [basket]);

  const [selectedIds, setSelectedIds] = useState<Set<number>>(new Set());

  useEffect(() => {
    if (items.length > 0) {
      setSelectedIds(new Set(items.map((item) => item.id)));
    }
  }, [items]);

  const selectedItems = useMemo(
    () => items.filter((item) => selectedIds.has(item.id)),
    [items, selectedIds],
  );

  const selectedTotal = useMemo(
    () =>
      selectedItems.reduce(
        (acc, item) =>
          acc + getAdvertisementPrice(item.advertisement) * item.quantity,
        0,
      ),
    [selectedItems],
  );

  const isAllSelected = items.length > 0 && selectedIds.size === items.length;

  const handleToggleAll = () => {
    if (isAllSelected) {
      setSelectedIds(new Set());
    } else {
      setSelectedIds(new Set(items.map((item) => item.id)));
    }
  };

  const handleToggleItem = (id: number) => {
    setSelectedIds((prev) => {
      const next = new Set(prev);
      if (next.has(id)) {
        next.delete(id);
      } else {
        next.add(id);
      }
      return next;
    });
  };

  const handleClearCart = () => {
    items.forEach((item) => deleteItem(item.id));
  };

  if (isLoading) {
    return <div className={styles.loading}>Завантаження кошика...</div>;
  }

  if (isError || !basket) {
    return (
      <div className={styles.emptyState}>Увійдіть, щоб переглянути кошик.</div>
    );
  }

  return (
    <div className={styles.pageWrapper}>
      <Container>
        <Title order={1} className={styles.title}>
          Кошик
        </Title>

        {items.length === 0 ? (
          <Text c="dimmed">Кошик порожній.</Text>
        ) : (
          <>
            <div className={styles.headerControls}>
              <Checkbox
                label={`Вибрати все (${items.length})`}
                checked={isAllSelected}
                onChange={handleToggleAll}
                size="sm"
                vars={() => ({
                  root: {
                    "--checkbox-color": "var(--color-primary)",
                  },
                })}
              />

              <UnstyledButton
                onClick={handleClearCart}
                className={styles.clearBtn}
              >
                <Group gap={6}>
                  <Trash2 size={16} color="#64748b" />
                  <Text size="sm" c="dimmed">
                    Очистити кошик
                  </Text>
                </Group>
              </UnstyledButton>
            </div>

            <div className={styles.layout}>
              <div className={styles.leftSection}>
                <BasketItemsList
                  items={items}
                  selectedIds={selectedIds}
                  onToggleItem={handleToggleItem}
                  isLoading={isLoading}
                />
                <BasketFreeShipping currentTotal={selectedTotal} />
              </div>

              <div className={styles.rightSection}>
                <BasketSummary
                  totalCount={selectedItems.length}
                  subtotalPrice={selectedTotal}
                  discountPrice={0}
                />
                <TrustGuarantees />
              </div>
            </div>
          </>
        )}
      </Container>
    </div>
  );
}
