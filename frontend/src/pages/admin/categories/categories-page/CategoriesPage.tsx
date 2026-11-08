import { useState } from "react";
import { useNavigate } from "react-router-dom";
import { Badge, Stack, Text, TextInput } from "@mantine/core";
import { Search } from "lucide-react";
import type { ColumnDef } from "@tanstack/react-table";
import type { Category } from "@/shared/lib/types/category";
import { useGetAdminCategories } from "@/entities/category/model/useGetAdminCategories";
import { useDeleteCategory } from "@/features/admin/category/delete-category/model/hooks/useDeleteCategory";
import DataTable from "@/shared/ui/data-display/data-table";
import { AdminPageHeader } from "@/widgets/admin/page-header";
import { RowActions } from "@/widgets/admin/row-actions";
import AppPagination from "@/shared/ui/navigation/pagination";
import { CategoriesPageSkeleton } from "./CategoriesPageSkeleton";

const PAGE_SIZE = 10;

export function CategoriesPage() {
  const [page, setPage] = useState(1);
  const [search, setSearch] = useState("");
  const navigate = useNavigate();
  const { data, isLoading } = useGetAdminCategories(page, PAGE_SIZE);
  const deleteMutation = useDeleteCategory();

  const columns: ColumnDef<Category>[] = [
    {
      accessorKey: "id",
      header: "ID",
      cell: ({ row }) => <Text c="dimmed">#{row.original.id}</Text>,
    },
    {
      accessorKey: "lable",
      header: "Назва",
      cell: ({ row }) => <Text fw={600}>{row.original.label}</Text>,
    },
    {
      id: "productType",
      accessorFn: (category) => category.productType.label,
      header: "Тип товару",
      cell: ({ row }) => <Badge variant="light">{row.original.label}</Badge>,
    },
    {
      id: "actions",
      header: "",
      cell: ({ row }) => (
        <RowActions
          deleteTitle="Видалити категорію"
          onEdit={() => navigate(`/admin/update-category/${row.original.id}`)}
          onDelete={() => deleteMutation.mutate(row.original.id)}
        />
      ),
    },
  ];

  return (
    <Stack gap="lg">
      <AdminPageHeader
        title="Категорії"
        description="Керування категоріями товарів"
      />

      <TextInput
        value={search}
        onChange={(e) => setSearch(e.currentTarget.value)}
        placeholder="Пошук на сторінці..."
        leftSection={<Search size={16} />}
      />

      {isLoading || !data ? (
        <CategoriesPageSkeleton />
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
