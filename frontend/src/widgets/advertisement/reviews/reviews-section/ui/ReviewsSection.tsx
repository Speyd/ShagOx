import { useState } from "react";
import { useNavigate } from "react-router-dom";
import { Button, Skeleton, Text, UnstyledButton } from "@mantine/core";
import { Star } from "lucide-react";
import clsx from "clsx";
import {
  useGetMyReview,
  useGetReviews,
  useGetReviewStats,
  type Review,
} from "@/entities/review";
import { useAuthStore } from "@/features/auth";
import { ReviewFormModal, useDeleteReview } from "@/features/review/manage-review";
import { confirmDelete } from "@/shared/lib/confirmDelete";
import { ReviewCard } from "./ReviewCard";
import styles from "./ReviewsSection.module.css";

type ReviewsSectionProps = {
  advertisementId: number;
  sellerId?: number;
};

export default function ReviewsSection({ advertisementId, sellerId }: ReviewsSectionProps) {
  const navigate = useNavigate();
  // Користувача кладе в стор App (через useMe). Окремий useMe тут для гостя
  // перезапитував би 401 і перемонтовував застосунок по колу.
  const me = useAuthStore((state) => state.user);
  const [grade, setGrade] = useState<number>();
  const [modal, setModal] = useState<{ review: Review | null } | null>(null);

  const { data: stats } = useGetReviewStats(advertisementId);
  const { data: myReview } = useGetMyReview(advertisementId, me?.id);
  const reviews = useGetReviews(advertisementId, grade);
  const deleteReview = useDeleteReview(advertisementId);

  const items = reviews.data?.pages.flatMap((page) => page.items) ?? [];
  const isSeller = me?.id === sellerId;

  const handleDelete = (review: Review) =>
    confirmDelete({
      title: "Видалити відгук",
      message: "Ви дійсно бажаєте видалити свій відгук? Цю дію неможливо скасувати.",
      onConfirm: () => deleteReview.mutate(review.id),
    });

  const renderAction = () => {
    if (!me)
      return (
        <Button variant="light" onClick={() => navigate("/authentication")}>
          Увійдіть, щоб залишити відгук
        </Button>
      );
    if (isSeller) return null;
    if (myReview)
      return (
        <Button variant="light" onClick={() => setModal({ review: myReview })}>
          Редагувати мій відгук
        </Button>
      );
    return (
      <Button bg="var(--color-base)" onClick={() => setModal({ review: null })}>
        Написати відгук
      </Button>
    );
  };

  const renderList = () => {
    if (reviews.isLoading)
      return Array.from({ length: 3 }, (_, i) => (
        <Skeleton key={i} height={110} radius="md" />
      ));

    if (reviews.isError)
      return <Text c="red">Не вдалося завантажити відгуки.</Text>;

    if (items.length === 0)
      return (
        <Text c="dimmed">
          {grade
            ? "Відгуків з такою оцінкою немає."
            : "Відгуків ще немає. Будьте першим, хто поділиться враженнями!"}
        </Text>
      );

    return items.map((review) => (
      <ReviewCard
        key={review.id}
        review={review}
        advertisementId={advertisementId}
        sellerId={sellerId}
        currentUserId={me?.id}
        onEdit={(r) => setModal({ review: r })}
        onDelete={handleDelete}
      />
    ));
  };

  const filters = [
    { value: undefined, label: "Усі", count: stats.total },
    ...stats.distribution.map((d) => ({ value: d.grade, label: String(d.grade), count: d.count })),
  ];

  return (
    <div className={styles.container}>
      <aside className={styles.filterSidebar}>
        <Text fw={700} size="lg" c="#0f172a">
          Фільтр за оцінкою
        </Text>
        <div className={styles.filterList}>
          {filters.map((filter) => (
            <UnstyledButton
              key={filter.label}
              className={clsx(styles.filterItem, grade === filter.value && styles.filterActive)}
              onClick={() => setGrade(filter.value)}
              disabled={filter.value !== undefined && filter.count === 0}
            >
              <span className={styles.ratingLabel}>
                {filter.value && <Star size={14} fill="#fea945" color="#fea945" />}
                {filter.label}
              </span>
              <span className={styles.filterCount}>{filter.count}</span>
            </UnstyledButton>
          ))}
        </div>
      </aside>

      <main className={styles.reviewsContent}>
        <div className={styles.contentHeader}>
          <Text fw={700} size="xl" c="#0f172a">
            Відгуки покупців
          </Text>
          {renderAction()}
        </div>

        <div className={styles.reviewsList}>{renderList()}</div>

        {reviews.hasNextPage && (
          <Button
            variant="default"
            onClick={() => reviews.fetchNextPage()}
            loading={reviews.isFetchingNextPage}
          >
            Показати ще
          </Button>
        )}
      </main>

      <ReviewFormModal
        advertisementId={advertisementId}
        review={modal?.review}
        opened={modal !== null}
        onClose={() => setModal(null)}
      />
    </div>
  );
}
