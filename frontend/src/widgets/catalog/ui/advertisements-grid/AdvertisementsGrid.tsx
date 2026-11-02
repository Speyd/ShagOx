import { useState, useEffect } from "react";
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
  attributeFilters: {
    attributes: Record<string, unknown[]>;
    variantAttributes: Record<string, unknown>;
  };
  minPrice?: number;
  maxPrice?: number;
  hasActiveFilters: boolean;
  onResetFilters: () => void;
};

export function AdvertisementsGrid({
  categoryId,
  isLoadingCategory,
  searchQuery = "",
  attributeFilters,
  minPrice,
  maxPrice,
  hasActiveFilters,
  onResetFilters,
}: AdvertisementsGridProps) {
  const [page, setPage] = useState(1);
  const navigate = useNavigate();

  useEffect(() => {
    setPage(1);
  }, [categoryId, minPrice, maxPrice, attributeFilters, searchQuery]);

  const { data, isLoading, isError } = useGetCatalogAdvertisements({
    categoryId,
    minPrice,
    maxPrice,
    search: searchQuery,
    page,
    ...attributeFilters,
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
