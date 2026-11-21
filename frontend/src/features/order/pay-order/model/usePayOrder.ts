import { useMutation, useQueryClient } from "@tanstack/react-query";
import { toast } from "sonner";
import { payOrder } from "@/entities/order/api/api";
import { getErrorMessage } from "@/shared/api/getErrorMessage";

/**
 * Отримує посилання на оплату (діюче або нове) і переходить на сторінку банку.
 * Якщо попередній платіж прострочено, бекенд сам відновить замовлення
 * та повторно зарезервує товари.
 */
export function usePayOrder(orderId: number) {
  const queryClient = useQueryClient();

  return useMutation({
    mutationFn: () => payOrder(orderId),
    onSuccess: ({ paymentUrl }) => {
      // assign, а не replace: кнопка «Назад» у банку поверне на сторінку замовлення.
      window.location.assign(paymentUrl);
    },
    onError: (error) => {
      toast.error(
        getErrorMessage(
          error,
          "Не вдалося перейти до оплати. Спробуйте ще раз.",
        ),
      );
    },
    onSettled: () => {
      // Стан замовлення міг змінитися (відновлено, скасовано, вже оплачено).
      queryClient.invalidateQueries({ queryKey: ["orders"] });
    },
  });
}
