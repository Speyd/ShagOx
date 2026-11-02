import {
  flexRender,
  getCoreRowModel,
  getFilteredRowModel,
  getSortedRowModel,
  useReactTable,
  type SortingState,
} from "@tanstack/react-table";
import { Table, Paper, ScrollArea, Text, Center, Stack } from "@mantine/core";
import { Database } from "lucide-react";
import type { DataTableProps } from "./types";
import { useEffect, useState } from "react";

export default function DataTable<T>({
  data,
  columns,
  search,
}: DataTableProps<T>) {
  const [sorting, setSorting] = useState<SortingState>([]);
  const [globalFilter, setGlobalFilter] = useState(search);

  useEffect(() => {
    setGlobalFilter(search);
  }, [search]);

  const table = useReactTable({
    data,
    columns,

    state: {
      sorting,
      globalFilter,
    },

    onSortingChange: setSorting,
    onGlobalFilterChange: setGlobalFilter,

    getCoreRowModel: getCoreRowModel(),
    getSortedRowModel: getSortedRowModel(),
    getFilteredRowModel: getFilteredRowModel(),
  });

  const rows = table.getRowModel().rows;

  if (data.length === 0) {
    return (
      <Paper withBorder radius="lg" p="xl">
        <Center py={40}>
          <Stack align="center" gap="sm">
            <Database size={48} color="#94a3b8" />
            <Text fw={600} size="lg">
              No data
            </Text>
            <Text c="dimmed" size="sm">
              There is nothing to display yet.
            </Text>
          </Stack>
        </Center>
      </Paper>
    );
  }

  return (
    <Paper shadow="xs" radius="lg" withBorder style={{ overflow: "hidden" }}>
      <ScrollArea>
        <Table
          striped
          highlightOnHover
          withTableBorder
          withColumnBorders
          verticalSpacing="md"
          horizontalSpacing="lg"
          miw={900}
        >
          <Table.Thead>
            {table.getHeaderGroups().map((headerGroup) => (
              <Table.Tr key={headerGroup.id}>
                {headerGroup.headers.map((header) => (
                  <Table.Th key={header.id}>
                    {header.isPlaceholder
                      ? null
                      : flexRender(
                          header.column.columnDef.header,
                          header.getContext(),
                        )}
                    {{
                      asc: " ↑",
                      desc: " ↓",
                    }[header.column.getIsSorted() as string] ?? null}
                  </Table.Th>
                ))}
              </Table.Tr>
            ))}
          </Table.Thead>

          <Table.Tbody>
            {rows.length > 0 ? (
              rows.map((row) => (
                <Table.Tr key={row.id}>
                  {row.getVisibleCells().map((cell) => (
                    <Table.Td key={cell.id}>
                      {flexRender(
                        cell.column.columnDef.cell,
                        cell.getContext(),
                      )}
                    </Table.Td>
                  ))}
                </Table.Tr>
              ))
            ) : (
              <Table.Tr>
                <Table.Td colSpan={columns.length}>
                  <Center py={50}>
                    <Stack align="center" gap="xs">
                      <Database size={48} color="#94a3b8" />
                      <Text fw={600}>Nothing found</Text>
                      <Text c="dimmed" size="sm">
                        Try changing your search query.
                      </Text>
                    </Stack>
                  </Center>
                </Table.Td>
              </Table.Tr>
            )}
          </Table.Tbody>
        </Table>
      </ScrollArea>
    </Paper>
  );
}
