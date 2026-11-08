import { useState } from "react";
import { Link } from "react-router-dom";
import type { ColumnDef } from "@tanstack/react-table";
import { Avatar, Badge, Rating, SegmentedControl, Select, Text } from "@mantine/core";
import type { AdminReview } from "@/shared/lib/types/admin";
import { formatDate } from "@/shared/lib/formatDate";
import { CrudPage, idColumn } from "@/widgets/admin/crud-page";
import styles from "../AdminPages.module.css";

type Tab = "reviews" | "replies";

const ownerName = (review: AdminReview) =>
  [review.owner?.firstName, review.owner?.lastName].filter(Boolean).join(" ") ||
  `@${review.owner?.userName ?? "user"}`;

function buildColumns(tab: Tab): ColumnDef<AdminReview>[] {
  return [
    idColumn(),
    {
      id: "owner",
      header: "Автор",
      accessorFn: ownerName,
      cell: ({ row }) => (
        <div className={styles.row}>
          <Avatar src={row.original.owner?.avatar?.url} radius="xl" size="sm">
            {row.original.owner?.userName?.[0]?.toUpperCase()}
          </Avatar>
          <Text size="sm" fw={600} truncate>
            {ownerName(row.original)}
          </Text>
        </div>
      ),
    },
    {
      accessorKey: "text",
      header: tab === "reviews" ? "Відгук" : "Відповідь",
      cell: ({ row }) => (
        <div className={styles.cellStack}>
          {tab === "reviews" && (
            <Rating value={row.original.grade ?? 0} readOnly size="xs" />
          )}
          <Text size="sm" lineClamp={3} maw={420}>
            {row.original.text}
          </Text>
          {row.original.pictures?.length > 0 && (
            <Badge size="xs" variant="light" color="gray">
              Фото: {row.original.pictures.length}
            </Badge>
          )}
        </div>
      ),
    },
    {
      id: "target",
      header: tab === "reviews" ? "Оголошення" : "До відгуку",
      cell: ({ row }) =>
        tab === "reviews" ? (
          <Link
            to={`/advertisement/${row.original.commentableEntityId}`}
            target="_blank"
            className={styles.link}
          >
            <Text size="sm">#{row.original.commentableEntityId}</Text>
          </Link>
        ) : (
          <Text size="sm">#{row.original.commentableEntityId}</Text>
        ),
    },
    {
      accessorKey: "createdAt",
      header: "Дата",
      cell: ({ row }) => (
        <Text size="sm" c="dimmed">
          {formatDate(row.original.createdAt)}
        </Text>
      ),
    },
  ];
}

const reviewColumns = buildColumns("reviews");
const replyColumns = buildColumns("replies");

const GRADE_OPTIONS = [5, 4, 3, 2, 1].map((grade) => ({
  value: String(grade),
  label: `${"★".repeat(grade)} (${grade})`,
}));

export default function ReviewsPage() {
  const [tab, setTab] = useState<Tab>("reviews");
  const [grade, setGrade] = useState<string | null>(null);

  const tabs = (
    <SegmentedControl
      value={tab}
      onChange={(value) => setTab(value as Tab)}
      data={[
        { value: "reviews", label: "Відгуки" },
        { value: "replies", label: "Відповіді" },
      ]}
      radius="md"
    />
  );

  return tab === "reviews" ? (
    <CrudPage<AdminReview>
      key="reviews"
      title="Відгуки"
      description="Модерація відгуків покупців: приховуйте або видаляйте порушення"
      entityName="відгук"
      endpoint="/admin/review-comment"
      columns={reviewColumns}
      searchParam="Text"
      searchPlaceholder="Пошук за текстом..."
      filters={{ Grade: grade ?? undefined }}
      toolbarExtra={
        <Select
          data={GRADE_OPTIONS}
          value={grade}
          onChange={setGrade}
          placeholder="Будь-яка оцінка"
          clearable
          radius="md"
          w={190}
        />
      }
      headerActions={tabs}
      fields={[]}
      getDefaults={() => ({})}
      toPayload={() => ({})}
      canCreate={false}
      canEdit={false}
      minWidth={900}
    />
  ) : (
    <CrudPage<AdminReview>
      key="replies"
      title="Відповіді на відгуки"
      description="Відповіді продавців і покупців у гілках відгуків"
      entityName="відповідь"
      endpoint="/admin/review-repli-comment"
      columns={replyColumns}
      searchParam="Text"
      searchPlaceholder="Пошук за текстом..."
      headerActions={tabs}
      fields={[]}
      getDefaults={() => ({})}
      toPayload={() => ({})}
      canCreate={false}
      canEdit={false}
      minWidth={900}
    />
  );
}
