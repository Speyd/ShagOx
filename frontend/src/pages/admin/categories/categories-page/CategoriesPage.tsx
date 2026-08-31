import { useGetAdminCategories } from "@/entities/category/model/useGetAdminCategories";
import type { ColumnDef } from "@tanstack/react-table";
import {
  ActionIcon,
  Group,
  Pagination,
  Stack,
  Text,
  TextInput,
  Title,
} from "@mantine/core";
import DataTable from "@/shared/ui/data-table";
import { useState } from "react";
import { Pencil, Search, Trash2 } from "lucide-react";
import type { Category } from "@/shared/lib/types/category";
import styles from "./CategoriesPage.module.css";
import { useNavigate } from "react-router-dom";
import { useDeleteCategory } from "@/features/admin/category/delete-category/model/hooks/useDeleteCategory";
import { modals } from "@mantine/modals";

export default function CategoriesPage() {
  const [page, setPage] = useState(1);
  const [search, setSearch] = useState("");
  const pageSize = 10;
  const navigate = useNavigate();
  const { data, isLoading } = useGetAdminCategories(page, pageSize);
  const deleteMutation = useDeleteCategory();

  if (isLoading) {
    return <div>Loading...</div>;
  }

  if (!data) return <div>No data</div>;

  const handleDelete = (id: number) => {
    modals.openConfirmModal({
      title: "Delete category",
      centered: true,

      children: (
        <Text size="sm">
          Are you sure you want to delete this category?
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

  const columns: ColumnDef<Category>[] = [
    {
      accessorKey: "name",
      header: "Name",
      cell: ({ row }) => <Text fw={600}>{row.original.name}</Text>,
    },

    {
      accessorKey: "productType.name",
      header: "Product Type",
      cell: ({ row }) => (
        <Stack gap={2}>
          <Text fw={600}>{row.original.productType.name}</Text>
        </Stack>
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
              navigate(`/admin/update-category/${row.original.id}`)
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
    <Stack gap="lg" className={styles.categoriesPage}>
      <Group justify="space-between" align="center">
        <div>
          <Title order={2}>Categories</Title>
          <Text c="dimmed">Manage marketplace categories</Text>
        </div>
      </Group>

      {isLoading ? (
        <Text>Loading...</Text>
      ) : (
        <>
          <TextInput
            value={search}
            onChange={(e) => setSearch(e.currentTarget.value)}
            placeholder="Search..."
            leftSection={<Search size={16} />}
          />

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
