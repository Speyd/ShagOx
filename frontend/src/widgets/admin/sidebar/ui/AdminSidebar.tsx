import { Link, NavLink } from "react-router-dom";
import { Text } from "@mantine/core";
import {
  ArrowLeft,
  BadgeCheck,
  Boxes,
  CircleDollarSign,
  FolderTree,
  LayoutDashboard,
  ListTree,
  Map as MapIcon,
  MapPin,
  Megaphone,
  MessageSquareText,
  ShieldCheck,
  Sparkles,
  Users,
  type LucideIcon,
} from "lucide-react";
import logo from "@/shared/assets/icons/logo.png";
import styles from "./AdminSidebar.module.css";

type NavItem = { to: string; label: string; icon: LucideIcon; end?: boolean };

const sections: { title: string; items: NavItem[] }[] = [
  {
    title: "Головне",
    items: [{ to: "/admin", label: "Огляд", icon: LayoutDashboard, end: true }],
  },
  {
    title: "Маркетплейс",
    items: [
      { to: "/admin/advertisements", label: "Оголошення", icon: Megaphone },
      { to: "/admin/users", label: "Користувачі", icon: Users },
      { to: "/admin/reviews", label: "Відгуки", icon: MessageSquareText },
    ],
  },
  {
    title: "Каталог",
    items: [
      { to: "/admin/product-types", label: "Типи товарів", icon: Boxes },
      { to: "/admin/categories", label: "Категорії", icon: FolderTree },
      { to: "/admin/dictionaries", label: "Довідники атрибутів", icon: ListTree },
    ],
  },
  {
    title: "Довідники",
    items: [
      { to: "/admin/conditions", label: "Стани товару", icon: Sparkles },
      { to: "/admin/statuses", label: "Статуси оголошень", icon: BadgeCheck },
      { to: "/admin/currencies", label: "Валюти", icon: CircleDollarSign },
    ],
  },
  {
    title: "Локації",
    items: [
      { to: "/admin/regions", label: "Регіони", icon: MapIcon },
      { to: "/admin/cities", label: "Міста", icon: MapPin },
    ],
  },
  {
    title: "Доступ",
    items: [{ to: "/admin/roles", label: "Ролі", icon: ShieldCheck }],
  },
];

type AdminSidebarProps = {
  onNavigate?: () => void;
};

export default function AdminSidebar({ onNavigate }: AdminSidebarProps) {
  return (
    <aside className={styles.sidebar}>
      <Link to="/admin" className={styles.logo} onClick={onNavigate}>
        <img src={logo} alt="" className={styles.logoImage} />
        <span className={styles.logoText}>Marketly</span>
        <span className={styles.badge}>Admin</span>
      </Link>

      <nav className={styles.nav}>
        {sections.map((section) => (
          <div key={section.title} className={styles.section}>
            <Text component="span" className={styles.sectionTitle}>
              {section.title}
            </Text>
            {section.items.map(({ to, label, icon: Icon, end }) => (
              <NavLink
                key={to}
                to={to}
                end={end}
                onClick={onNavigate}
                className={({ isActive }) =>
                  `${styles.link} ${isActive ? styles.active : ""}`
                }
              >
                <Icon size={19} strokeWidth={1.8} />
                {label}
              </NavLink>
            ))}
          </div>
        ))}
      </nav>

      <Link to="/" className={styles.back} onClick={onNavigate}>
        <ArrowLeft size={18} />
        Повернутися на сайт
      </Link>

    </aside>
  );
}
