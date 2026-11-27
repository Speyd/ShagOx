import { useState } from "react";
import { useDebouncedValue } from "@mantine/hooks";
import { Text } from "@mantine/core";
import { Plus } from "lucide-react";
import type { ColumnDef } from "@tanstack/react-table";
import type { BaseEntity } from "@/shared/api/crud";
import { useAdminDelete, useAdminList } from "@/shared/api/crudHooks";
import Button from "@/shared/ui/buttons/button";
import DataTable from "@/shared/ui/data-display/data-table";
import AppPagination from "@/shared/ui/navigation/pagination";
import { AdminPageHeader } from "@/widgets/admin/page-header";
import { ListToolbar } from "@/widgets/admin/list-toolbar";
import { RowActions } from "@/widgets/admin/row-actions";
import type { CrudPageProps } from "../model/types";
import EntityFormModal from "./EntityFormModal";
import styles from "./CrudPage.module.css";

export default function CrudPage<T extends BaseEntity>(props: CrudPageProps<T>) {
  const {
    title,
    description,
    entityName,
    endpoint,
    commandEndpoint = endpoint,
    columns,
    searchParam,
    searchPlaceholder,
    filters,
    toolbarExtra,
    headerActions,
    backTo,
    rowExtra,
    onCreate,
    onEdit,
    createLabel = "Додати",
    canCreate = true,
    canEdit = true,
    canDelete = true,
    pageSize = 15,
    minWidth,
    embedded = false,
  } = props;

  const [page, setPage] = useState(1);
  const [search, setSearch] = useState("");
  const [debouncedSearch] = useDebouncedValue(search.trim(), 350);
  const [editing, setEditing] = useState<T | undefined>();
  const [modalOpened, setModalOpened] = useState(false);

  const { data, isLoading, isFetching } = useAdminList<T>(endpoint, {
    page,
    pageSize,
    filters: {
      ...filters,
      ...(searchParam && { [searchParam]: debouncedSearch }),
    },
  });

  const deleteMutation = useAdminDelete(commandEndpoint, entityName);

  const openCreate = () => {
    if (onCreate) return onCreate();
    setEditing(undefined);
    setModalOpened(true);
  };

  const openEdit = (item: T) => {
    if (onEdit) return onEdit(item);
    setEditing(item);
    setModalOpened(true);
  };

  const tableColumns: ColumnDef<T>[] = [
      ...columns,
      {
        id: "actions",
        header: "",
        size: 160,
        cell: ({ row }) => {
          const item = row.original;
          return (
            <RowActions
              onEdit={canEdit ? () => openEdit(item) : undefined}
              onDelete={canDelete ? () => deleteMutation.mutate(item.id) : undefined}
              deleteTitle={`Видалити ${entityName}`}
              deleteMessage={`Запис #${item.id} буде видалено з адмін-панелі та сайту.`}
            >
              {rowExtra?.(item)}
            </RowActions>
          );
        },
      },
  ];

  const totalPages = data ? Math.max(1, Math.ceil(data.totalCount / pageSize)) : 1;

  return (
    <div className={embedded ? styles.embedded : styles.page}>
      {!embedded && (
        <AdminPageHeader
          title={title}
          description={description}
          backTo={backTo}
          actions={
            <>
              {headerActions}
              {canCreate && (
                <Button leftSection={Plus} iconProps={{ size: 18 }} onClick={openCreate}>
                  {createLabel}
                </Button>
              )}
            </>
          }
        />
      )}

      {embedded && (
        <div className={styles.embeddedHeader}>
          <div>
            <Text fw={700} size="lg" c="var(--color-base)">
              {title}
            </Text>
            {description && (
              <Text size="sm" c="dimmed">
                {description}
              </Text>
            )}
          </div>
          {canCreate && (
            <Button size="sm" leftSection={Plus} iconProps={{ size: 16 }} onClick={openCreate}>
              {createLabel}
            </Button>
          )}
        </div>
      )}

      <ListToolbar
        search={searchParam ? search : undefined}
        onSearchChange={
          searchParam
            ? (value) => {
                setSearch(value);
                setPage(1);
              }
            : undefined
        }
        searchPlaceholder={searchPlaceholder}
      >
        {toolbarExtra}
      </ListToolbar>

      <DataTable
        data={data?.items ?? []}
        columns={tableColumns}
        loading={isLoading || isFetching}
        minWidth={minWidth}
      />

      <div className={styles.footerRow}>
        <Text size="sm" c="dimmed">
          Усього: {data?.totalCount ?? 0}
        </Text>
        <AppPagination page={page} onPageChange={setPage} totalPages={totalPages} />
      </div>

      <EntityFormModal
        {...props}
        opened={modalOpened}
        item={editing}
        onClose={() => setModalOpened(false)}
      />
    </div>
  );
}
