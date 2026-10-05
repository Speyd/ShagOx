import { useMemo, useState } from "react";
import { Link } from "react-router-dom";
import { Button, Text } from "@mantine/core";
import Container from "@/shared/ui/container";
import CheckoutDetailsForm from "@/widgets/checkout/checkout-details-form";
import OrderSummary from "@/widgets/checkout/checkout-order-summary";
import CheckoutStepper from "@/widgets/checkout/checkout-stepper";
import { useGetBasket } from "@/features/basket/model/hooks/useGetBasket";
import styles from "./CheckoutPage.module.css";

export default function CheckoutPage() {
  const { data: basket, isLoading } = useGetBasket();
  const [selectedIds] = useState(() => {
    const savedIds = sessionStorage.getItem("checkoutItemIds");

    if (!savedIds) return new Set<number>();

    try {
      return new Set<number>(JSON.parse(savedIds) as number[]);
    } catch {
      return new Set<number>();
    }
  }, []);
  const items = useMemo(
    () =>
      (basket?.basketItems || []).filter((item) => selectedIds.has(item.id)),
    [basket, selectedIds],
  );

  if (isLoading) {
    return <div className={styles.loading}>Завантаження оформлення...</div>;
  }

  return (
    <div className={styles.checkoutPage}>
      <Container>
        <CheckoutStepper />
        {items.length === 0 ? (
          <div className={styles.empty}>
            <Text fw={600} fz={22}>
              Немає вибраних товарів
            </Text>
            <Text c="dimmed">
              Поверніться до кошика та виберіть товари для оформлення.
            </Text>
            <Button
              component={Link}
              to="/basket"
              radius="md"
              className={styles.primaryButton}
            >
              Повернутися до кошика
            </Button>
          </div>
        ) : (
          <div className={styles.contentWrapper}>
            <OrderSummary items={items} />
            <CheckoutDetailsForm />
          </div>
        )}
      </Container>
    </div>
  );
}
