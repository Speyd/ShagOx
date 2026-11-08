import { useState } from "react";
import { useDebouncedValue } from "@mantine/hooks";
import { Text } from "@mantine/core";
import { Plus } from "lucide-react";
import type { ColumnDef } from "@tanstack/react-table";
import type { BaseEntity, EntityStatus } from "@/shared/api/crud";
import {
  useAdminDelete,
  useAdminList,
  useAdminVisibility,
} from "@/shared/api/crudHooks";
import Button from "@/shared/ui/buttons/button";
import DataTable from "@/shared/ui/data-display/data-table";
import AppPagination from "@/shared/ui/navigation/pagination";
import { AdminPageHeader } from "@/widgets/admin/page-header";
import { ListToolbar } from "@/widgets/admin/list-toolbar";
import { RowActions } from "@/widgets/admin/row-actions";
import { StatusBadge } from "@/widgets/admin/status-badge";
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
    canHide = true,
    pageSize = 15,
    minWidth,
    embedded = false,
  } = props;

  const [page, setPage] = useState(1);
  const [search, setSearch] = useState("");
  const [debouncedSearch] = useDebouncedValue(search.trim(), 350);
  const [status, setStatus] = useState<EntityStatus>("ActiveAndHidden");
  const [editing, setEditing] = useState<T | undefined>();
  const [modalOpened, setModalOpened] = useState(false);

  const { data, isLoading, isFetching } = useAdminList<T>(endpoint, {
    page,
    pageSize,
    status,
    filters: {
      ...filters,
      ...(searchParam && { [searchParam]: debouncedSearch }),
    },
  });

  const deleteMutation = useAdminDelete(commandEndpoint, entityName);
  const visibilityMutation = useAdminVisibility(commandEndpoint);

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
        id: "entityStatus",
        header: "Видимість",
        size: 120,
        cell: ({ row }) => <StatusBadge status={row.original.entityStatus} />,
      },
      {
        id: "actions",
        header: "",
        size: 160,
        cell: ({ row }) => {
          const item = row.original;
          const hidden = item.entityStatus === "Hidden";
          return (
            <RowActions
              onEdit={canEdit ? () => openEdit(item) : undefined}
              onToggleVisibility={
                canHide
                  ? () => visibilityMutation.mutate({ id: item.id, hidden })
                  : undefined
              }
              onDelete={canDelete ? () => deleteMutation.mutate(item.id) : undefined}
              deleteTitle={`Видалити ${entityName}`}
              deleteMessage={`Запис #${item.id} буде видалено назавжди. Пов'язані дані можуть стати недоступними.`}
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
                <Button leftIcon={<Plus size={18} />} onClick={openCreate}>
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
            <Button size="sm" leftIcon={<Plus size={16} />} onClick={openCreate}>
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
        status={status}
        onStatusChange={(value) => {
          setStatus(value);
          setPage(1);
        }}
      >
        {toolbarExtra}
      </ListToolbar>

      <DataTable
        data={data?.items ?? []}
        columns={tableColumns}
        loading={isLoading || isFetching}
        minWidth={minWidth}
        rowClassName={(item) =>
          item.entityStatus === "Hidden" ? styles.hiddenRow : undefined
        }
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
