import { useState, useMemo } from "react";
import { Navigate, useNavigate } from "react-router-dom";
import { Title, Checkbox, Group, UnstyledButton, Text } from "@mantine/core";
import { Trash2 } from "lucide-react";
import { useDisclosure } from "@mantine/hooks";
import { useGetBasket } from "@/features/basket/model/hooks/useGetBasket";
import { getBasketTotal } from "@/features/basket/model/types";
import BasketSellerGroup from "@/widgets/checkout/basket/basket-seller-group";
import { splitBySeller } from "@/features/basket/model/splitBySeller";
import type { BasketItem } from "@/features/basket/model/types";
import BasketSummary from "@/widgets/checkout/basket/basket-summary";
import styles from "./BasketPage.module.css";

import TrustGuarantees from "@/widgets/checkout/basket/basket-trust-guarantees";
import BasketFreeShipping from "@/widgets/checkout/basket/basket-free-shipping";
import { useDeleteBasketItem } from "@/features/basket/model/hooks/useDeleteBacketItem";
import Container from "@/shared/ui/layout/container";
import ConfirmModal from "@/shared/ui/feedback/confirm-modal";
import { BasketPageSkeleton } from "./BasketPageSkeleton";

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

  // Одне замовлення = один продавець, тож кошик оформлюється по продавцях.
  const groups = useMemo(() => splitBySeller(items), [items]);
  const selectedGroups = useMemo(
    () => splitBySeller(selectedItems),
    [selectedItems],
  );
  const hasManySellers = groups.length > 1;

  // Підсумок рахує те, що реально піде в наступне замовлення.
  const nextOrderItems = useMemo(
    () => selectedGroups[0]?.items ?? [],
    [selectedGroups],
  );
  const selectedTotal = useMemo(
    () => getBasketTotal(nextOrderItems),
    [nextOrderItems],
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
    try {
      await Promise.all(items.map((item) => deleteItem(item.id)));
      closeClearModal();
    } catch {
      // Individual mutation hooks report the server error.
    }
  };

  const checkoutItems = (itemsToOrder: BasketItem[]) => {
    if (itemsToOrder.length === 0) return;

    sessionStorage.setItem(
      "checkoutItemIds",
      JSON.stringify(itemsToOrder.map((item) => item.id)),
    );
    navigate("/checkout");
  };

  const handleCheckout = () => checkoutItems(nextOrderItems);

  if (isLoading) {
    return <BasketPageSkeleton />;
  }

  if (items.length === 0) return <Navigate to="/catalog" replace />;

  return (
    <section className={styles.pageWrapper}>
      <Container>
        <div className={styles.content}>
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
                <div className={styles.itemsList}>
                  {groups.map((group) => (
                    <BasketSellerGroup
                      key={group.sellerId}
                      sellerId={group.sellerId}
                      items={group.items}
                      selectedIds={selectedIds}
                      onToggleItem={handleToggleItem}
                      onCheckout={checkoutItems}
                      showCheckout={hasManySellers}
                    />
                  ))}
                </div>
                <div className={styles.freeShippingWrapper}>
                  <BasketFreeShipping currentTotal={selectedTotal.amount} />
                </div>
              </div>

              <div className={styles.rightSection}>
                <BasketSummary
                  totalCount={nextOrderItems.length}
                  subtotalPrice={selectedTotal.amount}
                  currencySymbol={selectedTotal.symbol}
                  discountPrice={0}
                  onCheckout={handleCheckout}
                  checkoutLabel={
                    selectedGroups.length > 1
                      ? "Оформити першого продавця"
                      : "Перейти до оформлення"
                  }
                  notice={
                    selectedGroups.length > 1
                      ? `Обрано товари ${selectedGroups.length} продавців. Кожен продавець відправляє своє замовлення окремо, тому вони оформлюються по черзі.`
                      : undefined
                  }
                />
                <TrustGuarantees />
              </div>
            </div>
          </>
        </div>
      </Container>
    </section>
  );
}
