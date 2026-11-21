import { Text } from "@mantine/core";
import { useEffect } from "react";
import { Link, useOutletContext } from "react-router-dom";
import { Heart, MessageSquare, Package, ShoppingBag, Star } from "lucide-react";
import type { ProfileContextType } from "@/app/layouts/ProfileLayout";
import { useMyStatistics } from "@/entities/user/model/hooks/useMyStatistics";
import type {
  GradeCount,
  MoneyAmount,
} from "@/entities/user/model/statistics";
import { EmptyState } from "@/shared/ui/data-display/empty-state/EmptyState";
import styles from "./StatisticsTab.module.css";

const formatMoney = (amounts: MoneyAmount[]) =>
  amounts.length === 0
    ? "0 ₴"
    : amounts
        .map(
          (x) =>
            `${x.amount.toLocaleString("uk-UA", { maximumFractionDigits: 0 })} ${x.symbol}`,
        )
        .join(" · ");

const formatNumber = (value: number) => value.toLocaleString("uk-UA");

type TileProps = {
  label: string;
  value: string;
  hint?: string;
};

function StatTile({ label, value, hint }: TileProps) {
  return (
    <div className={styles.tile}>
      <span className={styles.tileLabel}>{label}</span>
      <span className={styles.tileValue}>{value}</span>
      {hint && <span className={styles.tileHint}>{hint}</span>}
    </div>
  );
}

/** Single-series horizontal bars: share of each grade, count as a direct label. */
function GradeBars({ grades, total }: { grades: GradeCount[]; total: number }) {
  const max = Math.max(1, ...grades.map((g) => g.count));

  return (
    <ul className={styles.grades} aria-label="Розподіл оцінок">
      {grades.map((g) => (
        <li
          key={g.grade}
          className={styles.gradeRow}
          title={`${g.grade} ★ — ${g.count} (${total ? Math.round((g.count / total) * 100) : 0}%)`}
        >
          <span className={styles.gradeLabel}>{g.grade} ★</span>
          <span className={styles.gradeTrack}>
            {g.count > 0 && (
              <span
                className={styles.gradeBar}
                style={{ width: `${(g.count / max) * 100}%` }}
              />
            )}
          </span>
          <span className={styles.gradeCount}>{g.count}</span>
        </li>
      ))}
    </ul>
  );
}

export default function StatisticsTab() {
  const { setOrientation } = useOutletContext<ProfileContextType>();
  const { data, isLoading, isError } = useMyStatistics();

  useEffect(() => {
    setOrientation("row");
  }, [setOrientation]);

  if (isLoading) {
    return (
      <section className={styles.profileStatistics}>
        <Text className={styles.title}>Моя статистика</Text>
        <Text c="dimmed">Завантаження...</Text>
      </section>
    );
  }

  if (isError || !data) {
    return (
      <section className={styles.profileStatistics}>
        <Text className={styles.title}>Моя статистика</Text>
        <Text c="red">Не вдалося завантажити статистику.</Text>
      </section>
    );
  }

  const { seller, buyer } = data;
  const isSeller = seller.activeAdvertisements > 0 || seller.salesCount > 0;

  return (
    <section className={styles.profileStatistics}>
      <Text className={styles.title}>Моя статистика</Text>

      <div className={styles.block}>
        <div className={styles.blockHeader}>
          <ShoppingBag size={18} />
          <Text fw={600}>Як покупець</Text>
        </div>
        <div className={styles.tiles}>
          <StatTile
            label="Замовлень"
            value={formatNumber(buyer.ordersCount)}
            hint={buyer.activeOrdersCount > 0 ? `${buyer.activeOrdersCount} в дорозі або в обробці` : undefined}
          />
          <StatTile label="Витрачено" value={formatMoney(buyer.spent)} hint="без скасованих" />
          <StatTile label="В обраному" value={formatNumber(buyer.favoritesCount)} />
          <StatTile label="Залишено відгуків" value={formatNumber(buyer.reviewsWritten)} />
        </div>
      </div>

      <div className={styles.block}>
        <div className={styles.blockHeader}>
          <Package size={18} />
          <Text fw={600}>Як продавець</Text>
        </div>

        {!isSeller ? (
          <EmptyState
            variant="dashed"
            description="У вас ще немає товарів. Додайте перший — і тут зʼявиться статистика продажів."
          />
        ) : (
          <>
            <div className={styles.tiles}>
              <StatTile
                label="Товарів у продажу"
                value={formatNumber(seller.activeAdvertisements)}
                hint={
                  seller.outOfStockAdvertisements > 0
                    ? `${seller.outOfStockAdvertisements} без залишку`
                    : `${formatNumber(seller.unitsInStock)} од. на складі`
                }
              />
              <StatTile
                label="Продажі"
                value={formatNumber(seller.salesCount)}
                hint={`${formatNumber(seller.soldUnits)} од. товару`}
              />
              <StatTile label="Виторг" value={formatMoney(seller.revenue)} hint="без скасованих" />
              <StatTile
                label="Додали в обране"
                value={formatNumber(seller.favoritesCount)}
              />
            </div>

            <div className={styles.columns}>
              <div className={styles.panel}>
                <div className={styles.panelHeader}>
                  <Text fw={600} fz={15}>
                    Оцінки покупців
                  </Text>
                  <span className={styles.rating}>
                    <Star size={16} className={styles.ratingIcon} />
                    {seller.averageGrade?.toFixed(1) ?? "—"}
                    <span className={styles.ratingCount}>
                      · {formatNumber(seller.reviewsCount)} відгуків
                    </span>
                  </span>
                </div>
                {seller.reviewsCount > 0 ? (
                  <GradeBars grades={seller.grades} total={seller.reviewsCount} />
                ) : (
                  <Text c="dimmed" fz={14}>
                    Відгуків на ваші товари ще немає.
                  </Text>
                )}
              </div>

              <div className={styles.panel}>
                <Text fw={600} fz={15}>
                  Найкращі товари
                </Text>
                <ul className={styles.topList}>
                  {seller.topAdvertisements.map((advert) => (
                    <li key={advert.id} className={styles.topItem}>
                      {advert.pictureUrl ? (
                        <img src={advert.pictureUrl} alt="" className={styles.topImage} />
                      ) : (
                        <span className={styles.topImage} />
                      )}
                      <Link to={`/advertisement/${advert.id}`} className={styles.topTitle}>
                        {advert.title}
                      </Link>
                      <span className={styles.topMeta}>
                        <span title="Продано одиниць">
                          <ShoppingBag size={13} /> {advert.soldUnits}
                        </span>
                        <span title="В обраному">
                          <Heart size={13} /> {advert.favorites}
                        </span>
                        <span title="Відгуків">
                          <MessageSquare size={13} /> {advert.reviews}
                        </span>
                      </span>
                    </li>
                  ))}
                </ul>
              </div>
            </div>
          </>
        )}
      </div>
    </section>
  );
}
