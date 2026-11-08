import { useMemo, useState } from "react";
import type { ColumnDef } from "@tanstack/react-table";
import { Badge, Select } from "@mantine/core";
import type { AdminCity, AdminRegion } from "@/shared/lib/types/admin";
import { useAdminOptions } from "@/shared/api/crudHooks";
import { CrudPage, codeField, idColumn, nameColumn } from "@/widgets/admin/crud-page";

const columns: ColumnDef<AdminCity>[] = [
  idColumn(),
  nameColumn("Місто", (item) => item.label, (item) => item.code),
  {
    id: "region",
    header: "Регіон",
    accessorFn: (item) => item.region?.label ?? item.region?.code ?? "",
    cell: ({ row }) =>
      row.original.region ? (
        <Badge variant="light" radius="sm">
          {row.original.region.label || row.original.region.code}
        </Badge>
      ) : (
        "—"
      ),
  },
];

export default function CitiesPage() {
  const [regionId, setRegionId] = useState<string | null>(null);
  const { data: regions = [] } = useAdminOptions<AdminRegion>("/admin/regions");

  const regionOptions = useMemo(
    () =>
      regions.map((region) => ({
        value: String(region.id),
        label: region.label || region.code,
      })),
    [regions],
  );

  return (
    <CrudPage<AdminCity>
      title="Міста"
      description="Міста, які користувачі обирають у профілі"
      entityName="місто"
      endpoint="/admin/cities"
      columns={columns}
      searchParam="Code"
      searchPlaceholder="Пошук за кодом..."
      filters={{ RegionId: regionId ?? undefined }}
      toolbarExtra={
        <Select
          data={regionOptions}
          value={regionId}
          onChange={setRegionId}
          placeholder="Усі регіони"
          clearable
          searchable
          radius="md"
          w={220}
        />
      }
      fields={[
        codeField("Код", "Напр. kyiv"),
        {
          name: "regionId",
          label: "Регіон",
          kind: "select",
          required: true,
          options: regionOptions,
        },
      ]}
      getDefaults={(item) => ({
        code: item?.code ?? "",
        regionId: item?.region?.id ? String(item.region.id) : regionId,
      })}
      toPayload={(values) => ({
        code: values.code,
        regionId: Number(values.regionId),
      })}
      translations={{
        path: "/admin/cities/translations",
        codeParam: "CityCode",
        codeOf: (values) => String(values.code),
      }}
    />
  );
}
