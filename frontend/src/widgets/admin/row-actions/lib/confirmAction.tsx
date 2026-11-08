import { Text } from "@mantine/core";
import { modals } from "@mantine/modals";

export function confirmAction({
  title,
  message,
  confirmLabel = "Видалити",
  danger = true,
  onConfirm,
}: {
  title: string;
  message: string;
  confirmLabel?: string;
  danger?: boolean;
  onConfirm: () => void;
}) {
  modals.openConfirmModal({
    title: <Text fw={700}>{title}</Text>,
    centered: true,
    radius: "md",
    children: (
      <Text size="sm" c="dimmed">
        {message}
      </Text>
    ),
    labels: { confirm: confirmLabel, cancel: "Скасувати" },
    confirmProps: { color: danger ? "red" : "blue" },
    onConfirm,
  });
}
