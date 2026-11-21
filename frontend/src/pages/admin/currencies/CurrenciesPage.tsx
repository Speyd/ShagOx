import type { ColumnDef } from "@tanstack/react-table";
import { Text } from "@mantine/core";
import type { AdminCurrency } from "@/shared/lib/types/admin";
import { CrudPage, idColumn } from "@/widgets/admin/crud-page";

const columns: ColumnDef<AdminCurrency>[] = [
  idColumn(),
  {
    accessorKey: "code",
    header: "Код",
    cell: ({ row }) => (
      <Text fw={700} size="sm" ff="monospace">
        {row.original.code}
      </Text>
    ),
  },
  { accessorKey: "name", header: "Назва" },
  {
    accessorKey: "symbol",
    header: "Символ",
    cell: ({ row }) => (
      <Text fw={700} size="lg" c="var(--color-primary)">
        {row.original.symbol}
      </Text>
    ),
  },
];

export default function CurrenciesPage() {
  return (
    <CrudPage<AdminCurrency>
      title="Валюти"
      description="Валюти, в яких продавці можуть вказувати ціну"
      entityName="валюту"
      endpoint="/admin/currencies"
      columns={columns}
      searchParam="Code"
      searchPlaceholder="Пошук за кодом..."
      fields={[
        {
          name: "code",
          label: "Код ISO",
          kind: "text",
          required: true,
          maxLength: 3,
          pattern: /^[A-Za-z]{3}$/,
          patternMessage: "Три латинські літери, напр. UAH",
          placeholder: "UAH",
        },
        {
          name: "symbol",
          label: "Символ",
          kind: "text",
          required: true,
          maxLength: 5,
          placeholder: "₴",
        },
        {
          name: "name",
          label: "Назва",
          kind: "text",
          required: true,
          maxLength: 100,
          placeholder: "Українська гривня",
          fullWidth: true,
        },
      ]}
      getDefaults={(item) => ({
        code: item?.code ?? "",
        symbol: item?.symbol ?? "",
        name: item?.name ?? "",
      })}
      toPayload={(values) => ({
        code: String(values.code).toUpperCase(),
        symbol: values.symbol,
        name: values.name,
      })}
    />
  );
}
