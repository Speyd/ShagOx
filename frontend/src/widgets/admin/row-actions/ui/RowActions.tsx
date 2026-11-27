import type { ReactNode } from "react";
import { ActionIcon, Tooltip } from "@mantine/core";
import { Pencil, Trash2 } from "lucide-react";
import { confirmAction } from "../lib/confirmAction";
import styles from "./RowActions.module.css";

type RowActionsProps = {
  onEdit?: () => void;
  onDelete?: () => void;
  deleteTitle?: string;
  deleteMessage?: string;
  children?: ReactNode;
};

export default function RowActions({
  onEdit,
  onDelete,
  deleteTitle = "Видалити запис",
  deleteMessage = "Ви впевнені? Цю дію неможливо скасувати.",
  children,
}: RowActionsProps) {
  return (
    <div className={styles.actions}>
      {children}
      {onEdit && (
        <Tooltip label="Редагувати">
          <ActionIcon
            variant="light"
            radius="md"
            size="lg"
            onClick={onEdit}
            aria-label="Редагувати"
          >
            <Pencil size={16} />
          </ActionIcon>
        </Tooltip>
      )}
      {onDelete && (
        <Tooltip label="Видалити">
          <ActionIcon
            variant="light"
            color="red"
            radius="md"
            size="lg"
            aria-label="Видалити"
            onClick={() =>
              confirmAction({
                title: deleteTitle,
                message: deleteMessage,
                onConfirm: onDelete,
              })
            }
          >
            <Trash2 size={16} />
          </ActionIcon>
        </Tooltip>
      )}
    </div>
  );
}
