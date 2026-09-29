import { Divider, Flex, Group, Text, Title } from "@mantine/core";
import ApplyPromoCode from "@/features/apply-promo-code/ui/ApplyPromoCode";

import styles from "./OrderSummary.module.css";

export default function OrderSummary() {
  return (
    <section className={styles.orderSummary}>
      <Title order={2} fz={24} fw={600} mb="lg">
        Підсумок замовлення
      </Title>

      <div className={styles.promoCodeContainer}>
        <ApplyPromoCode />

        <Divider my="xs" color="gray.2" />

        <Flex direction="column" gap="sm">
          <Group justify="space-between">
            <Text c="dimmed" fz={16}>
              Підсумок
            </Text>
            <Text fw={500} fz={16}>
              16 855 грн
            </Text>
          </Group>

          <Group justify="space-between">
            <Text c="dimmed" fz={16}>
              Знижка
            </Text>
            <Text fw={500} fz={16} c="red.6">
              -400 грн
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
              16 455 грн
            </Text>
          </Group>
        </Flex>
      </div>
    </section>
  );
}
