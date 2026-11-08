import { Rating, Skeleton, Text } from "@mantine/core";
import { Star } from "lucide-react";
import { pluralizeReviews, useGetReviewStats } from "@/entities/review";
import styles from "./ReviewsBlock.module.css";

type ReviewsBlockProps = {
  advertisementId: number;
};

export default function ReviewsBlock({ advertisementId }: ReviewsBlockProps) {
  const { data: stats, isLoading } = useGetReviewStats(advertisementId);
  const average = Math.round(stats.average * 10) / 10;

  return (
    <div id="reviews" className={styles.container}>
      <Text fz={24} fw={600}>
        Відгуки
      </Text>

      {isLoading ? (
        <Skeleton height={120} mt="md" radius="md" />
      ) : (
        <div className={styles.content}>
          <div className={styles.summary}>
            <div
              className={styles.circle}
              style={{
                background: `conic-gradient(#f59e0b ${(average / 5) * 100}%, #e5e7eb 0%)`,
              }}
            >
              <div className={styles.circleInner}>
                <span className={styles.score}>{average.toFixed(1)}</span>
              </div>
            </div>

            <div className={styles.starsMeta}>
              <Rating value={average} fractions={10} readOnly color="#fea945" />
              <span className={styles.usersCount}>
                {stats.total > 0 ? pluralizeReviews(stats.total) : "Ще немає відгуків"}
              </span>
            </div>
          </div>

          <div className={styles.barsList}>
            {stats.distribution.map(({ grade, count }) => (
              <div key={grade} className={styles.barRow}>
                <div className={styles.starLabel}>
                  <Text fz={15}>{grade.toFixed(1)}</Text>
                  <Star size={20} fill="#fea945" color="#fea945" />
                </div>

                <div className={styles.progressTrack}>
                  <div
                    className={styles.progressFill}
                    style={{
                      width: `${stats.total ? (count / stats.total) * 100 : 0}%`,
                    }}
                  />
                </div>

                <span className={styles.countLabel}>{count}</span>
              </div>
            ))}
          </div>
        </div>
      )}
    </div>
  );
}
