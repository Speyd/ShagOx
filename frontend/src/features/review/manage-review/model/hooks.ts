import { useMutation, useQueryClient } from "@tanstack/react-query";
import { toast } from "sonner";
import { reviewKeys } from "@/entities/review";
import { getErrorMessage } from "@/shared/api/getErrorMessage";
import { createReview, deleteReview, updateReview } from "../api/api";

/** Після будь-якої зміни оновлюємо список, зведення та "мій відгук". */
function useInvalidateReviews(advertisementId: number) {
  const queryClient = useQueryClient();
  return () =>
    queryClient.invalidateQueries({
      queryKey: reviewKeys.byAdvertisement(advertisementId),
    });
}

const onError = (fallback: string) => (error: unknown) =>
  toast.error(getErrorMessage(error, fallback));

export function useCreateReview(advertisementId: number) {
  const invalidate = useInvalidateReviews(advertisementId);

  return useMutation({
    mutationFn: createReview,
    onSuccess: () => {
      toast.success("Дякуємо за відгук!");
      return invalidate();
    },
    onError: onError("Не вдалося опублікувати відгук"),
  });
}

export function useUpdateReview(advertisementId: number) {
  const invalidate = useInvalidateReviews(advertisementId);

  return useMutation({
    mutationFn: updateReview,
    onSuccess: () => {
      toast.success("Відгук оновлено");
      return invalidate();
    },
    onError: onError("Не вдалося оновити відгук"),
  });
}

export function useDeleteReview(advertisementId: number) {
  const invalidate = useInvalidateReviews(advertisementId);

  return useMutation({
    mutationFn: deleteReview,
    onSuccess: () => {
      toast.success("Відгук видалено");
      return invalidate();
    },
    onError: onError("Не вдалося видалити відгук"),
  });
}
