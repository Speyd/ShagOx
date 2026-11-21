import {
  Paper,
  Text,
  Group,
  Button,
  Divider,
  Badge,
  Stack,
} from "@mantine/core";
import { ShieldCheck, ArrowRight } from "lucide-react";
import styles from "./BasketSummary.module.css";
import Price from "@/shared/ui/data-display/price";

type BasketSummaryProps = {
  totalCount: number;
  subtotalPrice: number;
  currencySymbol?: string;
  discountPrice?: number;
  freeShippingThreshold?: number;
  onCheckout?: () => void;
  /** Текст кнопки (наприклад, коли оформлюється лише частина кошика). */
  checkoutLabel?: string;
  /** Підказка над кнопкою оформлення. */
  notice?: string;
};

export default function BasketSummary({
  totalCount,
  subtotalPrice,
  currencySymbol = "₴",
  discountPrice = 0,
  freeShippingThreshold = 5000,
  onCheckout,
  checkoutLabel = "Перейти до оформлення",
  notice,
}: BasketSummaryProps) {
  const isFreeShipping = subtotalPrice >= freeShippingThreshold;
  const finalPrice = Math.max(0, subtotalPrice - discountPrice);

  return (
    <Paper radius="lg" p="xl" withBorder className={styles.card}>
      <Text fw={700} size="xl" className={styles.title}>
        Підсумок замовлення
      </Text>

      <Stack gap="sm" mt="md">
        <Group justify="space-between">
          <Text size="sm" c="dimmed">
            Товари ({totalCount})
          </Text>
          <Text size="sm" fw={600} className={styles.value}>
            {subtotalPrice.toLocaleString()} {currencySymbol}
          </Text>
        </Group>

        {discountPrice > 0 && (
          <Group justify="space-between">
            <Text size="sm" c="dimmed">
              Знижка
            </Text>
            <Text size="sm" c="red" fw={600}>
              -{discountPrice.toLocaleString()} {currencySymbol}
            </Text>
          </Group>
        )}

        <Group justify="space-between">
          <Text size="sm" c="dimmed">
            Доставка
          </Text>
          {isFreeShipping ? (
            <Badge color="green" variant="light" size="sm" radius="md">
              Безкоштовно
            </Badge>
          ) : (
            <Text size="sm" c="dimmed">
              За тарифами перевізника
            </Text>
          )}
        </Group>
      </Stack>

      <Divider my="lg" />

      <Group justify="space-between" align="baseline" mb="lg">
        <Text fw={600} size="md">
          До сплати
        </Text>
        <Price price={finalPrice} currency={currencySymbol} />
      </Group>

      {notice && (
        <Text size="sm" c="dimmed" mb="md">
          {notice}
        </Text>
      )}

      <Button
        onClick={onCheckout}
        fullWidth
        size="md"
        radius="md"
        className={styles.checkoutButton}
        disabled={totalCount === 0}
        rightSection={<ArrowRight size={18} />}
      >
        {checkoutLabel}
      </Button>

      <Group justify="center" gap={6} mt="md">
        <ShieldCheck size={16} color="#64748b" />
        <Text size="xs" c="dimmed">
          Безпечне оформлення замовлення
        </Text>
      </Group>
    </Paper>
  );
}
