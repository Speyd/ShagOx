import type { ColumnDef } from "@tanstack/react-table";
import type { AdminCodeEntity } from "@/shared/lib/types/admin";
import { CrudPage, codeField, idColumn, nameColumn } from "@/widgets/admin/crud-page";

const columns: ColumnDef<AdminCodeEntity>[] = [
  idColumn(),
  nameColumn("Стан товару", (item) => item.label, (item) => item.code),
];

export default function ConditionsPage() {
  return (
    <CrudPage<AdminCodeEntity>
      title="Стани товару"
      description="Новий, вживаний, відновлений тощо"
      entityName="стан"
      endpoint="/admin/conditions"
      columns={columns}
      searchParam="Code"
      searchPlaceholder="Пошук за кодом..."
      fields={[codeField("Код", "Напр. new, used")]}
      getDefaults={(item) => ({ code: item?.code ?? "" })}
      toPayload={(values) => ({ code: values.code })}
      translations={{
        path: "/admin/conditions/translations",
        codeParam: "ConditionCode",
        codeOf: (values) => String(values.code),
      }}
    />
  );
}
