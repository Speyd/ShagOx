import { Badge, Paper, Tabs, Text } from "@mantine/core";
import { useEffect, useState } from "react";
import { Link, useOutletContext } from "react-router-dom";
import type { ProfileContextType } from "@/app/layouts/ProfileLayout";
import { EmptyState } from "@/shared/ui/data-display/empty-state/EmptyState";
import { useMyOrders } from "@/entities/order/model/hooks";
import type { OrderStatus } from "@/entities/order/model/types";
import {
  ORDER_STATUS_COLORS,
  ORDER_STATUS_LABELS,
  formatMoney,
  formatOrderDate,
} from "@/entities/order/lib/labels";
import styles from "./OrdersTab.module.css";

type TabValue = "all" | "processing" | "sent" | "delivered" | "cancelled";

const TAB_STATUSES: Record<Exclude<TabValue, "all">, OrderStatus[]> = {
  processing: ["Pending", "Confirmed", "Processing", "ReadyForShipment"],
  sent: ["Shipped", "InTransit"],
  delivered: ["Delivered", "Completed"],
  cancelled: ["Cancelled", "Rejected", "Refunded"],
};

export default function OrdersTab() {
  const { setOrientation } = useOutletContext<ProfileContextType>();
  const [activeTab, setActiveTab] = useState<TabValue>("all");
  const { data: orders = [], isLoading } = useMyOrders();

  useEffect(() => {
    setOrientation("row");
  }, [setOrientation]);

  const visibleOrders =
    activeTab === "all"
      ? orders
      : orders.filter((order) =>
          TAB_STATUSES[activeTab].includes(order.orderStatus),
        );

  return (
    <Paper p="xl" radius="lg" className={styles.profileOrders}>
      <Text className={styles.title}>Мої замовлення</Text>
      <Tabs
        value={activeTab}
        onChange={(value) => setActiveTab((value as TabValue) ?? "all")}
      >
        <Tabs.List className={styles.list}>
          <Tabs.Tab value="all" className={styles.tab}>
            Усі замовлення
          </Tabs.Tab>
          <Tabs.Tab value="processing" className={styles.tab}>
            В обробці
          </Tabs.Tab>
          <Tabs.Tab value="sent" className={styles.tab}>
            Відправлені
          </Tabs.Tab>
          <Tabs.Tab value="delivered" className={styles.tab}>
            Доставлені
          </Tabs.Tab>
          <Tabs.Tab value="cancelled" className={styles.tab}>
            Скасовані
          </Tabs.Tab>
        </Tabs.List>
      </Tabs>

      {isLoading && <Text c="dimmed">Завантаження...</Text>}

      {!isLoading && visibleOrders.length === 0 && (
        <EmptyState
          variant="dashed"
          description="У вас поки що немає замовлень."
        />
      )}

      <div className={styles.orders}>
        {visibleOrders.map((order) => (
          <Link key={order.id} to={`/orders/${order.id}`} className={styles.order}>
            <div className={styles.orderHeader}>
              <div>
                <Text fw={600} fz={15}>
                  № {order.orderNumber}
                </Text>
                <Text c="dimmed" fz={13}>
                  {formatOrderDate(order.createdAt)}
                </Text>
              </div>
              <Badge color={ORDER_STATUS_COLORS[order.orderStatus]} variant="light">
                {ORDER_STATUS_LABELS[order.orderStatus]}
              </Badge>
            </div>

            <div className={styles.orderFooter}>
              <div className={styles.pictures}>
                {order.orderItems.slice(0, 4).map((item) =>
                  item.advertisement.picture?.url ? (
                    <img
                      key={item.id}
                      src={item.advertisement.picture.url}
                      alt={item.advertisement.title}
                      className={styles.picture}
                    />
                  ) : (
                    <div key={item.id} className={styles.picture} />
                  ),
                )}
              </div>
              <Text fw={700} fz={16}>
                {formatMoney(order.totalPrice, order.currency.symbol)}
              </Text>
            </div>
          </Link>
        ))}
      </div>
    </Paper>
  );
}
