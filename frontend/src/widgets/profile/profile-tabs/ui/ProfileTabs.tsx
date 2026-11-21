import { NavLink } from "react-router-dom";
import styles from "./ProfileTabs.module.css";
import { Text } from "@mantine/core";
import { useMySales } from "@/entities/order/model/hooks";

const TABS = [
  { label: "Профіль", to: "/profile" },
  { label: "Обране", to: "/profile/favorites" },
  { label: "Замовлення", to: "/profile/orders" },
  { label: "Продажі", to: "/profile/sales" },
  { label: "Інформаційна панель", to: "/profile/statistics" },
];

export default function ProfileTabs() {
  // Скільки нових замовлень чекають на підтвердження продавцем.
  const { data: sales } = useMySales();
  const newSales =
    sales?.filter((order) => order.orderStatus === "Pending").length ?? 0;

  return (
    <nav className={styles.tabsNav}>
      {TABS.map((tab) => (
        <NavLink
          key={tab.to}
          to={tab.to}
          end={tab.to === "/profile"}
          className={({ isActive }) =>
            `${styles.tabBtn} ${isActive ? styles.active : ""}`
          }
        >
          <Text className={styles.label}>
            {tab.label}
            {tab.to === "/profile/sales" && newSales > 0 && (
              <span className={styles.counter}>{newSales}</span>
            )}
          </Text>
        </NavLink>
      ))}
    </nav>
  );
}
