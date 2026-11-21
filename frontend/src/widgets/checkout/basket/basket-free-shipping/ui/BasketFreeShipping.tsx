import { Paper, Text, Group, Progress, Stack } from "@mantine/core";
import { Gift } from "lucide-react";
import styles from "./BasketFreeShipping.module.css";

type BasketFreeShippingProps = {
  currentTotal: number;
  targetThreshold?: number;
};

export default function BasketFreeShipping({
  currentTotal,
  targetThreshold = 5000,
}: BasketFreeShippingProps) {
  const remaining = Math.max(0, targetThreshold - currentTotal);
  const percentage = Math.min(100, (currentTotal / targetThreshold) * 100);
  const isUnlocked = remaining === 0;

  return (
    <Paper radius="md" p="md" withBorder className={styles.card}>
      <Group justify="space-between" align="center" wrap="nowrap">
        <Group gap="sm" align="center" wrap="nowrap">
          <div className={styles.iconWrapper}>
            <Gift size={22} className={styles.icon} />
          </div>

          <Stack gap={2}>
            <Text fw={700} size="sm" className={styles.title}>
              {isUnlocked ? (
                "Ви отримали безкоштовну доставку!"
              ) : (
                <>Додайте товарів на ще {remaining.toLocaleString()} грн</>
              )}
            </Text>

            <Text size="xs" c="dimmed">
              {isUnlocked
                ? "Доставка за рахунок магазину"
                : "і отримайте безкоштовну доставку"}
            </Text>
          </Stack>
        </Group>

        <Stack gap={4} align="flex-end" className={styles.progressGroup}>
          <Progress
            value={percentage}
            radius="xl"
            size="sm"
            color="var(--color-base)"
            className={styles.progressBar}
          />
          {!isUnlocked && (
            <Text size="xs" c="dimmed">
              Потрібно {remaining.toLocaleString()} грн
            </Text>
          )}
        </Stack>
      </Group>
    </Paper>
  );
}
