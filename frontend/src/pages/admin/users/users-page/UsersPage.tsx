import { useState } from "react";
import { useNavigate } from "react-router-dom";
import {
  Avatar,
  Badge,
  Group,
  Stack,
  Text,
  TextInput,
} from "@mantine/core";
import { Search } from "lucide-react";
import type { ColumnDef } from "@tanstack/react-table";
import type { User } from "@/shared/lib/types/user";
import { useGetUsers } from "@/entities/user/model/hooks/useGetUsers";
import { useAdminDeleteUser } from "@/features/admin/user/delete-user";
import DataTable from "@/shared/ui/data-display/data-table";
import { AdminPageHeader } from "@/widgets/admin/page-header";
import { RowActions } from "@/widgets/admin/row-actions";
import AppPagination from "@/shared/ui/navigation/pagination";
import { UsersPageSkeleton } from "./UsersPageSkeleton";

const PAGE_SIZE = 10;

export function UsersPage() {
  const [page, setPage] = useState(1);
  const [search, setSearch] = useState("");
  const navigate = useNavigate();
  const { data, isLoading } = useGetUsers(page, PAGE_SIZE);
  const deleteMutation = useAdminDeleteUser();

  const columns: ColumnDef<User>[] = [
    {
      id: "user",
      accessorFn: (user) =>
        `${user.userName} ${user.firstName ?? ""} ${user.lastName ?? ""}`,
      header: "Користувач",
      cell: ({ row: { original: user } }) => (
        <Group wrap="nowrap">
          <Avatar src={user.avatar?.url} radius="xl">
            {user.userName[0]?.toUpperCase()}
          </Avatar>
          <div>
            <Text fw={600}>
              {[user.firstName, user.lastName].filter(Boolean).join(" ") ||
                `@${user.userName}`}
            </Text>
            <Text size="sm" c="dimmed">
              {user.phone}
            </Text>
          </div>
        </Group>
      ),
    },
    {
      accessorKey: "email",
      header: "Email",
      cell: ({ row }) => row.original.email ?? "—",
    },
    {
      id: "city",
      header: "Місто",
      cell: ({ row }) => row.original.city?.name ?? "—",
    },
    {
      id: "roles",
      header: "Роль",
      cell: ({ row }) => (
        <Group gap={4}>
          {row.original.roles.map((role) => (
            <Badge
              key={role.id}
              variant="light"
              color={role.name === "Admin" ? "red" : "blue"}
            >
              {role.name}
            </Badge>
          ))}
        </Group>
      ),
    },
    {
      id: "registered",
      header: "Реєстрація",
      cell: ({ row }) =>
        new Date(row.original.registeredAt).toLocaleDateString("uk-UA"),
    },
    {
      id: "actions",
      header: "",
      cell: ({ row }) => (
        <RowActions
          deleteTitle="Видалити користувача"
          onEdit={() => navigate(`/admin/update-user/${row.original.id}`)}
          onDelete={() => deleteMutation.mutate(row.original.id)}
        />
      ),
    },
  ];

  return (
    <Stack gap="lg">
      <AdminPageHeader
        title="Користувачі"
        description="Керування користувачами маркетплейсу"
      />

      <TextInput
        value={search}
        onChange={(e) => setSearch(e.currentTarget.value)}
        placeholder="Пошук на сторінці..."
        leftSection={<Search size={16} />}
      />

      {isLoading || !data ? (
        <UsersPageSkeleton />
      ) : (
        <>
          <DataTable data={data.items} columns={columns} search={search} />
          <AppPagination
            page={page}
            onPageChange={setPage}
            totalPages={Math.ceil(data.totalCount / PAGE_SIZE)}
          />
        </>
      )}
    </Stack>
  );
}
