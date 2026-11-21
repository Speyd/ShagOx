import type { ColumnDef } from "@tanstack/react-table";
import { Text } from "@mantine/core";
import type { AdminProductType } from "@/shared/lib/types/admin";
import { CrudPage, codeField, idColumn, nameColumn } from "@/widgets/admin/crud-page";

const columns: ColumnDef<AdminProductType>[] = [
  idColumn(),
  nameColumn("Тип товару", (item) => item.label, (item) => item.name),
  {
    accessorKey: "description",
    header: "Опис",
    cell: ({ row }) => (
      <Text size="sm" c="dimmed" lineClamp={2} maw={360}>
        {row.original.description || "—"}
      </Text>
    ),
  },
];

export default function ProductTypesPage() {
  return (
    <CrudPage<AdminProductType>
      title="Типи товарів"
      description="Верхній рівень каталогу: електроніка, побутова техніка тощо"
      entityName="тип товару"
      endpoint="/admin/product-types"
      columns={columns}
      searchParam="Code"
      searchPlaceholder="Пошук за кодом..."
      fields={[
        codeField("Код", "Латиницею, напр. electronics"),
        { name: "description", label: "Опис", kind: "textarea", maxLength: 500 },
      ]}
      getDefaults={(item) => ({
        code: item?.name ?? "",
        description: item?.description ?? "",
      })}
      toPayload={(values) => ({
        code: values.code,
        description: values.description ?? "",
      })}
      translations={{
        path: "/admin/product-types/translations",
        codeParam: "ProductTypeCode",
        codeOf: (values) => String(values.code),
      }}
    />
  );
}
