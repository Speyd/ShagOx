import { useGetUsers } from "@/entities/user/model/hooks/useGetUsers";
import { useState } from "react";
import { useNavigate } from "react-router-dom";
import {
  ActionIcon,
  Avatar,
  Badge,
  Group,
  Pagination,
  Stack,
  Text,
  TextInput,
  Title,
} from "@mantine/core";
import { Pencil, Search, Trash2 } from "lucide-react";
import type { ColumnDef } from "@tanstack/react-table";
import type { User } from "@/shared/lib/types/user";
import DataTable from "@/shared/ui/data-table";

export default function UsersPage() {
  const [page, setPage] = useState(1);

  const pageSize = 10;

  const { data, isLoading } = useGetUsers(page, pageSize);

  const [search, setSearch] = useState("");
  const navigate = useNavigate();

  if (isLoading) return <Text>Loading...</Text>;
  if (!data) return <Text>No data</Text>;

  const handleDelete = (id: number) => {
    console.log(id);
  };

  const columns: ColumnDef<User>[] = [
    {
      id: "avatar",
      header: "Avatar",
      cell: ({ row }) => (
        <Avatar src={row.original.avatar} radius="md" size={56}>
          {row.original.name[0]}
        </Avatar>
      ),
    },
    {
      accessorKey: "name",
      header: "User",
      cell: ({ row }) => (
        <Stack gap={2}>
          <Text fw={600}>
            {row.original.name}
            {row.original.surname && ` ${row.original.surname}`}
          </Text>

          <Text size="sm" c="dimmed">
            {row.original.phone}
          </Text>
        </Stack>
      ),
    },
    {
      accessorKey: "email",
      header: "Email",
      cell: ({ row }) => <Text>{row.original.email ?? "-"}</Text>,
    },
    {
      id: "city",
      header: "City",
      cell: ({ row }) => (
        <Stack gap={0}>
          <Text>{row.original.city.name}</Text>
          <Text size="xs" c="dimmed">
            {row.original.city.region.name}
          </Text>
        </Stack>
      ),
    },
    {
      id: "role",
      header: "Role",
      cell: ({ row }) => (
        <Group gap={4}>
          {row.original.roles.map((role) => (
            <Badge key={role.id} variant="light">
              {role.name}
            </Badge>
          ))}
        </Group>
      ),
    },
    {
      id: "lastSeen",
      header: "Last seen",
      cell: ({ row }) => (
        <Text>{new Date(row.original.lastSeenAt).toLocaleString("uk-UA")}</Text>
      ),
    },
    {
      id: "registered",
      header: "Registered",
      cell: ({ row }) => (
        <Text>
          {new Date(row.original.registeredAt).toLocaleDateString("uk-UA")}
        </Text>
      ),
    },
    {
      id: "actions",
      header: "Actions",
      cell: ({ row }) => (
        <Group gap="xs" wrap="nowrap">
          <ActionIcon
            variant="light"
            color="blue"
            radius="md"
            size="lg"
            onClick={() => navigate(`/admin/update-user/${row.original.id}`)}
          >
            <Pencil size={16} />
          </ActionIcon>

          <ActionIcon
            variant="light"
            color="red"
            radius="md"
            size="lg"
            onClick={() => handleDelete(row.original.id)}
          >
            <Trash2 size={16} />
          </ActionIcon>
        </Group>
      ),
    },
  ];

  return (
    <Stack gap="lg">
      <Group justify="space-between" align="center">
        <div>
          <Title order={2}>Users</Title>
          <Text c="dimmed">Manage users</Text>
        </div>
      </Group>

      <TextInput
        value={search}
        onChange={(e) => setSearch(e.currentTarget.value)}
        placeholder="Search..."
        leftSection={<Search size={16} />}
      />

      {isLoading ? (
        <Text>Loading...</Text>
      ) : (
        <>
          <DataTable data={data.items} columns={columns} search={search} />

          <Group justify="center">
            <Pagination
              value={page}
              onChange={setPage}
              total={data.totalPages}
            />
          </Group>
        </>
      )}
    </Stack>
  );
}
