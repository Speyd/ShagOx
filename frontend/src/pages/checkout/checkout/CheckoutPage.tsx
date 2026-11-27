import { useMemo, useState } from "react";
import { useNavigate } from "react-router-dom";
import { Text } from "@mantine/core";
import Container from "@/shared/ui/layout/container";
import Button from "@/shared/ui/buttons/button";
import CheckoutDetailsForm from "@/widgets/checkout/checkout/checkout-details-form";
import OrderSummary from "@/widgets/checkout/checkout/checkout-order-summary";
import { useGetBasket } from "@/features/basket/model/hooks/useGetBasket";
import { splitBySeller } from "@/features/basket/model/splitBySeller";
import { useUserDisplayName } from "@/entities/user/model/hooks/useUserDisplayName";
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

  const groups = useMemo(() => {
    const basketItems = basket?.basketItems ?? [];
    const selected = basketItems.filter((item) => selectedIds.has(item.id));
    return splitBySeller(selected.length > 0 ? selected : basketItems);
  }, [basket, selectedIds]);

  const items = groups[0]?.items ?? [];
  const otherSellers = groups.length - 1;
  const sellerName = useUserDisplayName(groups[0]?.sellerId);

  if (isLoading) {
    return <div className={styles.loading}>Завантаження оформлення...</div>;
  }

  return (
    <div className={styles.checkoutPage}>
      <Container className={styles.container}>
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
          <>
            {otherSellers > 0 && (
              <div className={styles.notice}>
                <Text fz={14}>
                  Замовлення оформлюється окремо для кожного продавця. Зараз —
                  товари продавця <b>{sellerName ?? "…"}</b>; товари ще{" "}
                  {otherSellers} продавц{otherSellers === 1 ? "я" : "ів"}{" "}
                  залишаться в кошику.
                </Text>
              </div>
            )}
            <div className={styles.contentWrapper}>
              <OrderSummary items={items} />
              <CheckoutDetailsForm items={items} />
            </div>
          </>
        )}
      </Container>
    </div>
  );
}
