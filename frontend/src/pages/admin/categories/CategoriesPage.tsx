import { useMemo, useState } from "react";
import { Link } from "react-router-dom";
import type { ColumnDef } from "@tanstack/react-table";
import { ActionIcon, Badge, Select, Tooltip } from "@mantine/core";
import { SlidersHorizontal } from "lucide-react";
import type { AdminCategory, AdminProductType } from "@/shared/lib/types/admin";
import { useAdminOptions } from "@/shared/api/crudHooks";
import { CrudPage, codeField, idColumn, nameColumn } from "@/widgets/admin/crud-page";

export default function CategoriesPage() {
  const [productTypeId, setProductTypeId] = useState<string | null>(null);
  const { data: productTypes = [] } = useAdminOptions<AdminProductType>(
    "/admin/product-types",
  );

  const productTypeOptions = useMemo(
    () =>
      productTypes.map((type) => ({
        value: String(type.id),
        label: type.label || type.name,
      })),
    [productTypes],
  );

  const columns = useMemo<ColumnDef<AdminCategory>[]>(() => {
    const typeLabels = new Map(productTypeOptions.map((o) => [o.value, o.label]));
    return [
      idColumn(),
      nameColumn("Категорія", (item) => item.label, (item) => item.code),
      {
        id: "productType",
        header: "Тип товару",
        accessorFn: (item) => typeLabels.get(String(item.productTypeId)) ?? "",
        cell: ({ row }) => (
          <Badge variant="light" radius="sm">
            {typeLabels.get(String(row.original.productTypeId)) ??
              `#${row.original.productTypeId}`}
          </Badge>
        ),
      },
    ];
  }, [productTypeOptions]);

  return (
    <CrudPage<AdminCategory>
      title="Категорії"
      description="Категорії товарів та їхні характеристики"
      entityName="категорію"
      endpoint="/admin/categories"
      columns={columns}
      searchParam="Code"
      searchPlaceholder="Пошук за кодом..."
      filters={{ ProductTypeId: productTypeId ?? undefined }}
      toolbarExtra={
        <Select
          data={productTypeOptions}
          value={productTypeId}
          onChange={setProductTypeId}
          placeholder="Усі типи товарів"
          clearable
          searchable
          radius="md"
          w={220}
        />
      }
      rowExtra={(item) => (
        <Tooltip label="Атрибути та деталі">
          <ActionIcon
            component={Link}
            to={`/admin/categories/${item.id}`}
            variant="light"
            color="indigo"
            radius="md"
            size="lg"
            aria-label="Атрибути"
          >
            <SlidersHorizontal size={16} />
          </ActionIcon>
        </Tooltip>
      )}
      fields={[
        codeField("Код", "Латиницею, напр. smartphones"),
        {
          name: "productTypeId",
          label: "Тип товару",
          kind: "select",
          required: true,
          options: productTypeOptions,
        },
      ]}
      getDefaults={(item) => ({
        code: item?.code ?? "",
        productTypeId: item ? String(item.productTypeId) : productTypeId,
      })}
      toPayload={(values) => ({
        code: values.code,
        productTypeId: Number(values.productTypeId),
      })}
      translations={{
        path: "/admin/categories/translations",
        codeParam: "CategoryCode",
        codeOf: (values) => String(values.code),
      }}
    />
  );
}
