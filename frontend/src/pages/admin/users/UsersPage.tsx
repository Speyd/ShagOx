import { useState } from "react";
import { useNavigate } from "react-router-dom";
import type { ColumnDef } from "@tanstack/react-table";
import { Avatar, Badge, Select, Text } from "@mantine/core";
import type { User } from "@/shared/lib/types/user";
import type { BaseEntity } from "@/shared/api/crud";
import { formatDate } from "@/shared/lib/formatDate";
import { CrudPage, idColumn } from "@/widgets/admin/crud-page";
import styles from "../AdminPages.module.css";

type AdminUser = User & BaseEntity;

const SEARCH_FIELDS = [
  { value: "UserName", label: "Нікнейм" },
  { value: "FullName", label: "Ім'я" },
  { value: "Email", label: "Email" },
  { value: "Phone", label: "Телефон" },
];

const fullName = (user: AdminUser) =>
  [user.firstName, user.lastName].filter(Boolean).join(" ");

const columns: ColumnDef<AdminUser>[] = [
  idColumn(),
  {
    id: "user",
    header: "Користувач",
    accessorFn: (user) => `${user.userName} ${fullName(user)}`,
    cell: ({ row: { original: user } }) => (
      <div className={styles.row}>
        <Avatar src={user.avatar?.url} radius="xl" color="blue">
          {user.userName?.[0]?.toUpperCase()}
        </Avatar>
        <div className={styles.grow}>
          <Text fw={600} size="sm" c="var(--color-base)" truncate>
            {fullName(user) || `@${user.userName}`}
          </Text>
          <Text size="xs" c="dimmed" truncate>
            @{user.userName}
          </Text>
        </div>
      </div>
    ),
  },
  {
    id: "contacts",
    header: "Контакти",
    accessorFn: (user) => `${user.email ?? ""} ${user.phone ?? ""}`,
    cell: ({ row }) => (
      <div className={styles.cellStack}>
        <Text size="sm">{row.original.email ?? "—"}</Text>
        <Text size="xs" c="dimmed">
          {row.original.phone ?? ""}
        </Text>
      </div>
    ),
  },
  {
    id: "city",
    header: "Місто",
    accessorFn: (user) => user.city?.lable ?? user.city?.name ?? "",
    cell: ({ row }) => (
      <Text size="sm">
        {row.original.city?.lable || row.original.city?.name || row.original.city?.code || "—"}
      </Text>
    ),
  },
  {
    id: "roles",
    header: "Ролі",
    cell: ({ row }) => (
      <div className={styles.badges}>
        {row.original.roles.map((role) => (
          <Badge
            key={role.id}
            variant="light"
            radius="sm"
            color={role.name === "Admin" ? "red" : "blue"}
          >
            {role.name}
          </Badge>
        ))}
      </div>
    ),
  },
  {
    id: "registered",
    header: "Реєстрація",
    accessorFn: (user) => user.registeredAt,
    cell: ({ row }) => (
      <div className={styles.cellStack}>
        <Text size="sm">{formatDate(row.original.registeredAt)}</Text>
        {row.original.lastSeenAt && (
          <Text size="xs" c="dimmed">
            Був(ла): {formatDate(row.original.lastSeenAt)}
          </Text>
        )}
      </div>
    ),
  },
];

export default function UsersPage() {
  const navigate = useNavigate();
  const [searchField, setSearchField] = useState("UserName");

  return (
    <CrudPage<AdminUser>
      title="Користувачі"
      description="Профілі покупців і продавців. Приховування блокує показ профілю."
      entityName="користувача"
      endpoint="/admin/users"
      columns={columns}
      searchParam={searchField}
      searchPlaceholder="Пошук користувачів..."
      toolbarExtra={
        <Select
          data={SEARCH_FIELDS}
          value={searchField}
          onChange={(value) => value && setSearchField(value)}
          allowDeselect={false}
          radius="md"
          w={150}
          aria-label="Поле пошуку"
        />
      }
      fields={[]}
      getDefaults={() => ({})}
      toPayload={() => ({})}
      canCreate={false}
      onEdit={(user) => navigate(`/admin/users/${user.id}`)}
      minWidth={1000}
    />
  );
}
