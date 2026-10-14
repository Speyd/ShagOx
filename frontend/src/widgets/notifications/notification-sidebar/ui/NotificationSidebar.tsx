import { Text } from "@mantine/core";
import {
  Bell,
  Video,
  Package,
  MessageSquare,
  Tag,
  Settings,
} from "lucide-react";
import styles from "./NotificationSidebar.module.css";
import { NavLink } from "react-router-dom";

export type NotificationCategoryType =
  | "all"
  | "videos"
  | "orders"
  | "comments"
  | "discounts";

type CategoryItem = {
  id: NotificationCategoryType;
  label: string;
  icon: React.ReactNode;
  to: string;
};

const categories: CategoryItem[] = [
  {
    id: "all",
    label: "Усі сповіщення",
    icon: <Bell size={18} />,
    to: "/notifications",
  },
  {
    id: "videos",
    label: "Нові відео",
    icon: <Video size={18} />,
    to: "/notifications/videos",
  },
  {
    id: "orders",
    label: "Замовлення",
    icon: <Package size={18} />,
    to: "/notifications/orders",
  },
  {
    id: "comments",
    label: "Коментарі",
    icon: <MessageSquare size={18} />,
    to: "/notifications/comments",
  },
  {
    id: "discounts",
    label: "Зниження ціни",
    icon: <Tag size={18} />,
    to: "/notifications/discounts",
  },
];

interface NotificationSidebarProps {
  onOpenSettings?: () => void;
}

export default function NotificationSidebar({
  onOpenSettings,
}: NotificationSidebarProps) {
  return (
    <div className={styles.sidebar}>
      <div className={styles.categoryList}>
        {categories.map((cat) => (
          <NavLink
            key={cat.id}
            to={cat.to}
            end={cat.to === "/notifications"}
            className={({ isActive }) =>
              `${styles.menuItem} ${isActive ? styles.menuItemActive : ""}`
            }
          >
            <div className={styles.itemLeft}>
              <span className={styles.icon}>{cat.icon}</span>
              <Text fz={14} fw={500} className={styles.label}>
                {cat.label}
              </Text>
            </div>
          </NavLink>
        ))}
      </div>

      <div className="divider" />

      <NavLink
        to="/notifications/settings"
        className={({ isActive }) =>
          `${styles.menuItem} ${isActive ? styles.menuItemActive : ""}`
        }
        onClick={onOpenSettings}
      >
        <span className={styles.icon}>
          <Settings size={18} />
        </span>
        <Text fz={14} fw={600} className={styles.label}>
          Налаштування повідомлень
        </Text>
      </NavLink>
    </div>
  );
}
