import { Select, Text } from "@mantine/core";
import { useEffect, useState } from "react";
import { useOutletContext } from "react-router-dom";
import type { ProfileContextType } from "@/app/layouts/ProfileLayout";
import styles from "./FavoritesTab.module.css";
import { ChevronDown } from "lucide-react";
import { useGetFavorites } from "@/features/favorites/model/hooks/useGetFavorites";
import AdvertisementCard from "@/entities/advertisement/ui/AdvertisementCard";
import AdvertisementCardSkeleton from "@/shared/ui/advertisement-card-skeleton";
import AppPagination from "@/shared/ui/pagination";

const SORT_OPTIONS = [
  { value: "newest", label: "Найновіші" },
  { value: "oldest", label: "Найстаріші" },
  { value: "popular", label: "Популярні" },
];

export default function FavoritesTab() {
  const { setOrientation } = useOutletContext<ProfileContextType>();
  const [sort, setSort] = useState<string | null>("newest");
  const [page, setPage] = useState(1);

  const { data: favorites, isLoading } = useGetFavorites(page);

  const handlePageChange = (newPage: number) => {
    setPage(newPage);
    window.scrollTo({ top: 0, behavior: "smooth" });
  };

  useEffect(() => {
    setOrientation("row");
  }, [setOrientation]);

  const handleSortChange = (value: string | null) => {
    setSort(value);
    setPage(1);
  };

  const advertisements = [...(favorites?.items ?? [])].sort((left, right) => {
    if (sort === "oldest") {
      return (
        new Date(left.advertisement.createdAt).getTime() -
        new Date(right.advertisement.createdAt).getTime()
      );
    }
    if (sort === "popular") {
      return right.advertisement.id - left.advertisement.id;
    }
    return (
      new Date(right.advertisement.createdAt).getTime() -
      new Date(left.advertisement.createdAt).getTime()
    );
  });

  if (isLoading) {
    return (
      <section className={styles.favoritesAdvertisements}>
        <div className={styles.advertisementsGrid}>
          {Array.from({ length: 8 }).map((_, index) => (
            <AdvertisementCardSkeleton key={index} />
          ))}
        </div>
      </section>
    );
  }

  return (
    <section className={styles.favoritesTab}>
      <div className={styles.header}>
        <Text fw={700} fz={24}>
          Обрані товари
        </Text>
        {advertisements.length > 0 && (
          <Select
            value={sort}
            onChange={handleSortChange}
            data={SORT_OPTIONS}
            rightSection={<ChevronDown size={16} />}
            rightSectionPointerEvents="none"
            allowDeselect={false}
            withCheckIcon={false}
            radius="md"
            classNames={{ input: styles.input }}
          />
        )}
      </div>

      {advertisements.length > 0 ? (
        <div className={styles.advertisementsGrid}>
          {advertisements.map(({ advertisement }) => (
            <AdvertisementCard key={advertisement.id} {...advertisement} />
          ))}
        </div>
      ) : (
        <div className="emptyState">
          <Text c="dimmed">У вас поки що немає обраних товарів.</Text>
        </div>
      )}

      {(favorites?.totalPages ?? 0) > 1 && (
        <AppPagination
          page={favorites?.page ?? page}
          totalPages={favorites?.totalPages ?? 1}
          onPageChange={handlePageChange}
        />
      )}
    </section>
  );
}
