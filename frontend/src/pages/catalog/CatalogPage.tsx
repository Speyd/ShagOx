import { useState, useMemo, useCallback } from "react";
import styles from "./CatalogPage.module.css";
import Container from "@/shared/ui/container";
import { CatalogFilters, CatalogGrid } from "@/widgets/catalog";
import Breadcrumbs from "@/widgets/breadcrumbs";
import { useGetCatalogAdvertisements } from "@/entities/advertisement/model/hooks/useGetCatalogAdvertisements";
import { X } from "lucide-react";
import { useCatalogFilters } from "@/features/catalog/model/hooks/useCatalogFilters";

export default function CatalogPage() {
  const [mobileFiltersOpen, setMobileFiltersOpen] = useState(false);
  const { filters, updateParams, clearFilters } = useCatalogFilters();

  const queryParams = useMemo(
    () => ({
      page: filters.page,
      pageSize: 20,
      search: filters.search ?? undefined,
      categoryIds:
        filters.categoryIds.length > 0 ? filters.categoryIds : undefined,
      inStock: filters.inStock ?? undefined,
      memory: filters.memory.length > 0 ? filters.memory : undefined,
      colors: filters.colors.length > 0 ? filters.colors : undefined,
      minPrice: filters.minPrice ?? undefined,
      maxPrice: filters.maxPrice ?? undefined,
      sortBy: filters.sortBy,
      sortOrder: filters.sortOrder,
    }),
    [filters],
  );

  const { data, isLoading } = useGetCatalogAdvertisements(queryParams);

  const items = data?.items ?? [];
  const totalCount = data?.totalCount ?? 0;
  const totalPages = data?.totalPages ?? 1;

  const handlePageChange = useCallback(
    (newPage: number) => {
      updateParams({ page: newPage });
      window.scrollTo({ top: 0, behavior: "smooth" });
    },
    [updateParams],
  );

  const filtersProps = useMemo(
    () => ({
      selectedCategories: filters.categoryIds,
      onCategoriesChange: (categoryIds: number[]) =>
        updateParams({ categoryIds }),
      selectedMemory: filters.memory,
      onMemoryChange: (memory: string[]) => updateParams({ memory }),
      selectedColors: filters.colors,
      onColorsChange: (colors: string[]) => updateParams({ colors }),
      inStock: filters.inStock ?? undefined,
      onInStockChange: (inStock: boolean | undefined) =>
        updateParams({ inStock }),
      minPrice: filters.minPrice ?? undefined,
      maxPrice: filters.maxPrice ?? undefined,
      onMinPriceChange: (minPrice: number | undefined) =>
        updateParams({ minPrice }),
      onMaxPriceChange: (maxPrice: number | undefined) =>
        updateParams({ maxPrice }),
      onClearFilters: clearFilters,
    }),
    [filters, updateParams, clearFilters],
  );

  return (
    <div className={styles.catalogPage}>
      <Container>
        <div className={styles.breadcrumbsRow}>
          <Breadcrumbs
            lastItemLabel={
              filters.search ? `Пошук "${filters.search}"` : "Каталог"
            }
          />
        </div>

        <div className={styles.catalogLayout}>
          <CatalogFilters {...filtersProps} />

          <CatalogGrid
            items={items}
            isLoading={isLoading}
            totalCount={totalCount}
            totalPages={totalPages}
            page={filters.page}
            onPageChange={handlePageChange}
            search={filters.search ?? ""}
            sortBy={filters.sortBy}
            sortOrder={filters.sortOrder}
            onSortChange={(sortBy, sortOrder) =>
              updateParams({ sortBy, sortOrder })
            }
            onMobileFilterToggle={() => setMobileFiltersOpen(true)}
            onResetFilters={clearFilters}
          />
        </div>
      </Container>

      <div
        className={`${styles.mobileFilterOverlay} ${
          mobileFiltersOpen ? styles.mobileFilterOverlayOpen : ""
        }`}
        onClick={() => setMobileFiltersOpen(false)}
      />
      <div
        className={`${styles.mobileFilterDrawer} ${
          mobileFiltersOpen ? styles.mobileFilterDrawerOpen : ""
        }`}
      >
        <div className={styles.mobileFilterDrawerHeader}>
          <span className={styles.mobileFilterDrawerTitle}>Фільтри</span>
          <button
            type="button"
            className={styles.mobileFilterClose}
            onClick={() => setMobileFiltersOpen(false)}
            aria-label="Закрити фільтри"
          >
            <X size={18} />
          </button>
        </div>
        <CatalogFilters {...filtersProps} mobile />
      </div>
    </div>
  );
}
