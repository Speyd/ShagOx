import type { ColumnDef } from "@tanstack/react-table";
import type { AdminCodeEntity } from "@/shared/lib/types/admin";
import { CrudPage, codeField, idColumn, nameColumn } from "@/widgets/admin/crud-page";

const columns: ColumnDef<AdminCodeEntity>[] = [
  idColumn(),
  nameColumn("Статус оголошення", (item) => item.label, (item) => item.code),
];

export default function StatusesPage() {
  return (
    <CrudPage<AdminCodeEntity>
      title="Статуси оголошень"
      description="Життєвий цикл оголошення: опубліковано, чернетка, архів"
      entityName="статус"
      endpoint="/admin/statuses"
      columns={columns}
      searchParam="Code"
      searchPlaceholder="Пошук за кодом..."
      fields={[codeField("Код", "Напр. published, draft")]}
      getDefaults={(item) => ({ code: item?.code ?? "" })}
      toPayload={(values) => ({ code: values.code })}
      translations={{
        path: "/admin/statuses/translations",
        codeParam: "StatusCode",
        withDescription: true,
        codeOf: (values) => String(values.code),
      }}
    />
  );
}
