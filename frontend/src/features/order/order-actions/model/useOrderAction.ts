import { useMutation, useQueryClient } from "@tanstack/react-query";
import { toast } from "sonner";
import {
  cancelOrder,
  completeOrder,
  confirmOrder,
  rejectOrder,
  shipOrder,
} from "@/entities/order/api/api";
import type { OrderAction } from "@/entities/order/lib/actions";
import { getErrorMessage } from "@/shared/api/getErrorMessage";

type Variables = { trackingNumber?: string | null };

const SUCCESS_MESSAGES: Record<OrderAction, string> = {
  cancel: "Замовлення скасовано",
  confirm: "Замовлення підтверджено",
  reject: "Замовлення відхилено",
  ship: "Статус замовлення оновлено",
  complete: "Замовлення завершено",
};

function run(action: OrderAction, orderId: number, vars: Variables) {
  switch (action) {
    case "cancel":
      return cancelOrder(orderId);
    case "confirm":
      return confirmOrder(orderId);
    case "reject":
      return rejectOrder(orderId);
    case "ship":
      return shipOrder(orderId, vars.trackingNumber ?? null);
    case "complete":
      return completeOrder(orderId);
  }
}

/** Виконує дію із замовленням і оновлює всі списки та сторінку замовлення. */
export function useOrderAction(orderId: number, action: OrderAction) {
  const queryClient = useQueryClient();

  return useMutation({
    mutationFn: (vars: Variables = {}) => run(action, orderId, vars),
    onSuccess: () => {
      toast.success(SUCCESS_MESSAGES[action]);
    },
    onError: (error) => {
      toast.error(
        getErrorMessage(error, "Не вдалося змінити статус замовлення."),
      );
    },
    onSettled: () => {
      // Навіть після помилки (наприклад, «статус щойно змінився») —
      // показуємо актуальний стан.
      queryClient.invalidateQueries({ queryKey: ["orders"] });
      // Скасування/відхилення повертає товар на склад.
      if (action === "cancel" || action === "reject") {
        queryClient.invalidateQueries({
          predicate: ({ queryKey }) =>
            typeof queryKey[0] === "string" &&
            /advertisement/i.test(queryKey[0]),
        });
      }
    },
  });
}
