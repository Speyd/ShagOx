import { Modal, Text } from "@mantine/core";
import styles from "./ConfirmModal.module.css";
import { type LucideIcon } from "lucide-react";
import Button from "../../buttons/button";

type ConfirmModalProps = {
  opened: boolean;
  onClose: () => void;
  onConfirm: () => void;
  icon?: LucideIcon;
  iconProps?: { size?: number; color?: string; className?: string };
  title: string;
  message: string;
  confirmLabel: string;
  isLoading?: boolean;
  confirmColor?: string;
};

export default function ConfirmModal({
  icon: Icon,
  iconProps,
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
      centered
      radius="lg"
      size="sm"
      withCloseButton={false}
      zIndex={1002}
      classNames={{
        header: styles.header,
        content: styles.content,
        body: styles.body,
      }}
    >
      {Icon && <Icon {...iconProps} />}

      <Text className={styles.title}>{title}</Text>

      <Text className={styles.message}>{message}</Text>

      <div className="divider" />

      <div className={styles.actions}>
        <Button
          variant="light"
          onClick={onClose}
          disabled={isLoading}
          className={styles.button}
        >
          Скасувати
        </Button>

        <Button
          variant="danger"
          onClick={onConfirm}
          loading={isLoading}
          color={confirmColor}
          className={styles.button}
        >
          {confirmLabel}
        </Button>
      </div>
    </Modal>
  );
}
