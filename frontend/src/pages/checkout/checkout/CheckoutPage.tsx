import { useMemo, useState } from "react";
import { useNavigate } from "react-router-dom";
import { Text } from "@mantine/core";
import Container from "@/shared/ui/layout/container";
import Button from "@/shared/ui/buttons/button";
import CheckoutDetailsForm from "@/widgets/checkout/checkout/checkout-details-form";
import OrderSummary from "@/widgets/checkout/checkout/checkout-order-summary";
import { useGetBasket } from "@/features/basket/model/hooks/useGetBasket";
import styles from "./CheckoutPage.module.css";

function readSelectedIds() {
  const savedIds = sessionStorage.getItem("checkoutItemIds");
  if (!savedIds) return new Set<number>();

  try {
    return new Set<number>(JSON.parse(savedIds) as number[]);
  } catch {
    return new Set<number>();
  }
}

export default function CheckoutPage() {
  const navigate = useNavigate();
  const { data: basket, isLoading } = useGetBasket();
  const [selectedIds] = useState(readSelectedIds);

  const items = useMemo(() => {
    const basketItems = basket?.basketItems ?? [];
    const selected = basketItems.filter((item) => selectedIds.has(item.id));
    return selected.length > 0 ? selected : basketItems;
  }, [basket, selectedIds]);

  if (isLoading) {
    return <div className={styles.loading}>Завантаження оформлення...</div>;
  }

  return (
    <div className={styles.checkoutPage}>
      <Container>
        {items.length === 0 ? (
          <div className={styles.empty}>
            <Text fw={600} fz={22}>
              Немає товарів для оформлення
            </Text>
            <Text c="dimmed">
              Поверніться до кошика та виберіть товари для замовлення.
            </Text>
            <Button onClick={() => navigate("/basket")}>
              Повернутися до кошика
            </Button>
          </div>
        ) : (
          <div className={styles.contentWrapper}>
            <OrderSummary items={items} />
            <CheckoutDetailsForm items={items} />
          </div>
        )}
      </Container>
    </div>
  );
}
