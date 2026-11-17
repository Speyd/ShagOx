import { useState } from "react";
import { Link, useNavigate } from "react-router-dom";
import { Skeleton, Text } from "@mantine/core";
import { ImageOff, Plus } from "lucide-react";
import clsx from "clsx";
import { useAuthStore } from "@/features/auth";
import { useGetSellerAdvertisements } from "@/entities/advertisement/model/hooks/useGetSellerAdvertisements";
import DeleteAdvertisementButton from "@/features/advertisement/delete-advertisement";
import { UpdateAdvertisementButton } from "@/features/advertisement/update-advertisement";
import type { Advertisement } from "@/shared/lib/types/advertisements";
import Button from "@/shared/ui/buttons/button";
import Price from "@/shared/ui/data-display/price";
import AppPagination from "@/shared/ui/navigation/pagination";
import { EmptyState } from "@/shared/ui/data-display/empty-state/EmptyState";
import styles from "./MyAdvertisements.module.css";

const PAGE_SIZE = 10;

function getPriceRange(advertisement: Advertisement) {
  const prices = advertisement.variants.map((variant) => variant.price);
  if (prices.length === 0) return { min: advertisement.price ?? 0, isRange: false };
  const min = Math.min(...prices);
  return { min, isRange: prices.some((price) => price !== min) };
}

function pluralizeVariants(count: number) {
  const mod10 = count % 10;
  const mod100 = count % 100;
  if (mod10 === 1 && mod100 !== 11) return "варіант";
  if (mod10 >= 2 && mod10 <= 4 && (mod100 < 12 || mod100 > 14)) return "варіанти";
  return "варіантів";
}

function getTotalStock(advertisement: Advertisement) {
  return advertisement.variants.reduce((sum, variant) => sum + variant.stock, 0);
}

function getMainPicture(advertisement: Advertisement) {
  return [...advertisement.pictures].sort((a, b) => a.order - b.order)[0];
}

type AdvertisementRowProps = {
  advertisement: Advertisement;
  onDeleted: () => void;
};

function AdvertisementRow({ advertisement, onDeleted }: AdvertisementRowProps) {
  const picture = getMainPicture(advertisement);
  const { min, isRange } = getPriceRange(advertisement);
  const stock = getTotalStock(advertisement);
  const variantsCount = advertisement.variants.length;
  const meta = [
    advertisement.category?.label,
    advertisement.condition?.label ?? advertisement.condition?.code,
  ].filter(Boolean);

  return (
    <article className={styles.row}>
      <Link to={`/advertisement/${advertisement.id}`} className={styles.preview}>
        {picture ? (
          <img src={picture.url} alt={advertisement.title} />
        ) : (
          <ImageOff size={24} />
        )}
      </Link>

      <div className={styles.info}>
        <Link to={`/advertisement/${advertisement.id}`} className={styles.title}>
          {advertisement.title}
        </Link>
        {meta.length > 0 && <span className={styles.meta}>{meta.join(" · ")}</span>}
        <div className={styles.badges}>
          <span className={styles.status}>
            {advertisement.status?.label ?? advertisement.status?.code}
          </span>
          {variantsCount > 1 && (
            <span className={styles.badge}>
              {variantsCount} {pluralizeVariants(variantsCount)}
            </span>
          )}
        </div>
      </div>

      <div className={styles.price}>
        {isRange && <span className={styles.priceFrom}>від</span>}
        <Price price={min} currency={advertisement.currency?.symbol} />
      </div>

      <span className={clsx(styles.stock, stock === 0 && styles.outOfStock)}>
        {stock > 0 ? `${stock} шт.` : "Немає в наявності"}
      </span>

      <div className={styles.actions}>
        <UpdateAdvertisementButton id={advertisement.id} />
        <DeleteAdvertisementButton
          id={advertisement.id}
          title={advertisement.title}
          onDeleted={onDeleted}
        />
      </div>
    </article>
  );
}

export default function MyAdvertisements() {
  const navigate = useNavigate();
  const userId = useAuthStore((state) => state.user?.id);
  const [page, setPage] = useState(1);

  const { data, isLoading, isError } = useGetSellerAdvertisements(
    userId,
    page,
    PAGE_SIZE,
  );

  const items = data?.items ?? [];
  const totalPages = data?.totalPages ?? 1;

  const handleDeleted = () => {
    if (items.length === 1 && page > 1) setPage(page - 1);
  };

  const goToCreate = () => navigate("/create-advertisement");

  const handlePageChange = (next: number) => {
    setPage(next);
    window.scrollTo({ top: 0, behavior: "smooth" });
  };

  const renderContent = () => {
    if (isLoading) {
      return (
        <div className={styles.list}>
          {Array.from({ length: 3 }, (_, index) => (
            <Skeleton key={index} height={96} radius="md" />
          ))}
        </div>
      );
    }

    if (isError) {
      return (
        <EmptyState
          variant="dashed"
          description="Не вдалося завантажити ваші товари. Спробуйте оновити сторінку."
        />
      );
    }

    if (items.length === 0) {
      return (
        <EmptyState
          variant="dashed"
          description="У вас поки що немає створених оголошень."
          buttonLabel="Додати перший товар"
          onButtonClick={goToCreate}
        />
      );
    }

    return (
      <div className={styles.list}>
        {items.map((advertisement) => (
          <AdvertisementRow
            key={advertisement.id}
            advertisement={advertisement}
            onDeleted={handleDeleted}
          />
        ))}
      </div>
    );
  };

  return (
    <section className={styles.section}>
      <div className={styles.header}>
        <Text className={styles.heading}>
          Мої товари
          {data && data.totalCount > 0 && (
            <span className={styles.count}>{data.totalCount}</span>
          )}
        </Text>
        <Button size="sm" leftSection={Plus} iconProps={{ size: 18 }} onClick={goToCreate}>
          Додати товар
        </Button>
      </div>

      {renderContent()}

      <AppPagination
        page={page}
        totalPages={totalPages}
        onPageChange={handlePageChange}
      />
    </section>
  );
}
