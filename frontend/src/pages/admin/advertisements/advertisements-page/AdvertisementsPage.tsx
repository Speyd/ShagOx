import type { Advertisement } from "@/shared/lib/types/advertisements";
import DataTable from "@/shared/ui/data-table";
import type { ColumnDef } from "@tanstack/react-table";
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
import { useState } from "react";
import { useNavigate } from "react-router-dom";
import { modals } from "@mantine/modals";
import { useAdminDeleteAdvertisement } from "@/features/admin/advertisement/delete-advertisement/model/hooks/useAdminDeleteAdvertisement";
import { useGetAdminAdvertisements } from "@/entities/advertisement/model/hooks/useGetAdminAdvertisements";

export default function AdvertisementsPage() {
  const [page, setPage] = useState(1);

  const pageSize = 10;

  const { data, isLoading } = useGetAdminAdvertisements(page, pageSize);

  const [search, setSearch] = useState("");
  const navigate = useNavigate();
  const deleteMutation = useAdminDeleteAdvertisement();

  if (!data) return <div>No data</div>;

  const handleDelete = (id: number) => {
    modals.openConfirmModal({
      title: "Delete advertisement",
      centered: true,

      children: (
        <Text size="sm">
          Are you sure you want to delete this advertisement?
          <br />
          This action cannot be undone.
        </Text>
      ),

      labels: {
        confirm: "Delete",
        cancel: "Cancel",
      },

      confirmProps: {
        color: "red",
      },

      onConfirm: () => {
        deleteMutation.mutate(id);
      },
    });
  };

  const columns: ColumnDef<Advertisement>[] = [
    {
      id: "image",
      header: "Image",
      cell: ({ row }) => (
        <Avatar src={row.original.images?.[0]?.url} radius="md" size={56} />
      ),
    },

    {
      accessorKey: "title",
      header: "Title",
      cell: ({ row }) => (
        <Stack gap={2}>
          <Text fw={600}>{row.original.title}</Text>

          <Text size="sm" c="dimmed" lineClamp={1}>
            {row.original.description}
          </Text>
        </Stack>
      ),
    },

    {
      id: "seller",
      header: "Seller",
      cell: ({ row }) => (
        <Text fw={500}>{row.original.seller?.name ?? "Unknown seller"}</Text>
      ),
    },

    {
      accessorKey: "price",
      header: "Price",
      cell: ({ row }) => (
        <Text fw={700} c="blue">
          {row.original.currency?.symbol ?? ""}
          {row.original.price}
        </Text>
      ),
    },

    {
      id: "status",
      header: "Status",
      cell: ({ row }) =>
        row.original.soldAt ? (
          <Badge color="red" variant="light">
            Sold
          </Badge>
        ) : (
          <Badge color="green" variant="light">
            Active
          </Badge>
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
            onClick={() =>
              navigate(`/admin/update-advertisement/${row.original.id}`)
            }
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
          <Title order={2}>Advertisements</Title>
          <Text c="dimmed">Manage marketplace advertisements</Text>
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
