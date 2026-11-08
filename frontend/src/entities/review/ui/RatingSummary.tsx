import { Rating, Skeleton } from "@mantine/core";
import { Star } from "lucide-react";
import clsx from "clsx";
import { pluralizeReviews } from "../lib/format";
import { useAdvertisementRating } from "../model/hooks";
import styles from "./RatingSummary.module.css";

type RatingSummaryProps = {
  advertisementId: number;
  size?: "sm" | "lg";
  onReviewsClick?: () => void;
};

export function RatingSummary({ advertisementId, size = "sm", onReviewsClick }: RatingSummaryProps) {
  const { data: rating, isLoading } = useAdvertisementRating(advertisementId);

  if (isLoading) {
    return <Skeleton height={size === "lg" ? 28 : 16} width={size === "lg" ? 220 : 80} radius="sm" />;
  }

  const average = Math.round((rating?.average ?? 0) * 10) / 10;
  const count = rating?.count ?? 0;
  const hasReviews = count > 0;

  if (size === "sm") {
    return (
      <span className={styles.compact}>
        <Star className={clsx(styles.compactStar, !hasReviews && styles.compactStarEmpty)} />
        {hasReviews ? (
          <>
            <span className={styles.compactValue}>{average.toFixed(1)}</span>
            <span className={styles.compactCount}>({count})</span>
          </>
        ) : (
          <span className={styles.compactCount}>Немає відгуків</span>
        )}
      </span>
    );
  }

  return (
    <div className={styles.full}>
      <Rating value={average} fractions={10} readOnly size="lg" color="#fea945" />
      {hasReviews && <span className={styles.fullValue}>{average.toFixed(1)}</span>}
      <button type="button" className={styles.reviewsLink} onClick={onReviewsClick}>
        {hasReviews ? pluralizeReviews(count) : "Ще немає відгуків"}
      </button>
    </div>
  );
}
