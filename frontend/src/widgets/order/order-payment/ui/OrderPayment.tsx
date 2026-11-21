import { useState, type ReactNode } from "react";
import { Badge, Loader, Text } from "@mantine/core";
import { useQueryClient } from "@tanstack/react-query";
import {
  CircleAlert,
  CircleCheck,
  Clock,
  CreditCard,
  RotateCcw,
  type LucideIcon,
} from "lucide-react";
import clsx from "clsx";
import Button from "@/shared/ui/buttons/button";
import { useCountdown } from "@/shared/lib/hooks/useCountdown";
import { formatMoney } from "@/entities/order/lib/labels";
import {
  formatCountdown,
  formatDateTime,
  getOrderPaymentState,
  type OrderPaymentState,
} from "@/entities/order/lib/payment";
import { orderQueryKey } from "@/entities/order/model/hooks";
import type { Order } from "@/entities/order/model/types";
import { usePayOrder } from "@/features/order/pay-order";
import styles from "./OrderPayment.module.css";

/** Скільки чекаємо вебхук банку після повернення зі сторінки оплати. */
const PAYMENT_CHECK_WINDOW = 60_000;

type Tone = "warning" | "danger" | "success" | "neutral";

type OrderPaymentProps = {
  order: Order;
  /** Користувач щойно повернувся зі сторінки оплати банку. */
  returnedFromPayment?: boolean;
};

/**
 * Картка оплати: з'являється, лише коли від користувача потрібна дія
 * (оплатити, оплатити повторно) або щоб підтвердити щойно здійснену оплату.
 */
export function OrderPayment({
  order,
  returnedFromPayment = false,
}: OrderPaymentProps) {
  const queryClient = useQueryClient();
  const payOrder = usePayOrder(order.id);

  const [checkUntil] = useState(() =>
    returnedFromPayment ? new Date(Date.now() + PAYMENT_CHECK_WINDOW) : null,
  );
  const checkingLeft = useCountdown(checkUntil);

  // Перераховується щосекунди: useCountdown викликає ререндер.
  const state = getOrderPaymentState(order);
  const linkExpiresAt = state.kind === "awaiting" ? state.expiresAt : null;

  // Коли посилання закінчилося — оновлюємо замовлення з сервера.
  const linkLeft = useCountdown(linkExpiresAt, () =>
    queryClient.invalidateQueries({ queryKey: orderQueryKey(order.id) }),
  );

  const total = formatMoney(order.totalPrice, order.currency.symbol);
  const isRedirecting = payOrder.isPending || payOrder.isSuccess;

  const payButton = (label: string, icon: LucideIcon = CreditCard) => (
    <Button
      leftSection={icon}
      loading={isRedirecting}
      onClick={() => payOrder.mutate()}
    >
      {label}
    </Button>
  );

  if (state.kind === "awaiting" && checkingLeft > 0) {
    return (
      <PaymentCard
        tone="neutral"
        icon={<Loader size={28} />}
        title="Перевіряємо оплату…"
        description="Зазвичай це займає кілька секунд — сторінка оновиться автоматично."
        action={
          state.expiresAt && (
            <Button
              variant="outline"
              loading={isRedirecting}
              onClick={() => payOrder.mutate()}
            >
              Повернутися до оплати
            </Button>
          )
        }
      />
    );
  }

  switch (state.kind) {
    case "awaiting":
      return state.expiresAt ? (
        <PaymentCard
          tone="warning"
          icon={<Clock size={28} />}
          title="Замовлення очікує оплати"
          description={
            <>
              Посилання на оплату дійсне ще{" "}
              <span className={styles.timer}>{formatCountdown(linkLeft)}</span>.
              Товари зарезервовано за вами до завершення цього часу.
            </>
          }
          action={payButton(`Оплатити ${total}`)}
        />
      ) : (
        <PaymentCard
          tone="warning"
          icon={<Clock size={28} />}
          title="Замовлення очікує оплати"
          description="Термін дії посилання на оплату минув. Натисніть «Оплатити» — і ми сформуємо нове."
          action={payButton(`Оплатити ${total}`)}
        />
      );

    case "failed":
      return state.retryUntil ? (
        <PaymentCard
          tone="danger"
          icon={<CircleAlert size={28} />}
          title="Оплату не отримано"
          description={
            <>
              Замовлення скасовано, а товари повернуто у продаж. Ви можете
              оплатити його повторно до{" "}
              <b>{formatDateTime(state.retryUntil)}</b>, якщо товари ще будуть у
              наявності.
            </>
          }
          action={payButton("Оплатити повторно", RotateCcw)}
        />
      ) : (
        <PaymentCard
          tone="neutral"
          icon={<CircleAlert size={28} />}
          title="Оплату не отримано"
          description="Термін для повторної оплати минув. Щоб придбати ці товари, оформіть нове замовлення."
        />
      );

    case "paid":
      return returnedFromPayment ? (
        <PaymentCard
          tone="success"
          icon={<CircleCheck size={28} />}
          title="Оплату отримано"
          description="Дякуємо! Ми вже готуємо ваше замовлення до відправки."
        />
      ) : null;

    default:
      return null;
  }
}

type PaymentCardProps = {
  tone: Tone;
  icon: ReactNode;
  title: string;
  description: ReactNode;
  action?: ReactNode;
};

function PaymentCard({
  tone,
  icon,
  title,
  description,
  action,
}: PaymentCardProps) {
  return (
    <div className={clsx(styles.card, styles[tone])} role="status">
      <div className={styles.icon}>{icon}</div>
      <div className={styles.body}>
        <Text fw={700} fz={18}>
          {title}
        </Text>
        <Text fz={14} className={styles.description}>
          {description}
        </Text>
      </div>
      {action && <div className={styles.action}>{action}</div>}
    </div>
  );
}

/** Короткий статус оплати для блоку «Оплата» в деталях замовлення. */
export function PaymentStatusBadge({ state }: { state: OrderPaymentState }) {
  const badge = getBadge(state);
  if (!badge) return null;

  return (
    <Badge
      color={badge.color}
      variant="light"
      size="sm"
      className={styles.badge}
    >
      {badge.label}
    </Badge>
  );
}

function getBadge(state: OrderPaymentState) {
  switch (state.kind) {
    case "paid":
      return { label: "Оплачено", color: "green" };
    case "awaiting":
      return { label: "Очікує оплати", color: "orange" };
    case "failed":
      return { label: "Не оплачено", color: "red" };
    case "refunded":
      return { label: "Кошти повернено", color: "gray" };
    default:
      return null;
  }
}
