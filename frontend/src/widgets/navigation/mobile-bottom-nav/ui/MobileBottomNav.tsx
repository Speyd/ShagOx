import { NavLink, useLocation } from "react-router-dom";
import { Home, LayoutGrid, Heart, User } from "lucide-react";
import { Indicator } from "@mantine/core";
import clsx from "clsx";
import styles from "./MobileBottomNav.module.css";

export function MobileBottomNav() {
  const { pathname } = useLocation();
  const favoritesCount = 0;

  const navItems = [
    {
      to: "/",
      label: "Головна",
      icon: Home,
      isActive: pathname === "/",
    },
    {
      to: "/catalog",
      label: "Каталог",
      icon: LayoutGrid,
      isActive: pathname.startsWith("/catalog"),
    },
    {
      to: "/profile/favorites",
      label: "Обране",
      icon: Heart,
      badge: favoritesCount,
      isActive: pathname.startsWith("/profile/favorites"),
    },
    {
      to: "/profile",
      label: "Профіль",
      icon: User,
      isActive:
        pathname.startsWith("/profile") &&
        !pathname.startsWith("/profile/favorites"),
    },
  ];

  return (
    <nav className={styles.bottomNav}>
      {navItems.map(({ to, label, icon: Icon, badge, isActive }) => (
        <NavLink
          key={to}
          to={to}
          className={clsx(styles.navItem, isActive && styles.active)}
        >
          <Indicator
            label={badge}
            size={16}
            disabled={!badge || badge <= 0}
            color="var(--color-base)"
            offset={2}
          >
            <Icon size={22} strokeWidth={2} />
          </Indicator>
          <span className={styles.label}>{label}</span>
        </NavLink>
      ))}
    </nav>
  );
}
