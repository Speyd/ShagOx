import { useState } from "react";
import {
  flexRender,
  getCoreRowModel,
  getFilteredRowModel,
  getSortedRowModel,
  useReactTable,
  type SortingState,
} from "@tanstack/react-table";
import { Loader, ScrollArea, Table, Text } from "@mantine/core";
import { ArrowDown, ArrowUp, Database } from "lucide-react";
import clsx from "clsx";
import type { DataTableProps } from "./types";
import styles from "./DataTable.module.css";

export default function DataTable<T>({
  data,
  columns,
  search,
  minWidth = 800,
  loading = false,
  emptyTitle = "Даних поки немає",
  emptyDescription = "Тут з'являться записи, щойно їх буде створено.",
  getRowId,
  rowClassName,
}: DataTableProps<T>) {
  const [sorting, setSorting] = useState<SortingState>([]);

  const table = useReactTable({
    data,
    columns,
    state: { sorting, globalFilter: search },
    onSortingChange: setSorting,
    getRowId,
    getCoreRowModel: getCoreRowModel(),
    getSortedRowModel: getSortedRowModel(),
    getFilteredRowModel: getFilteredRowModel(),
  });

  const rows = table.getRowModel().rows;

  return (
    <div className={styles.wrapper}>
      {loading && (
        <div className={styles.overlay}>
          <Loader size="sm" />
        </div>
      )}

      <ScrollArea>
        <Table
          highlightOnHover
          verticalSpacing="sm"
          horizontalSpacing="md"
          miw={minWidth}
          className={styles.table}
        >
          <Table.Thead>
            {table.getHeaderGroups().map((headerGroup) => (
              <Table.Tr key={headerGroup.id}>
                {headerGroup.headers.map((header) => {
                  const sorted = header.column.getIsSorted();
                  const canSort = header.column.getCanSort();
                  return (
                    <Table.Th
                      key={header.id}
                      className={clsx(styles.th, canSort && styles.sortable)}
                      onClick={header.column.getToggleSortingHandler()}
                      style={{ width: header.column.columnDef.size }}
                    >
                      <span className={styles.thContent}>
                        {header.isPlaceholder
                          ? null
                          : flexRender(
                              header.column.columnDef.header,
                              header.getContext(),
                            )}
                        {sorted === "asc" && <ArrowUp size={12} />}
                        {sorted === "desc" && <ArrowDown size={12} />}
                      </span>
                    </Table.Th>
                  );
                })}
              </Table.Tr>
            ))}
          </Table.Thead>

          <Table.Tbody>
            {rows.map((row) => (
              <Table.Tr
                key={row.id}
                className={rowClassName?.(row.original)}
              >
                {row.getVisibleCells().map((cell) => (
                  <Table.Td key={cell.id}>
                    {flexRender(cell.column.columnDef.cell, cell.getContext())}
                  </Table.Td>
                ))}
              </Table.Tr>
            ))}
          </Table.Tbody>
        </Table>
      </ScrollArea>

      {rows.length === 0 && !loading && (
        <div className={styles.empty}>
          <Database size={40} color="#94a3b8" />
          <Text fw={600}>{data.length === 0 ? emptyTitle : "Нічого не знайдено"}</Text>
          <Text c="dimmed" size="sm">
            {data.length === 0 ? emptyDescription : "Спробуйте змінити запит."}
          </Text>
        </div>
      )}
    </div>
  );
}
