import { Star } from "lucide-react";
import styles from "./ReviewsBlock.module.css";
import { Text } from "@mantine/core";

type RatingItem = {
  stars: number;
  count: number;
};

type ReviewsBlockProps = {
  averageRating?: number;
  totalUsersText?: string;
  ratings?: RatingItem[];
};

const DEFAULT_RATINGS: RatingItem[] = [
  { stars: 5.0, count: 2823 },
  { stars: 4.0, count: 38 },
  { stars: 3.0, count: 4 },
  { stars: 2.0, count: 0 },
  { stars: 1.0, count: 0 },
];

export default function ReviewsBlock({
  averageRating = 4.5,
  totalUsersText = "1.25 тис користувачів",
  ratings = DEFAULT_RATINGS,
}: ReviewsBlockProps) {
  const totalCount = ratings.reduce((acc, item) => acc + item.count, 0);

  const circlePercentage = (averageRating / 5) * 100;

  return (
    <div className={styles.container}>
      <Text fz={24} fw={600}>
        Відгуки
      </Text>
      <div className={styles.content}>
        <div className={styles.summary}>
          <div
            className={styles.circle}
            style={{
              background: `conic-gradient(#f59e0b ${circlePercentage}%, #e5e7eb 0%)`,
            }}
          >
            <div className={styles.circleInner}>
              <span className={styles.score}>{averageRating}</span>
            </div>
          </div>

          <div className={styles.starsMeta}>
            <div className={styles.starsRow}>
              {Array.from({ length: 5 }).map((_, index) => (
                <Star
                  key={index}
                  className={styles.starIcon}
                  fill="#fea945"
                  color="#fea945"
                  size={20}
                />
              ))}
            </div>
            <span className={styles.usersCount}>{totalUsersText}</span>
          </div>
        </div>

        <div className={styles.barsList}>
          {ratings.map((item) => {
            const percentage =
              totalCount > 0 ? (item.count / totalCount) * 100 : 0;

            return (
              <div key={item.stars} className={styles.barRow}>
                <div className={styles.starLabel}>
                  <Text fw={400} fz={15}>
                    {item.stars.toFixed(1)}
                  </Text>

                  <Star size={20} fill="#fea945" color="#fea945" />
                </div>

                <div className={styles.progressTrack}>
                  <div
                    className={styles.progressFill}
                    style={{ width: `${percentage}%` }}
                  />
                </div>

                <span className={styles.countLabel}>{item.count}</span>
              </div>
            );
          })}
        </div>
      </div>
    </div>
  );
}
