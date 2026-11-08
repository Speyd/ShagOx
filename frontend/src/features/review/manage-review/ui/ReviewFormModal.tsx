import { Modal } from "@mantine/core";
import type { Review } from "@/entities/review";
import { useCreateReview, useUpdateReview } from "../model/hooks";
import type { ReviewFormValues } from "../model/schema";
import { ReviewForm } from "./ReviewForm";

type ReviewFormModalProps = {
  advertisementId: number;
  /** Передайте відгук для редагування; без нього форма створює новий */
  review?: Review | null;
  opened: boolean;
  onClose: () => void;
};

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
      update.mutate({ id: review.id, text: values.text, grade: values.grade }, options);
    } else {
      create.mutate({ advertisementId, ...values }, options);
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
      {/* key скидає форму при перемиканні між створенням і редагуванням */}
      <ReviewForm
        key={review?.id ?? "new"}
        defaultValues={review ? { grade: review.grade, text: review.text } : undefined}
        withPhotos={!isEdit}
        submitLabel={isEdit ? "Зберегти" : "Опублікувати"}
        isPending={create.isPending || update.isPending}
        onSubmit={handleSubmit}
        onCancel={onClose}
      />
    </Modal>
  );
}
