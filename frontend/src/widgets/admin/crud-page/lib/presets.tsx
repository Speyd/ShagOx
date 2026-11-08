import { Text } from "@mantine/core";
import type { ColumnDef } from "@tanstack/react-table";
import type { FieldConfig } from "../model/types";

export const CODE_PATTERN = /^[a-zA-Z0-9_.-]+$/;

export function codeField(label = "Код", hint = "Латиницею без пробілів, напр. smartphones"): FieldConfig {
  return {
    name: "code",
    label,
    kind: "text",
    required: true,
    maxLength: 100,
    pattern: CODE_PATTERN,
    patternMessage: "Лише латиниця, цифри, «-», «_» або «.»",
    hint,
    placeholder: "code",
  };
}

export function idColumn<T extends { id: number }>(): ColumnDef<T> {
  return {
    accessorKey: "id",
    header: "ID",
    size: 70,
    cell: ({ row }) => (
      <Text size="sm" c="dimmed">
        #{row.original.id}
      </Text>
    ),
  };
}

export function nameColumn<T>(
  header: string,
  getName: (item: T) => string | null | undefined,
  getCode?: (item: T) => string | null | undefined,
): ColumnDef<T> {
  return {
    id: "name",
    header,
    accessorFn: (item) => `${getName(item) ?? ""} ${getCode?.(item) ?? ""}`,
    cell: ({ row }) => (
      <div>
        <Text fw={600} size="sm" c="var(--color-base)">
          {getName(row.original) || getCode?.(row.original) || "—"}
        </Text>
        {getCode && (
          <Text size="xs" c="dimmed" ff="monospace">
            {getCode(row.original)}
          </Text>
        )}
      </div>
    ),
  };
}
