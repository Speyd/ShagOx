import { useState, useEffect } from "react";
import { Text, Button } from "@mantine/core";
import { useGetCatalogAdvertisements } from "@/entities/advertisement/model/hooks/useGetCatalogAdvertisements";
import AdvertisementCard from "@/entities/advertisement/ui/AdvertisementCard";
import AppPagination from "@/shared/ui/pagination";
import EmptyState from "@/shared/ui/empty-state/EmptyState";
import { useSearchParams } from "react-router-dom";
import styles from "./FeaturedAdvertisements.module.css";
import AdvertisementCardSkeleton from "@/shared/ui/advertisement-card-skeleton";

export default function FeaturedAdvertisements() {
  const [page, setPage] = useState(1);
  const [searchParams, setSearchParams] = useSearchParams();
  const searchQuery = searchParams.get("search")?.trim() || "";

  useEffect(() => {
    setPage(1);
  }, [searchQuery]);

  const { data: advertisementsData, isLoading } = useGetCatalogAdvertisements({
    search: searchQuery || undefined,
    page,
  });

  const handlePageChange = (newPage: number) => {
    setPage(newPage);
    window.scrollTo({ top: 0, behavior: "smooth" });
  };

  const handleClearSearch = () => {
    const nextParams = new URLSearchParams(searchParams);
    nextParams.delete("search");
    setSearchParams(nextParams);
  };

  if (isLoading) {
    return (
      <section className={styles.featuredAdvertisements}>
        <div className={styles.featuredAdvertisementsGrid}>
          {Array.from({ length: 8 }).map((_, index) => (
            <AdvertisementCardSkeleton key={index} />
          ))}
        </div>
      </section>
    );
  }

  if (!advertisementsData?.items?.length) {
    return (
      <section className={styles.featuredAdvertisements}>
        {searchQuery && (
          <div className={styles.searchHeader}>
            <Text
              fz={{ base: 16, sm: 20 }}
              fw={600}
              style={{ wordBreak: "break-word" }}
            >
              Результати пошуку: «{searchQuery}»
            </Text>
          </div>
        )}
        <div className={styles.emptyWrapper}>
          <EmptyState
            label={
              searchQuery
                ? `За запитом «${searchQuery}» товарів не знайдено`
                : "Оголошень поки немає"
            }
            description={
              searchQuery
                ? "Спробуйте змінити пошуковий запит або перевірити правильність написання слів."
                : "Будьте першим, хто розмістить товар у каталозі!"
            }
            buttonLabel={searchQuery ? "Очистити пошук" : undefined}
            onButtonClick={searchQuery ? handleClearSearch : undefined}
          />
        </div>
      </section>
    );
  }

  return (
    <section className={styles.featuredAdvertisements}>
      {searchQuery && (
        <div className={styles.searchHeader}>
          <div className={styles.searchTitleGroup}>
            <Text fz={20} fw={600}>
              Результати пошуку: «{searchQuery}»
            </Text>
            {advertisementsData.totalCount !== undefined && (
              <Text fz={14} c="dimmed">
                Знайдено {advertisementsData.totalCount} товарів
              </Text>
            )}
          </div>
          <Button variant="subtle" size="sm" onClick={handleClearSearch}>
            Очистити пошук
          </Button>
        </div>
      )}

      <div className={styles.featuredAdvertisementsGrid}>
        {advertisementsData.items.map((ad) => (
          <AdvertisementCard key={ad.id} {...ad} />
        ))}
      </div>

      {advertisementsData.totalPages > 1 && (
        <AppPagination
          page={advertisementsData.page ?? page}
          totalPages={advertisementsData.totalPages}
          onPageChange={handlePageChange}
        />
      )}
    </section>
  );
}
