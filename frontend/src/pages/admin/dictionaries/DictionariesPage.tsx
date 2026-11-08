import { Link } from "react-router-dom";
import type { ColumnDef } from "@tanstack/react-table";
import { ActionIcon, Badge, Text, Tooltip } from "@mantine/core";
import { ListTree } from "lucide-react";
import type { AdminDictionary } from "@/shared/lib/types/admin";
import { CrudPage, codeField, idColumn } from "@/widgets/admin/crud-page";
import styles from "../AdminPages.module.css";

const columns: ColumnDef<AdminDictionary>[] = [
  idColumn(),
  {
    accessorKey: "code",
    header: "Довідник",
    cell: ({ row }) => (
      <Link to={`/admin/dictionaries/${row.original.id}`} className={styles.link}>
        <Text fw={600} size="sm" ff="monospace">
          {row.original.code}
        </Text>
      </Link>
    ),
  },
  {
    id: "values",
    header: "Значень",
    accessorFn: (item) => item.valueIds?.length ?? 0,
    cell: ({ row }) => (
      <Badge variant="light" radius="sm">
        {row.original.valueIds?.length ?? 0}
      </Badge>
    ),
  },
  {
    id: "attributes",
    header: "Використовують атрибути",
    accessorFn: (item) => item.attributeIds?.length ?? 0,
    cell: ({ row }) => (
      <Badge variant="light" radius="sm" color="grape">
        {row.original.attributeIds?.length ?? 0}
      </Badge>
    ),
  },
];

export default function DictionariesPage() {
  return (
    <CrudPage<AdminDictionary>
      title="Довідники атрибутів"
      description="Списки значень для атрибутів типу «Вибір зі списку»: кольори, бренди, об'єм пам'яті"
      entityName="довідник"
      endpoint="/admin/attribute-dictionaries"
      columns={columns}
      searchParam="Code"
      searchPlaceholder="Пошук за кодом..."
      rowExtra={(item) => (
        <Tooltip label="Значення довідника">
          <ActionIcon
            component={Link}
            to={`/admin/dictionaries/${item.id}`}
            variant="light"
            color="indigo"
            radius="md"
            size="lg"
            aria-label="Значення"
          >
            <ListTree size={16} />
          </ActionIcon>
        </Tooltip>
      )}
      fields={[codeField("Код", "Напр. colors, brands")]}
      getDefaults={(item) => ({ code: item?.code ?? "" })}
      toPayload={(values) => ({ code: values.code })}
      modalSize="md"
    />
  );
}
