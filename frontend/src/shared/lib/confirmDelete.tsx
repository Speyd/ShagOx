import { Text } from "@mantine/core";
import { modals } from "@mantine/modals";

type ConfirmDeleteOptions = {
  title: string;
  message: string;
  onConfirm: () => void;
};

export function confirmDelete({ title, message, onConfirm }: ConfirmDeleteOptions) {
  modals.openConfirmModal({
    title,
    centered: true,
    children: <Text size="sm">{message}</Text>,
    labels: { confirm: "Видалити", cancel: "Скасувати" },
    confirmProps: { color: "red" },
    onConfirm,
  });
}
