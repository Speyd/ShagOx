import { Modal } from "@mantine/core";
import type { Review } from "@/entities/review";
import type { ReviewPictureChange } from "../api/api";
import { useCreateReview, useUpdateReview } from "../model/hooks";
import type { ReviewFormValues } from "../model/schema";
import { ReviewForm } from "./ReviewForm";

type ReviewFormModalProps = {
  advertisementId: number;
  review?: Review | null;
  opened: boolean;
  onClose: () => void;
};

function toPictureChanges(
  review: Review,
  values: ReviewFormValues,
): ReviewPictureChange[] | undefined {
  const original = [...review.pictures].sort((a, b) => a.order - b.order);
  const kept = original.filter((picture) =>
    values.keptPictureIds.includes(picture.id),
  );
  const removed = original.filter(
    (picture) => !values.keptPictureIds.includes(picture.id),
  );

  if (removed.length === 0 && values.pictures.length === 0) return undefined;

  return [
    ...removed.map((picture) => ({
      id: picture.id,
      order: 0,
      isDeleted: true,
    })),
    ...kept.map((picture, order) => ({
      id: picture.id,
      order,
      isDeleted: false,
    })),
    ...values.pictures.map((file, index) => ({
      file,
      order: kept.length + index,
      isDeleted: false,
    })),
  ];
}

export function ReviewFormModal({
  advertisementId,
  review,
  opened,
  onClose,
}: ReviewFormModalProps) {
  const create = useCreateReview(advertisementId);
  const update = useUpdateReview(advertisementId);
  const isEdit = Boolean(review);

  const handleSubmit = (values: ReviewFormValues) => {
    const options = { onSuccess: onClose };

    if (review) {
      update.mutate(
        {
          id: review.id,
          text: values.text,
          grade: values.grade,
          pictures: toPictureChanges(review, values),
        },
        options,
      );
    } else {
      create.mutate(
        {
          advertisementId,
          text: values.text,
          grade: values.grade,
          pictures: values.pictures,
        },
        options,
      );
    }
  };

  return (
    <Modal
      opened={opened}
      onClose={onClose}
      title={isEdit ? "Редагувати відгук" : "Новий відгук"}
      size="lg"
      centered
    >
      <ReviewForm
        key={review?.id ?? "new"}
        defaultValues={review ? { grade: review.grade, text: review.text } : undefined}
        existingPictures={review?.pictures ?? []}
        submitLabel={isEdit ? "Зберегти" : "Опублікувати"}
        isPending={create.isPending || update.isPending}
        onSubmit={handleSubmit}
        onCancel={onClose}
      />
    </Modal>
  );
}
