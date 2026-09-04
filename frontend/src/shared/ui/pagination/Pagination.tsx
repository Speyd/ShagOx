import { Group, Pagination } from "@mantine/core";

type Props = {
  page: number;
  totalPages: number;
  onPageChange: (page: number) => void;
};

export default function AppPagination({
  page,
  totalPages,
  onPageChange,
}: Props) {
  return (
    <Group justify="center">
      <Pagination value={page} total={totalPages} onChange={onPageChange} />
    </Group>
  );
}
