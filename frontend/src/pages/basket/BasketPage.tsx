import { useState, useMemo } from "react";
import { useNavigate } from "react-router-dom";
import { Title, Checkbox, Group, UnstyledButton, Text } from "@mantine/core";
import { Trash2 } from "lucide-react";
import { useDisclosure } from "@mantine/hooks";
import { useGetBasket } from "@/features/basket/model/hooks/useGetBasket";
import { getAdvertisementPrice } from "@/shared/lib/types/advertisements";
import BasketItemsList from "@/widgets/basket/basket-items-list";
import BasketSummary from "@/widgets/basket/basket-summary";
import styles from "./BasketPage.module.css";

import TrustGuarantees from "@/widgets/basket/basket-trust-guarantees";
import BasketFreeShipping from "@/widgets/basket/basket-free-shipping";
import { useDeleteBasketItem } from "@/features/basket/model/hooks/useDeleteBacketItem";
import Container from "@/shared/ui/container";
import BasketEmpty from "@/widgets/basket/basket-empty";
import ConfirmModal from "@/shared/ui/confirm-modal";

export default function BasketPage() {
  const navigate = useNavigate();
  const { data: basket, isLoading } = useGetBasket();
  const { mutateAsync: deleteItem, isPending: isDeleting } =
    useDeleteBasketItem();
  const [clearModalOpened, { open: openClearModal, close: closeClearModal }] =
    useDisclosure(false);

  const items = useMemo(() => basket?.basketItems || [], [basket]);

  const [unselectedIds, setUnselectedIds] = useState<Set<number>>(new Set());

  const selectedIds = useMemo(() => {
    const active = new Set<number>();
    items.forEach((item) => {
      if (!unselectedIds.has(item.id)) {
        active.add(item.id);
      }
    });
    return active;
  }, [items, unselectedIds]);

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
      setUnselectedIds(new Set(items.map((item) => item.id)));
    } else {
      setUnselectedIds(new Set());
    }
  };

  const handleToggleItem = (id: number) => {
    setUnselectedIds((prev) => {
      const next = new Set(prev);
      if (next.has(id)) {
        next.delete(id);
      } else {
        next.add(id);
      }
      return next;
    });
  };

  const handleClearCart = async () => {
    await Promise.all(items.map((item) => deleteItem(item.id)));
    closeClearModal();
  };

  const handleCheckout = () => {
    if (selectedIds.size === 0) return;

    sessionStorage.setItem(
      "checkoutItemIds",
      JSON.stringify(Array.from(selectedIds)),
    );
    navigate("/checkout");
  };

  if (isLoading) {
    return <div className={styles.loading}>Завантаження кошика...</div>;
  }

  return (
    <div className={styles.pageWrapper}>
      <Container>
        <div className={styles.content}>
          {items.length === 0 ? (
            <BasketEmpty />
          ) : (
            <>
              <div className={styles.header}>
                <Title fz={28} fw={700}>
                  Кошик
                </Title>
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
                    onClick={openClearModal}
                    className={styles.clearBtn}
                  >
                    <Group gap={6}>
                      <Trash2 size={16} />
                      <Text size="sm" className={styles.clearText}>
                        Очистити кошик
                      </Text>
                    </Group>
                  </UnstyledButton>
                </div>
              </div>

              <ConfirmModal
                opened={clearModalOpened}
                onClose={closeClearModal}
                onConfirm={handleClearCart}
                title="Очистити кошик"
                message="Ви впевнені, що хочете видалити всі товари з кошика?"
                confirmLabel="Очистити"
                confirmColor="red"
                isLoading={isDeleting}
              />

              <div className={styles.layout}>
                <div className={styles.leftSection}>
                  <BasketItemsList
                    items={items}
                    selectedIds={selectedIds}
                    onToggleItem={handleToggleItem}
                    isLoading={isLoading}
                    className={styles.itemsList}
                  />
                  <div className={styles.freeShippingWrapper}>
                    <BasketFreeShipping currentTotal={selectedTotal} />
                  </div>
                </div>

                <div className={styles.rightSection}>
                  <BasketSummary
                    totalCount={selectedItems.length}
                    subtotalPrice={selectedTotal}
                    discountPrice={0}
                    onCheckout={handleCheckout}
                  />
                  <TrustGuarantees />
                </div>
              </div>
            </>
          )}
        </div>
      </Container>
    </div>
  );
}
