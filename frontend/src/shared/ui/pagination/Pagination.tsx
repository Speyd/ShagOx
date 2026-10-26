import { Pagination } from "@mantine/core";
import styles from "./Pagination.module.css";

type Props = {
  page?: number;
  totalPages?: number;
  onPageChange?: (page: number) => void;
};

export default function AppPagination({
  page = 1,
  totalPages = 1,
  onPageChange,
}: Props) {
  if (totalPages <= 1) return null;

  return (
    <div className={styles.paginationWrapper}>
      <Pagination
        value={page}
        total={totalPages}
        onChange={onPageChange}
        siblings={1}
        boundaries={1}
        classNames={{
          control: styles.control,
          dots: styles.dots,
        }}
      />
    </div>
  );
}
