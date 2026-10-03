import { Divider, Flex, Group, ScrollArea, Text, Title } from "@mantine/core";
import ApplyPromoCode from "@/features/apply-promo-code/ui/ApplyPromoCode";

import styles from "./OrderSummary.module.css";
import BasketItemsList from "@/widgets/basket/basket-items-list";
import type { BasketItem } from "@/features/basket/model/types";
import { getAdvertisementPrice } from "@/shared/lib/types/advertisements";

type OrderSummaryProps = {
  items: BasketItem[];
};

export default function OrderSummary({ items }: OrderSummaryProps) {
  const subtotalPrice = items.reduce(
    (sum, item) =>
      sum + getAdvertisementPrice(item.advertisement) * item.quantity,
    0,
  );
  return (
    <section className={styles.orderSummary}>
      <Title order={2} fz={24} fw={600} mb="lg">
        Підсумок замовлення
      </Title>

      <div className={styles.promoCodeContainer}>
        <ScrollArea
          className={styles.scrollArea}
          type="hover"
          offsetScrollbars
          scrollbarSize={6}
        >
          <BasketItemsList items={items} />
        </ScrollArea>

        <ApplyPromoCode />

        <Divider my="xs" color="gray.2" />

        <Flex direction="column" gap="sm">
          <Group justify="space-between">
            <Text c="dimmed" fz={16}>
              Підсумок
            </Text>
            <Text fw={500} fz={16}>
              {subtotalPrice.toLocaleString()} грн
            </Text>
          </Group>

          <Group justify="space-between">
            <Text c="dimmed" fz={16}>
              Знижка
            </Text>
            <Text fw={500} fz={16} c="red.6">
              0 грн
            </Text>
          </Group>

          <Group justify="space-between">
            <Text c="dimmed" fz={16}>
              Відправлення
            </Text>
            <Text fw={500} fz={14} c="green.7">
              Безкоштовно
            </Text>
          </Group>

          <Divider my="xs" color="gray.2" />

          <Group justify="space-between">
            <Text fw={600} fz={18}>
              До сплати
            </Text>
            <Text fw={700} fz={20}>
              {subtotalPrice.toLocaleString()} грн
            </Text>
          </Group>
        </Flex>
      </div>
    </section>
  );
}
