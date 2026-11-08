import { useMutation, useQueryClient } from "@tanstack/react-query";
import { toast } from "sonner";
import { reviewKeys } from "@/entities/review";
import { getErrorMessage } from "@/shared/api/getErrorMessage";
import { createReply, deleteReply, updateReply } from "../api/api";

/**
 * Мутації відповідей на конкретний відгук. Після змін оновлюємо самі відповіді
 * та відгуки оголошення (лічильник repliesCount).
 */
export function useReplyMutations(reviewId: number, advertisementId: number) {
  const queryClient = useQueryClient();

  const invalidate = () =>
    Promise.all([
      queryClient.invalidateQueries({ queryKey: reviewKeys.replies(reviewId) }),
      queryClient.invalidateQueries({
        queryKey: reviewKeys.byAdvertisement(advertisementId),
      }),
    ]);

  const onError = (fallback: string) => (error: unknown) =>
    toast.error(getErrorMessage(error, fallback));

  const create = useMutation({
    mutationFn: createReply,
    onSuccess: invalidate,
    onError: onError("Не вдалося надіслати відповідь"),
  });

  const update = useMutation({
    mutationFn: updateReply,
    onSuccess: invalidate,
    onError: onError("Не вдалося змінити відповідь"),
  });

  const remove = useMutation({
    mutationFn: deleteReply,
    onSuccess: invalidate,
    onError: onError("Не вдалося видалити відповідь"),
  });

  return { create, update, remove };
}
