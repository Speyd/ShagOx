import type { ColumnDef } from "@tanstack/react-table";
import type { AdminRegion } from "@/shared/lib/types/admin";
import { CrudPage, codeField, idColumn, nameColumn } from "@/widgets/admin/crud-page";

const columns: ColumnDef<AdminRegion>[] = [
  idColumn(),
  nameColumn("Регіон", (item) => item.label, (item) => item.code),
];

export default function RegionsPage() {
  return (
    <CrudPage<AdminRegion>
      title="Регіони"
      description="Області, до яких належать міста"
      entityName="регіон"
      endpoint="/admin/regions"
      columns={columns}
      searchParam="Code"
      searchPlaceholder="Пошук за кодом..."
      fields={[codeField("Код", "Напр. kyiv-oblast")]}
      getDefaults={(item) => ({ code: item?.code ?? "" })}
      toPayload={(values) => ({ code: values.code })}
      translations={{
        path: "/admin/regions/translations",
        codeParam: "RegionCode",
        codeOf: (values) => String(values.code),
      }}
    />
  );
}
