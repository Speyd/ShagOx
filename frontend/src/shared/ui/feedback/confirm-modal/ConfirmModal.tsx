import { Button, Group, Modal, Text } from "@mantine/core";
import styles from "./ConfirmModal.module.css";

type ConfirmModalProps = {
  opened: boolean;
  onClose: () => void;
  onConfirm: () => void;
  title: string;
  message: string;
  confirmLabel: string;
  isLoading?: boolean;
  confirmColor?: string;
};

export default function ConfirmModal({
  opened,
  onClose,
  onConfirm,
  title,
  message,
  confirmLabel,
  isLoading = false,
  confirmColor = "var(--color-primary)",
}: ConfirmModalProps) {
  return (
    <Modal
      opened={opened}
      onClose={onClose}
      title={title}
      centered
      radius="md"
      zIndex={1002}
      classNames={{
        title: styles.title,
        content: styles.content,
        body: styles.body,
      }}
    >
      <Text className={styles.message}>{message}</Text>

      <Group className={styles.actions} justify="flex-end" gap="sm">
        <Button variant="default" onClick={onClose} disabled={isLoading}>
          Скасувати
        </Button>

        <Button
          onClick={onConfirm}
          loading={isLoading}
          color={confirmColor}
        >
          {confirmLabel}
        </Button>
      </Group>
    </Modal>
  );
}
