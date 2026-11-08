import { useMemo, useState } from "react";
import { Link, useNavigate } from "react-router-dom";
import type { ColumnDef } from "@tanstack/react-table";
import { ActionIcon, Badge, Select, Text, Tooltip } from "@mantine/core";
import { ExternalLink } from "lucide-react";
import type { BaseEntity } from "@/shared/api/crud";
import { useAdminOptions } from "@/shared/api/crudHooks";
import { formatDate } from "@/shared/lib/formatDate";
import type { AdminCategory } from "@/shared/lib/types/admin";
import type { Advertisement } from "@/shared/lib/types/advertisements";
import { CrudPage, idColumn } from "@/widgets/admin/crud-page";
import styles from "../AdminPages.module.css";

type AdminAd = Advertisement & BaseEntity;

const priceRange = (ad: AdminAd) => {
  const prices = (ad.variants ?? []).map((v) => v.price);
  if (prices.length === 0) return "—";
  const min = Math.min(...prices);
  const max = Math.max(...prices);
  const format = (value: number) => value.toLocaleString("uk-UA");
  const symbol = ad.currency?.symbol ?? "";
  return min === max ? `${format(min)} ${symbol}` : `${format(min)} – ${format(max)} ${symbol}`;
};

const totalStock = (ad: AdminAd) =>
  (ad.variants ?? []).reduce((sum, variant) => sum + (variant.stock ?? 0), 0);

const columns: ColumnDef<AdminAd>[] = [
  idColumn(),
  {
    id: "title",
    header: "Оголошення",
    accessorFn: (ad) => ad.title,
    cell: ({ row: { original: ad } }) => (
      <div className={styles.row}>
        <span className={styles.thumb}>
          {ad.pictures?.[0]?.url && <img src={ad.pictures[0].url} alt="" />}
        </span>
        <div className={styles.grow}>
          <Link to={`/admin/advertisements/${ad.id}`} className={styles.link}>
            <Text fw={600} size="sm" lineClamp={1} maw={320}>
              {ad.title}
            </Text>
          </Link>
          <Text size="xs" c="dimmed">
            {ad.category?.lable ?? ""} · {formatDate(ad.createdAt)}
          </Text>
        </div>
      </div>
    ),
  },
  {
    id: "seller",
    header: "Продавець",
    accessorFn: (ad) => ad.seller?.userName ?? "",
    cell: ({ row }) =>
      row.original.seller ? (
        <Link to={`/admin/users/${row.original.seller.id}`} className={styles.link}>
          <Text size="sm">@{row.original.seller.userName}</Text>
        </Link>
      ) : (
        "—"
      ),
  },
  {
    id: "price",
    header: "Ціна",
    accessorFn: (ad) => ad.variants?.[0]?.price ?? 0,
    cell: ({ row }) => (
      <Text fw={700} size="sm" c="var(--color-primary)" style={{ whiteSpace: "nowrap" }}>
        {priceRange(row.original)}
      </Text>
    ),
  },
  {
    id: "stock",
    header: "Склад",
    accessorFn: totalStock,
    cell: ({ row }) => {
      const stock = totalStock(row.original);
      return (
        <div className={styles.cellStack}>
          <Text size="sm" fw={600} c={stock === 0 ? "red.7" : undefined}>
            {stock} шт.
          </Text>
          <Text size="xs" c="dimmed">
            Варіантів: {row.original.variants?.length ?? 0}
          </Text>
        </div>
      );
    },
  },
  {
    id: "status",
    header: "Статус",
    cell: ({ row }) => (
      <div className={styles.badges}>
        {row.original.soldAt ? (
          <Badge color="orange" variant="light" radius="sm">
            Продано
          </Badge>
        ) : (
          <Badge color="blue" variant="light" radius="sm">
            {row.original.status?.lable || row.original.status?.code || "—"}
          </Badge>
        )}
      </div>
    ),
  },
];

export default function AdvertisementsPage() {
  const navigate = useNavigate();
  const [categoryId, setCategoryId] = useState<string | null>(null);
  const { data: categories = [] } = useAdminOptions<AdminCategory>("/admin/categories");

  const categoryOptions = useMemo(
    () => categories.map((c) => ({ value: String(c.id), label: c.label || c.code })),
    [categories],
  );

  return (
    <CrudPage<AdminAd>
      title="Оголошення"
      description="Усі товари маркетплейсу: повне редагування, варіанти, фото та модерація"
      entityName="оголошення"
      endpoint="/admin/advertisements"
      columns={columns}
      searchParam="Title"
      searchPlaceholder="Пошук за назвою..."
      filters={{ CategoryId: categoryId ?? undefined }}
      toolbarExtra={
        <Select
          data={categoryOptions}
          value={categoryId}
          onChange={setCategoryId}
          placeholder="Усі категорії"
          clearable
          searchable
          radius="md"
          w={220}
        />
      }
      rowExtra={(ad) => (
        <Tooltip label="Відкрити на сайті">
          <ActionIcon
            component={Link}
            to={`/advertisement/${ad.id}`}
            target="_blank"
            variant="light"
            color="gray"
            radius="md"
            size="lg"
            aria-label="Відкрити на сайті"
          >
            <ExternalLink size={16} />
          </ActionIcon>
        </Tooltip>
      )}
      fields={[]}
      getDefaults={() => ({})}
      toPayload={() => ({})}
      createLabel="Створити оголошення"
      onCreate={() => navigate("/admin/advertisements/new")}
      onEdit={(ad) => navigate(`/admin/advertisements/${ad.id}`)}
      minWidth={1100}
    />
  );
}
