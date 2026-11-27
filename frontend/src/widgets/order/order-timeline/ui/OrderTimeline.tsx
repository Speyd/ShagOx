import { Anchor, Text, Timeline } from "@mantine/core";
import {
  CircleCheck,
  CircleX,
  CreditCard,
  PackageCheck,
  ShoppingBag,
  Truck,
} from "lucide-react";
import type { ReactNode } from "react";
import { formatDateTime } from "@/entities/order/lib/payment";
import type { Order } from "@/entities/order/model/types";
import styles from "./OrderTimeline.module.css";

type Step = {
  key: string;
  title: string;
  icon: ReactNode;
  date: string | null;
  done: boolean;
  description?: ReactNode;
  danger?: boolean;
};

const toDate = (value: string | null) =>
  value ? formatDateTime(new Date(value)) : null;

const ICON_SIZE = 14;

function buildSteps(order: Order): Step[] {
  const status = order.orderStatus;
  const isPickup = order.deliveryType === "Pickup";
  const isOnline = order.paymentMethod === "Online";
  const closed = ["Cancelled", "Rejected", "Refunded"].includes(status);

  const shippedStatuses = [
    "ReadyForShipment",
    "Shipped",
    "InTransit",
    "Delivered",
    "Completed",
  ];
  const isShipped = shippedStatuses.includes(status);
  const isConfirmed = status === "Confirmed" || isShipped;
  const isCompleted = status === "Completed";

  const steps: Step[] = [
    {
      key: "created",
      title: "Замовлення оформлено",
      icon: <ShoppingBag size={ICON_SIZE} />,
      date: order.createdAt,
      done: true,
    },
  ];

  if (isOnline) {
    steps.push({
      key: "paid",
      title: "Оплачено онлайн",
      icon: <CreditCard size={ICON_SIZE} />,
      date: order.paidAt,
      done:
        order.paymentStatus === "Paid" || order.paymentStatus === "Refunded",
    });
  }

  steps.push({
    key: "confirmed",
    title: "Підтверджено продавцем",
    icon: <CircleCheck size={ICON_SIZE} />,
    date: order.confirmedAt,
    done: isConfirmed,
  });

  steps.push({
    key: "shipped",
    title: isPickup ? "Готове до видачі" : "Відправлено",
    icon: <Truck size={ICON_SIZE} />,
    date: order.shippedAt,
    done: isShipped,
    description:
      !isPickup && order.deliveryTrackingNumber ? (
        <>
          ТТН{" "}
          {order.deliveryTrackingUrl ? (
            <Anchor
              href={order.deliveryTrackingUrl}
              target="_blank"
              rel="noreferrer"
              fz={13}
            >
              {order.deliveryTrackingNumber}
            </Anchor>
          ) : (
            order.deliveryTrackingNumber
          )}
        </>
      ) : undefined,
  });

  steps.push({
    key: "completed",
    title: "Отримано",
    icon: <PackageCheck size={ICON_SIZE} />,
    date: order.deliveredAt,
    done: isCompleted,
  });

  if (!closed) return steps;

  const reached = steps.filter((step) => step.done);
  reached.push({
    key: "closed",
    title:
      status === "Rejected" ? "Відхилено продавцем" : "Замовлення скасовано",
    icon: <CircleX size={ICON_SIZE} />,
    date: order.cancelledAt,
    done: true,
    danger: true,
    description:
      order.paymentStatus === "Refunded"
        ? "Кошти повернено на картку покупця."
        : undefined,
  });

  return reached;
}

export function OrderTimeline({ order }: { order: Order }) {
  const steps = buildSteps(order);
  const active = steps.reduce(
    (last, step, index) => (step.done ? index : last),
    0,
  );

  return (
    <Timeline
      active={active}
      bulletSize={26}
      lineWidth={2}
      className={styles.timeline}
    >
      {steps.map((step) => (
        <Timeline.Item
          key={step.key}
          bullet={step.icon}
          color={step.danger ? "red" : undefined}
          title={
            <Text fw={600} fz={15} c={step.done ? undefined : "dimmed"}>
              {step.title}
            </Text>
          }
        >
          {step.done && step.date && (
            <Text c="dimmed" fz={13}>
              {toDate(step.date)}
            </Text>
          )}
          {step.description && (
            <Text c="dimmed" fz={13}>
              {step.description}
            </Text>
          )}
        </Timeline.Item>
      ))}
    </Timeline>
  );
}
