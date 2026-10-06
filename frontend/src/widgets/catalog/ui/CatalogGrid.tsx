import styles from "./CatalogGrid.module.css";
import AdvertisementCard from "@/entities/advertisement/ui/AdvertisementCard";
import type { Advertisement } from "@/shared/lib/types/advertisements";
import { PackageSearch, SlidersHorizontal, ChevronLeft, ChevronRight } from "lucide-react";

type CatalogGridProps = {
  items: Advertisement[];
  isLoading: boolean;
  totalCount: number;
  totalPages: number;
  page: number;
  onPageChange: (page: number) => void;
  search: string;
  sortBy: string;
  sortOrder: "asc" | "desc";
  onSortChange: (sortBy: string, sortOrder: "asc" | "desc") => void;
  onMobileFilterToggle?: () => void;
  onResetFilters?: () => void;
};

function SkeletonCard() {
  return (
    <div className={styles.skeletonCard}>
      <div className={styles.skeletonImage} />
      <div className={styles.skeletonLine} />
      <div className={`${styles.skeletonLine} ${styles.skeletonLineShort}`} />
      <div className={`${styles.skeletonLine} ${styles.skeletonLinePrice}`} />
      <div className={styles.skeletonButton} />
    </div>
  );
}

function LoadingSkeleton() {
  return (
    <div className={styles.skeletonGrid}>
      {Array.from({ length: 10 }).map((_, i) => (
        <SkeletonCard key={i} />
      ))}
    </div>
  );
}

function EmptyState({ onReset }: { onReset?: () => void }) {
  return (
    <div className={styles.emptyState}>
      <div className={styles.emptyIcon}>
        <PackageSearch size={36} />
      </div>
      <span className={styles.emptyTitle}>Нічого не знайдено</span>
      <span className={styles.emptyText}>
        Спробуйте змінити параметри пошуку або очистити фільтри
      </span>
      {onReset && (
        <button
          type="button"
          onClick={onReset}
          style={{
            marginTop: 8,
            padding: "8px 18px",
            borderRadius: 8,
            border: "1.5px solid #d2dfef",
            background: "#ffffff",
            color: "var(--color-base)",
            fontWeight: 600,
            fontSize: 13.5,
            cursor: "pointer",
          }}
        >
          Очистити всі фільтри
        </button>
      )}
    </div>
  );
}

function CustomPagination({
  page,
  totalPages,
  onPageChange,
}: {
  page: number;
  totalPages: number;
  onPageChange: (p: number) => void;
}) {
  const getPageNumbers = () => {
    const pages: (number | string)[] = [];
    if (totalPages <= 7) {
      for (let i = 1; i <= totalPages; i++) pages.push(i);
    } else {
      pages.push(1);
      if (page > 3) pages.push("...");

      const start = Math.max(2, page - 1);
      const end = Math.min(totalPages - 1, page + 1);

      for (let i = start; i <= end; i++) {
        if (!pages.includes(i)) pages.push(i);
      }

      if (page < totalPages - 2) pages.push("...");
      pages.push(totalPages);
    }
    return pages;
  };

  const pages = getPageNumbers();

  return (
    <nav className={styles.customPagination} aria-label="Pagination">
      <button
        type="button"
        className={styles.pageBtn}
        disabled={page <= 1}
        onClick={() => onPageChange(page - 1)}
        aria-label="Попередня сторінка"
      >
        <ChevronLeft size={16} />
      </button>

      {pages.map((p, idx) => {
        if (typeof p === "string") {
          return (
            <span key={`dots-${idx}`} className={styles.pageDots}>
              ...
            </span>
          );
        }
        const isActive = p === page;
        return (
          <button
            key={p}
            type="button"
            className={`${styles.pageBtn} ${isActive ? styles.pageBtnActive : ""}`}
            onClick={() => onPageChange(p)}
          >
            {p}
          </button>
        );
      })}

      <button
        type="button"
        className={styles.pageBtn}
        disabled={page >= totalPages}
        onClick={() => onPageChange(page + 1)}
        aria-label="Наступна сторінка"
      >
        <ChevronRight size={16} />
      </button>
    </nav>
  );
}

export default function CatalogGrid({
  items,
  isLoading,
  totalCount,
  totalPages,
  page,
  onPageChange,
  search,
  sortBy,
  sortOrder,
  onSortChange,
  onMobileFilterToggle,
  onResetFilters,
}: CatalogGridProps) {
  const handleSortChange = (e: React.ChangeEvent<HTMLSelectElement>) => {
    const value = e.target.value;
    const [newSortBy, newSortOrder] = value.split(":");
    onSortChange(newSortBy, newSortOrder as "asc" | "desc");
  };

  const titleText = search ? `“${search} ”` : "“Каталог ”";

  return (
    <div className={styles.gridSection}>
      <div className={styles.topBar}>
        <div className={styles.searchInfo}>
          <h1 className={styles.searchTitle}>{titleText}</h1>
          {!isLoading && (
            <span className={styles.resultsCount}>
              {totalCount} {getResultsWord()}
            </span>
          )}
        </div>

        <div className={styles.topRightActions}>
          <button
            type="button"
            className={styles.mobileFilterToggle}
            onClick={onMobileFilterToggle}
          >
            <SlidersHorizontal size={15} />
            <span>Фільтри</span>
          </button>

          <select
            className={styles.sortSelect}
            value={`${sortBy}:${sortOrder}`}
            onChange={handleSortChange}
            aria-label="Сортування товарів"
          >
            <option value="createdAt:desc">Спочатку нові</option>
            <option value="createdAt:asc">Спочатку старі</option>
            <option value="price:asc">Дешевші спочатку</option>
            <option value="price:desc">Дорожчі спочатку</option>
            <option value="title:asc">За назвою (А-Я)</option>
            <option value="title:desc">За назвою (Я-А)</option>
          </select>
        </div>
      </div>
      
      {isLoading ? (
        <LoadingSkeleton />
      ) : items.length === 0 ? (
        <EmptyState onReset={onResetFilters} />
      ) : (
        <>
          <div className={styles.grid}>
            {items.map((item) => (
              <AdvertisementCard key={item.id} {...item} />
            ))}
          </div>

          {totalPages > 1 && (
            <div className={styles.paginationWrapper}>
              <CustomPagination
                page={page}
                totalPages={totalPages}
                onPageChange={onPageChange}
              />
            </div>
          )}
        </>
      )}
    </div>
  );
}

function getResultsWord(): string {
  return "Знайдено";
}
