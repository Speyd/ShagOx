import { Divider, ScrollArea, Text } from "@mantine/core";
import styles from "./OrderSummary.module.css";
import BasketItemsList from "@/widgets/checkout/basket/basket-items-list";
import type { BasketItem } from "@/features/basket/model/types";
import { getBasketTotal } from "@/features/basket/model/types";

type OrderSummaryProps = {
  items: BasketItem[];
};

export default function OrderSummary({ items }: OrderSummaryProps) {
  const { amount: subtotalPrice, symbol: currencySymbol = "₴" } =
    getBasketTotal(items);
  const itemsCount = items.reduce((sum, item) => sum + item.quantity, 0);

  return (
    <section className={styles.orderSummary}>
      <Text fz={24} fw={600} mb="lg">
        Ваше замовлення
      </Text>

      <div className={styles.promoCodeContainer}>
        <ScrollArea
          className={styles.scrollArea}
          type="hover"
          offsetScrollbars
          scrollbarSize={6}
        >
          <BasketItemsList items={items} />
        </ScrollArea>

        <Divider my="xs" color="gray.2" />

        <div className={styles.row}>
          <Text c="dimmed" fz={16}>
            Товари ({itemsCount})
          </Text>
          <Text fw={500} fz={16}>
            {subtotalPrice.toLocaleString("uk-UA")} {currencySymbol}
          </Text>
        </div>

        <div className={styles.row}>
          <Text c="dimmed" fz={16}>
            Доставка
          </Text>
          <Text fw={500} fz={14} c="dimmed">
            За тарифами перевізника
          </Text>
        </div>

        <Divider my="xs" color="gray.2" />

        <div className={styles.row}>
          <Text fw={600} fz={18}>
            До сплати
          </Text>
          <Text fw={700} fz={20}>
            {subtotalPrice.toLocaleString("uk-UA")} {currencySymbol}
          </Text>
        </div>
      </div>
    </section>
  );
}
