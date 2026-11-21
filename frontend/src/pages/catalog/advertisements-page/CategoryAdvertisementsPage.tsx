import { useParams, useSearchParams } from "react-router-dom";
import { useDebouncedValue, useDisclosure } from "@mantine/hooks";
import { Button, Drawer } from "@mantine/core";
import { SlidersHorizontal } from "lucide-react";

import { AdvertisementsGrid, CatalogFilters } from "@/widgets/catalog";
import { useGetCategoryByCode } from "@/entities/category/model/useGetCategoryByCode";
import { useCatalogFilters } from "@/features/catalog";

import styles from "./CategoryAdvertisementsPage.module.css";

export default function CategoryAdvertisementsPage() {
  const { categoryCode } = useParams<{ categoryCode: string }>();
  const [searchParams, setSearchParams] = useSearchParams();
  const [drawerOpened, { open: openDrawer, close: closeDrawer }] = useDisclosure(false);

  const searchQuery = searchParams.get("search") || "";
  const [debouncedSearchQuery] = useDebouncedValue(searchQuery, 400);

  const { data: category, isLoading: isLoadingCategory } = useGetCategoryByCode(categoryCode);
  const filters = useCatalogFilters(category?.id);

  const hasFilterPanel = Boolean(filters.priceBounds) || filters.groups.length > 0;
  const hasActiveFilters = filters.activeCount > 0 || searchQuery !== "";

  const resetAll = () => {
    filters.reset();
    if (searchQuery) {
      searchParams.delete("search");
      setSearchParams(searchParams);
    }
  };

  return (
    <section className={hasFilterPanel ? styles.categoryAdvertisementsPage : styles.categoryAdvertisementsPageSingle}>
      {hasFilterPanel && (
        <>
          <div className={styles.mobileFilterButtonWrapper}>
            <Button
              leftSection={<SlidersHorizontal size={18} />}
              variant="outline"
              color="gray"
              onClick={openDrawer}
              className={styles.mobileFilterBtn}
            >
              Фільтри {filters.activeCount > 0 && `(${filters.activeCount})`}
            </Button>
          </div>

          <aside className={styles.desktopFilters}>
            <CatalogFilters filters={filters} />
          </aside>

          <Drawer
            opened={drawerOpened}
            onClose={closeDrawer}
            title="Фільтри"
            padding="md"
            size="85%"
            position="left"
            zIndex={1002}
          >
            <CatalogFilters filters={filters} />
          </Drawer>
        </>
      )}

      <AdvertisementsGrid
        categoryId={category?.id}
        isLoadingCategory={isLoadingCategory || filters.isLoading}
        filters={filters.searchParams}
        searchQuery={debouncedSearchQuery}
        hasActiveFilters={hasActiveFilters}
        onResetFilters={resetAll}
      />
    </section>
  );
}
