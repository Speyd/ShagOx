import { Badge, Paper, Tabs, Text } from "@mantine/core";
import { useEffect, useMemo, useState } from "react";
import { Link, useOutletContext } from "react-router-dom";
import type { ProfileContextType } from "@/app/layouts/ProfileLayout";
import { EmptyState } from "@/shared/ui/data-display/empty-state/EmptyState";
import { useMyOrders, useMySales } from "@/entities/order/model/hooks";
import type { Order, OrderStatus } from "@/entities/order/model/types";
import { formatMoney, formatOrderDate } from "@/entities/order/lib/labels";
import { getOrderDisplayStatus } from "@/entities/order/lib/payment";
import AppPagination from "@/shared/ui/navigation/pagination";
import styles from "./OrdersTab.module.css";

type Mode = "purchases" | "sales";

type TabDef = {
  value: string;
  label: string;
  statuses?: OrderStatus[];
};

const PAGE_SIZE = 4;

const PURCHASE_TABS: TabDef[] = [
  { value: "all", label: "Усі замовлення" },
  {
    value: "processing",
    label: "В обробці",
    statuses: ["Pending", "Confirmed", "Processing", "ReadyForShipment"],
  },
  { value: "sent", label: "Відправлені", statuses: ["Shipped", "InTransit"] },
  {
    value: "delivered",
    label: "Доставлені",
    statuses: ["Delivered", "Completed"],
  },
  {
    value: "cancelled",
    label: "Скасовані",
    statuses: ["Cancelled", "Rejected", "Refunded"],
  },
];

// Продавцю важливо, що потребує його дії: нові — підтвердити, в роботі — відправити.
const SALES_TABS: TabDef[] = [
  { value: "new", label: "Нові", statuses: ["Pending"] },
  {
    value: "work",
    label: "До відправки",
    statuses: ["Confirmed", "Processing"],
  },
  {
    value: "sent",
    label: "Відправлені",
    statuses: ["ReadyForShipment", "Shipped", "InTransit", "Delivered"],
  },
  { value: "done", label: "Завершені", statuses: ["Completed"] },
  {
    value: "cancelled",
    label: "Скасовані",
    statuses: ["Cancelled", "Rejected", "Refunded"],
  },
  { value: "all", label: "Усі" },
];

const TEXTS: Record<Mode, { title: string; empty: string }> = {
  purchases: {
    title: "Мої замовлення",
    empty: "У вас поки що немає замовлень.",
  },
  sales: {
    title: "Мої продажі",
    empty: "Тут з'являться замовлення ваших товарів.",
  },
};

export default function OrdersTab() {
  return <OrdersView mode="purchases" />;
}

export function SalesTab() {
  return <OrdersView mode="sales" />;
}

function OrdersView({ mode }: { mode: Mode }) {
  const { setOrientation } = useOutletContext<ProfileContextType>();
  const tabs = mode === "sales" ? SALES_TABS : PURCHASE_TABS;
  const [activeTab, setActiveTab] = useState(tabs[0].value);
  const [page, setPage] = useState(1);

  const purchases = useMyOrders();
  const sales = useMySales(mode === "sales");
  const { data: orders = [], isLoading } = mode === "sales" ? sales : purchases;

  useEffect(() => {
    setOrientation("row");
  }, [setOrientation]);

  const counts = useMemo(() => {
    const result: Record<string, number> = {};
    for (const tab of tabs) {
      result[tab.value] = tab.statuses
        ? orders.filter((order) => tab.statuses!.includes(order.orderStatus))
            .length
        : orders.length;
    }
    return result;
  }, [orders, tabs]);

  const statuses = tabs.find((tab) => tab.value === activeTab)?.statuses;
  const filteredOrders = statuses
    ? orders.filter((order) => statuses.includes(order.orderStatus))
    : orders;

  const totalPages = Math.max(1, Math.ceil(filteredOrders.length / PAGE_SIZE));
  const paginatedOrders = filteredOrders.slice(
    (page - 1) * PAGE_SIZE,
    page * PAGE_SIZE,
  );

  const handleTabChange = (value: string | null) => {
    setActiveTab(value ?? tabs[0].value);
    setPage(1);
  };

  const handlePageChange = (nextPage: number) => {
    setPage(nextPage);
    window.scrollTo({ top: 0, behavior: "smooth" });
  };

  return (
    <Paper p="xl" radius="lg" className={styles.profileOrders}>
      <Text className={styles.title}>{TEXTS[mode].title}</Text>
      <Tabs value={activeTab} onChange={handleTabChange}>
        <Tabs.List className={styles.list}>
          {tabs.map((tab) => (
            <Tabs.Tab key={tab.value} value={tab.value} className={styles.tab}>
              {tab.label}
              {mode === "sales" && tab.value === "new" && counts.new > 0 && (
                <span className={styles.counter}>{counts.new}</span>
              )}
            </Tabs.Tab>
          ))}
        </Tabs.List>
      </Tabs>

      {isLoading && <Text c="dimmed">Завантаження...</Text>}

      {!isLoading && filteredOrders.length === 0 && (
        <EmptyState variant="dashed" description={TEXTS[mode].empty} />
      )}

      <div className={styles.orders}>
        {paginatedOrders.map((order) => (
          <OrderCard key={order.id} order={order} mode={mode} />
        ))}

        {totalPages > 1 && (
          <AppPagination
            page={page}
            totalPages={totalPages}
            onPageChange={handlePageChange}
          />
        )}
      </div>
    </Paper>
  );
}

function OrderCard({ order, mode }: { order: Order; mode: Mode }) {
  const status = getOrderDisplayStatus(order);
  const needsAction = mode === "sales" && order.orderStatus === "Pending";

  return (
    <Link
      to={`/orders/${order.id}`}
      className={`${styles.order} ${needsAction ? styles.orderHighlighted : ""}`}
    >
      <div className={styles.orderHeader}>
        <div>
          <Text fw={600} fz={15}>
            № {order.orderNumber}
          </Text>
          <Text c="dimmed" fz={13}>
            {formatOrderDate(order.createdAt)}
            {mode === "sales" && ` · ${order.firstName} ${order.lastName}`}
          </Text>
        </div>
        <Badge color={status.color} variant="light">
          {needsAction ? "Потребує підтвердження" : status.label}
        </Badge>
      </div>

      <div className={styles.orderFooter}>
        <div className={styles.pictures}>
          {order.orderItems
            .slice(0, 4)
            .map((item) =>
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
  );
}
