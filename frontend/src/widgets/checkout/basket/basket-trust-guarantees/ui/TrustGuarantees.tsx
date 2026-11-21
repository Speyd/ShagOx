import { Paper, Text, Stack, Group } from "@mantine/core";
import { ShieldCheck, Check } from "lucide-react";
import styles from "./TrustGuarantees.module.css";

const GUARANTEES = [
  "Офіційна гарантія на всі товари",
  "Легке повернення протягом 14 днів",
  "Підтримка 24/7",
];

export default function TrustGuarantees() {
  return (
    <Paper radius="md" p="lg" withBorder className={styles.card}>
      <Group gap="xs" mb="sm">
        <ShieldCheck size={20} className={styles.icon} />
        <Text fw={700} size="md" className={styles.title}>
          Покупка без ризиків
        </Text>
      </Group>

      <Stack gap="xs">
        {GUARANTEES.map((text, index) => (
          <Group key={index} gap="xs" align="center">
            <Check size={16} className={styles.checkIcon} />
            <Text size="sm" c="dimmed">
              {text}
            </Text>
          </Group>
        ))}
      </Stack>
    </Paper>
  );
}
