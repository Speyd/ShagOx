import { useState } from "react";
import AdvertisementCard from "@/entities/advertisement/ui/AdvertisementCard";
import { useGetCatalogAdvertisements } from "@/entities/advertisement/model/hooks/useGetCatalogAdvertisements";
import { Text } from "@mantine/core";
import AppPagination from "@/shared/ui/pagination";
import EmptyState from "@/shared/ui/empty-state/EmptyState";
import { useNavigate } from "react-router-dom";
import styles from "./AdvertisementsGrid.module.css";
import AdvertisementCardSkeleton from "@/shared/ui/advertisement-card-skeleton";

export type AdvertisementsGridProps = {
  categoryId?: number;
  isLoadingCategory?: boolean;
  attributeFilters: {
    attributes: Record<string, unknown[]>;
    variantAttributes: Record<string, unknown>;
  };
  minPrice?: number;
  maxPrice?: number;
  hasActiveFilters: boolean;
  onResetFilters: () => void;
};

export default function AdvertisementsGrid({
  categoryId,
  isLoadingCategory,
  attributeFilters,
  minPrice,
  maxPrice,
  hasActiveFilters,
  onResetFilters,
}: AdvertisementsGridProps) {
  const [page, setPage] = useState(1);
  const navigate = useNavigate();

  const [prevFilters, setPrevFilters] = useState({
    categoryId,
    minPrice,
    maxPrice,
    attributeFilters,
  });

  if (
    prevFilters.categoryId !== categoryId ||
    prevFilters.minPrice !== minPrice ||
    prevFilters.maxPrice !== maxPrice ||
    prevFilters.attributeFilters !== attributeFilters
  ) {
    setPrevFilters({ categoryId, minPrice, maxPrice, attributeFilters });
    setPage(1);
  }

  const { data, isLoading, isError } = useGetCatalogAdvertisements({
    categoryId,
    minPrice,
    maxPrice,
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
          label={
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

      <AppPagination
        page={data.page ?? page}
        totalPages={data.totalPages ?? 1}
        onPageChange={handlePageChange}
      />
    </div>
  );
}
