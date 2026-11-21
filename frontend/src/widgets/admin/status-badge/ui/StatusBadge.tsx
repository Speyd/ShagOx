import { Badge } from "@mantine/core";
import type { EntityStatus } from "@/shared/api/crud";

const STATUS_META: Record<string, { label: string; color: string }> = {
  Active: { label: "Активний", color: "teal" },
  Hidden: { label: "Прихований", color: "gray" },
  Deleted: { label: "Видалений", color: "red" },
};

type StatusBadgeProps = {
  status?: EntityStatus | string;
};

export default function StatusBadge({ status = "Active" }: StatusBadgeProps) {
  const meta = STATUS_META[status] ?? { label: status, color: "gray" };
  return (
    <Badge variant="light" color={meta.color} radius="sm">
      {meta.label}
    </Badge>
  );
}
