import { Drawer, Divider, Text, Image, Collapse } from "@mantine/core";
import {
  ChevronRight,
  LogOut,
  Headphones,
  Home,
  Languages,
  LayoutGrid,
  Package,
  Phone,
  Settings,
  Shield,
  Smartphone,
  Truck,
  X,
} from "lucide-react";
import styles from "./NavigationDrawer.module.css";
import logo from "@/shared/assets/icons/logo.png";
import { useState } from "react";
import { useLogout } from "@/features/auth/model/hooks/useLogout";
import ConfirmModal from "@/shared/ui/confirm-modal";
import { NavLink } from "react-router-dom"; // 1. Імпортуємо NavLink
import clsx from "clsx"; // 2. Імпортуємо clsx для підключення активних класів

type NavigationDrawerProps = {
  opened: boolean;
  onClose: () => void;
};

const NAV_ITEMS = [
  { to: "/", label: "Головна", icon: Home, end: true },
  { to: "/catalog", label: "Каталог", icon: LayoutGrid },
  { to: "/profile/orders", label: "Мої замовлення", icon: Package },
  { to: "/devices", label: "Мої пристрої", icon: Smartphone },
  { to: "/delivery", label: "Доставка та оплата", icon: Truck },
  { to: "/reviews", label: "Відео та огляди", icon: Shield },
  { to: "/contacts", label: "Контакти", icon: Phone },
  { to: "/support", label: "Підтримка", icon: Headphones },
  { to: "/settings", label: "Налаштування", icon: Settings },
];

export default function NavigationDrawer({
  opened,
  onClose,
}: NavigationDrawerProps) {
  const [languageOpened, setLanguageOpened] = useState(false);
  const [logoutModalOpened, setLogoutModalOpened] = useState(false);
  const logoutMutation = useLogout();

  const handleLogout = () => {
    logoutMutation.mutate();
    setLogoutModalOpened(false);
    onClose();
  };

  return (
    <Drawer
      opened={opened}
      onClose={onClose}
      position="left"
      zIndex={1001}
      size={350}
      withCloseButton={false}
      classNames={{
        content: styles.drawerContent,
        body: styles.drawerBody,
      }}
    >
      <div className={styles.menu}>
        <div className={styles.menuTop}>
          <div className={styles.menuHeader}>
            <button className={styles.closeButton} onClick={onClose}>
              <X size={35} strokeWidth={2.5} className="icon" />
            </button>

            <div className={styles.logo}>
              <Image src={logo} alt="ShagOx" w={40} />
              <Text fw={600} fz={30}>
                Marketly
              </Text>
            </div>
          </div>

          <Divider />

          <nav className={styles.navigation}>
            {NAV_ITEMS.map(({ to, label, icon: Icon, end }) => (
              <NavLink
                key={to}
                to={to}
                end={end} 
                onClick={onClose}
                className={({ isActive }) =>
                  clsx(styles.menuItem, {
                    [styles.menuItemActive]: isActive,
                  })
                }
              >
                <Icon />
                <Text fz={14}>{label}</Text>
              </NavLink>
            ))}
          </nav>
        </div>

        <div className={styles.menuBottom}>
          <div className={styles.languageSection}>
            <Divider />

            <button
              type="button"
              className={styles.menuItem}
              onClick={() => setLanguageOpened((prev) => !prev)}
            >
              <Languages />

              <Text fz={14}>Українська мова</Text>

              <ChevronRight
                className={`${styles.arrow} ${
                  languageOpened ? styles.arrowOpened : ""
                }`}
              />
            </button>

            <Collapse expanded={languageOpened}>
              <div className={styles.languageList}>
                <button type="button" className={styles.languageItem}>
                  <Text fz={14}>Українська</Text>
                </button>

                <button type="button" className={styles.languageItem}>
                  <Text fz={14}>English</Text>
                </button>
              </div>
            </Collapse>

            <Divider />
          </div>

          <button
            type="button"
            className={styles.menuItem}
            onClick={() => setLogoutModalOpened(true)}
          >
            <LogOut />

            <Text fz={14}>Вийти</Text>
          </button>
        </div>
      </div>

      <ConfirmModal
        opened={logoutModalOpened}
        onClose={() => setLogoutModalOpened(false)}
        title="Вихід з акаунту"
        message="Ви впевнені, що хочете вийти з акаунту?"
        confirmLabel="Вийти"
        confirmColor="red"
        onConfirm={handleLogout}
        isLoading={logoutMutation.isPending}
      />
    </Drawer>
  );
}
