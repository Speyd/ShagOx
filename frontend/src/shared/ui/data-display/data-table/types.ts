import type { ColumnDef } from "@tanstack/react-table";

export type DataTableProps<T> = {
  data: T[];
  columns: ColumnDef<T>[];
  search?: string;
  minWidth?: number;
  loading?: boolean;
  emptyTitle?: string;
  emptyDescription?: string;
  getRowId?: (row: T) => string;
  rowClassName?: (row: T) => string | undefined;
};
