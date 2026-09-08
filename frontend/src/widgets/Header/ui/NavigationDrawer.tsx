import {
  Drawer,
  Divider,
  Text,
  Image,
  Collapse,
  Modal,
  Button,
} from "@mantine/core";
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

type NavigationDrawerProps = {
  opened: boolean;
  onClose: () => void;
};

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
            <button className={styles.menuItemActive}>
              <Home />

              <Text fz={14}>Головна</Text>
            </button>

            <button className={styles.menuItem}>
              <LayoutGrid />

              <Text fz={14}>Каталог</Text>

              <ChevronRight className={styles.arrow} />
            </button>

            <button className={styles.menuItem}>
              <Package />

              <Text fz={14}>Мої замовлення</Text>
            </button>

            <button className={styles.menuItem}>
              <Smartphone />

              <Text fz={14}>Мої пристрої</Text>
            </button>

            <button className={styles.menuItem}>
              <Truck />

              <Text fz={14}>Доставка та оплата</Text>
            </button>

            <button className={styles.menuItem}>
              <Shield />

              <Text fz={14}>Відео та огляди</Text>
            </button>

            <button className={styles.menuItem}>
              <Phone />

              <Text fz={14}>Контакти</Text>
            </button>

            <button className={styles.menuItem}>
              <Headphones />

              <Text fz={14}>Підтримка</Text>
            </button>

            <button className={styles.menuItem}>
              <Settings />

              <Text fz={14}>Налаштування</Text>
            </button>
          </nav>
        </div>
        <div className={styles.menuBottom}>
          <div className={styles.languageSection}>
            <Divider />

            <button
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
                <button className={styles.languageItem}>
                  <Text fz={14}>Українська</Text>
                </button>

                <button className={styles.languageItem}>
                  <Text fz={14}>English</Text>
                </button>
              </div>
            </Collapse>

            <Divider />
          </div>
          <button
            className={styles.menuItem}
            onClick={() => setLogoutModalOpened(true)}
          >
            <LogOut />

            <Text fz={14}>Вийти</Text>
          </button>
        </div>
      </div>
      <Modal
        opened={logoutModalOpened}
        onClose={() => setLogoutModalOpened(false)}
        title="Вихід з акаунту"
        centered
      >
        <Text mb="lg">Ви впевнені, що хочете вийти з акаунту?</Text>

        <div className={styles.logoutActions}>
          <Button variant="default" onClick={() => setLogoutModalOpened(false)}>
            Скасувати
          </Button>

          <Button
            color="red"
            onClick={handleLogout}
            loading={logoutMutation.isPending}
          >
            Вийти
          </Button>
        </div>
      </Modal>
    </Drawer>
  );
}
