import { useState } from "react";
import { useNavigate } from "react-router-dom";
import { Text } from "@mantine/core";

import { useGetCatalogAdvertisements } from "@/entities/advertisement/model/hooks/useGetCatalogAdvertisements";
import AppPagination from "@/shared/ui/navigation/pagination";

import styles from "./AdvertisementsGrid.module.css";
import {
  AdvertisementCard,
  AdvertisementCardSkeleton,
} from "@/entities/advertisement";
import { EmptyState } from "@/shared/ui/data-display";

export type AdvertisementsGridProps = {
  categoryId?: number;
  isLoadingCategory?: boolean;
  searchQuery?: string;
  filters: {
    attributes: Record<string, unknown[]>;
    minPrice?: number;
    maxPrice?: number;
  };
  hasActiveFilters: boolean;
  onResetFilters: () => void;
};

export function AdvertisementsGrid({
  categoryId,
  isLoadingCategory,
  searchQuery = "",
  filters,
  hasActiveFilters,
  onResetFilters,
}: AdvertisementsGridProps) {
  const navigate = useNavigate();
  const queryKey = JSON.stringify({ categoryId, searchQuery, filters });
  const [pageState, setPageState] = useState({ queryKey, page: 1 });
  const page = pageState.queryKey === queryKey ? pageState.page : 1;
  const setPage = (next: number) => setPageState({ queryKey, page: next });

  const { data, isLoading, isError } = useGetCatalogAdvertisements({
    categoryId,
    search: searchQuery,
    page,
    ...filters,
  });

  const handlePageChange = (newPage: number) => {
    setPage(newPage);
    window.scrollTo({ top: 0, behavior: "smooth" });
  };

  if (isLoadingCategory || isLoading) {
    return (
      <div className={styles.advertisementsContainer}>
        <div className={styles.advertisementsGrid}>
          {Array.from({ length: 8 }).map((_, index) => (
            <AdvertisementCardSkeleton key={index} />
          ))}
        </div>
      </div>
    );
  }

  if (isError) {
    return (
      <div className={styles.statusWrapper}>
        <Text c="red">Не вдалося завантажити оголошення.</Text>
      </div>
    );
  }

  if (!data?.items.length) {
    return (
      <div
        className={`${styles.emptyStateWrapper} ${
          !hasActiveFilters ? styles.emptyCategoryWrapper : ""
        }`}
      >
        <EmptyState
          title={
            hasActiveFilters
              ? "За обраними фільтрами нічого не знайдено"
              : "У цій категорії поки немає оголошень"
          }
          description={
            hasActiveFilters
              ? "Спробуйте змінити значення фільтрів або скинути їх."
              : "Будьте першим, хто розмістить товар у цій категорії!"
          }
          buttonLabel={
            hasActiveFilters ? "Скинути фільтри" : "Додати оголошення"
          }
          onButtonClick={
            hasActiveFilters
              ? onResetFilters
              : () => navigate("/create-advertisement")
          }
        />
      </div>
    );
  }

  return (
    <div className={styles.advertisementsContainer}>
      <div className={styles.advertisementsGrid}>
        {data.items.map((advertisement) => (
          <AdvertisementCard key={advertisement.id} {...advertisement} />
        ))}
      </div>

      {data.totalPages > 1 && (
        <AppPagination
          page={data.page ?? page}
          totalPages={data.totalPages ?? 1}
          onPageChange={handlePageChange}
        />
      )}
    </div>
  );
}
