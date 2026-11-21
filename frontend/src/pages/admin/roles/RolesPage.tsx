import type { ColumnDef } from "@tanstack/react-table";
import { Badge, Text } from "@mantine/core";
import type { AdminRole } from "@/shared/lib/types/admin";
import { CrudPage, idColumn } from "@/widgets/admin/crud-page";

const columns: ColumnDef<AdminRole>[] = [
  idColumn(),
  {
    accessorKey: "name",
    header: "Роль",
    cell: ({ row }) => (
      <Badge
        variant="light"
        color={row.original.name === "Admin" ? "red" : "blue"}
        radius="sm"
      >
        {row.original.name}
      </Badge>
    ),
  },
  {
    accessorKey: "description",
    header: "Опис",
    cell: ({ row }) => (
      <Text size="sm" c="dimmed">
        {row.original.description || "—"}
      </Text>
    ),
  },
];

export default function RolesPage() {
  return (
    <CrudPage<AdminRole>
      title="Ролі"
      description="Ролі визначають рівень доступу користувачів"
      entityName="роль"
      endpoint="/admin/roles"
      columns={columns}
      searchParam="Name"
      searchPlaceholder="Пошук за назвою..."
      fields={[
        {
          name: "name",
          label: "Назва",
          kind: "text",
          required: true,
          maxLength: 50,
          placeholder: "Moderator",
        },
        {
          name: "description",
          label: "Опис",
          kind: "textarea",
          required: true,
          maxLength: 300,
        },
      ]}
      getDefaults={(item) => ({
        name: item?.name ?? "",
        description: item?.description ?? "",
      })}
      toPayload={(values) => ({ name: values.name, description: values.description })}
    />
  );
}
