import { useState } from "react";
import { Link, useNavigate } from "react-router-dom";
import {
  Badge,
  Group,
  Image,
  Stack,
  Text,
  TextInput,
} from "@mantine/core";
import { Search } from "lucide-react";
import type { ColumnDef } from "@tanstack/react-table";
import type { Advertisement } from "@/shared/lib/types/advertisements";
import { getAdvertisementPrice } from "@/shared/lib/types/advertisements";
import { useGetAdminAdvertisements } from "@/entities/advertisement/model/hooks/useGetAdminAdvertisements";
import { useAdminDeleteAdvertisement } from "@/features/admin/advertisement/delete-advertisement/model/hooks/useAdminDeleteAdvertisement";
import DataTable from "@/shared/ui/data-display/data-table";
import { AdminPageHeader } from "@/widgets/admin/page-header";
import { RowActions } from "@/widgets/admin/row-actions";
import styles from "./AdvertisementsPage.module.css";
import AppPagination from "@/shared/ui/navigation/pagination";
import { AdvertisementsPageSkeleton } from "./AdvertisementsPageSkeleton";

const PAGE_SIZE = 10;

export default function AdvertisementsPage() {
  const [page, setPage] = useState(1);
  const [search, setSearch] = useState("");
  const navigate = useNavigate();
  const { data, isLoading } = useGetAdminAdvertisements(page, PAGE_SIZE);
  const deleteMutation = useAdminDeleteAdvertisement();

  const columns: ColumnDef<Advertisement>[] = [
    {
      accessorKey: "title",
      header: "Оголошення",
      cell: ({ row: { original: ad } }) => (
        <Group wrap="nowrap">
          <div className={styles.imageContainer}>
            <Image
              src={ad.pictures?.[0]?.url}
              alt={ad.title || "Ad preview"}
              fit="contain"
              w="100%"
              h="100%"
            />
          </div>

          <div
            style={{
              display: "flex",
              flexDirection: "column",
              textDecoration: "none",
              color: "black",
            }}
          >
            <Text
              fw={600}
              component={Link}
              to={`/advertisement/${ad.id}`}
              target="_blank"
              style={{ maxWidth: "200px", color: "inherit" }}
            >
              {ad.title}
            </Text>
            <Text size="sm" c="dimmed" lineClamp={1}>
              {ad.description}
            </Text>
          </div>
        </Group>
      ),
    },
    {
      id: "seller",
      accessorFn: (ad) => ad.seller?.userName,
      header: "Продавець",
      cell: ({ row }) =>
        row.original.seller ? `@${row.original.seller.userName}` : "—",
    },
    {
      id: "price",
      header: "Ціна",
      cell: ({ row }) => (
        <Text fw={700} c="var(--color-primary)">
          {row.original.currency?.symbol}
          {getAdvertisementPrice(row.original)}
        </Text>
      ),
    },
    {
      id: "status",
      header: "Статус",
      cell: ({ row }) => (
        <Badge color={row.original.soldAt ? "red" : "green"} variant="light">
          {row.original.soldAt ? "Продано" : "Активне"}
        </Badge>
      ),
    },
    {
      id: "created",
      header: "Створено",
      cell: ({ row }) =>
        new Date(row.original.createdAt).toLocaleDateString("uk-UA"),
    },
    {
      id: "actions",
      header: "",
      cell: ({ row }) => (
        <RowActions
          deleteTitle="Видалити оголошення"
          onEdit={() =>
            navigate(`/admin/update-advertisement/${row.original.id}`)
          }
          onDelete={() => deleteMutation.mutate(row.original.id)}
        />
      ),
    },
  ];

  return (
    <Stack gap="lg">
      <AdminPageHeader
        title="Оголошення"
        description="Керування оголошеннями маркетплейсу"
      />

      <TextInput
        value={search}
        onChange={(e) => setSearch(e.currentTarget.value)}
        placeholder="Пошук на сторінці..."
        leftSection={<Search size={16} />}
      />

      {isLoading || !data ? (
        <AdvertisementsPageSkeleton />
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
