import Container from "@/shared/ui/layout/container";
import styles from "./Header.module.css";
import {
  Bell,
  Heart,
  LayoutGrid,
  Menu,
  Scale,
  ShieldCheck,
  ShoppingCart,
  User,
} from "lucide-react";
import { Divider, Image, Text } from "@mantine/core";
import logo from "@/shared/assets/icons/logo.png";
import SearchInput from "@/shared/ui/data-display/inputs/search-input/SearchInput";
import { useDisclosure, useElementSize } from "@mantine/hooks";
import { Link, useLocation, useNavigate } from "react-router-dom";

import NavigationDrawer from "./NavigationDrawer";
import CartDrawer from "./BasketDrawer";
import Button from "@/shared/ui/buttons/button";
import { CitySelector } from "@/entities/city";
import { useAuthStore } from "@/features/auth";
import { hasRole } from "@/shared/lib/auth";
import { useBasketItemsCount } from "@/features/basket/model/hooks/useBasketItemsCount";

interface ActionItem {
  id: string;
  label: string;
  to?: string;
  icon: React.ComponentType<{ className?: string; fill?: string }>;
}

const ACTION_ITEMS: ActionItem[] = [
  { id: "compare", label: "Порівняння", to: "/compare", icon: Scale },
  { id: "favorites", label: "Обране", to: "/profile/favorites", icon: Heart },
  {
    id: "notifications",
    label: "Сповіщення",
    to: "/notifications",
    icon: Bell,
  },
  { id: "profile", label: "Профіль", to: "/profile", icon: User },
];

export default function Header() {
  const [menuOpened, { open, close }] = useDisclosure(false);
  const [cartOpened, { open: openCart, close: closeCart }] =
    useDisclosure(false);

  const { ref, height } = useElementSize();
  const navigate = useNavigate();

  const location = useLocation();
  const isFavoritesActive = location.pathname.startsWith("/profile/favorites");
  const isAdmin = useAuthStore((state) => hasRole(state.user, "Admin"));
  const basketCount = useBasketItemsCount();
  const basketBadge =
    basketCount > 0 ? (
      <span className={styles.badge}>
        {basketCount > 99 ? "99+" : basketCount}
      </span>
    ) : null;

  return (
    <header
      className={styles.header}
      ref={ref}
      style={{ "--header-height": `${height}px` } as React.CSSProperties}
    >
      <div className={styles.headerTop}>
        <Container>
          <div className={styles.headerTopContent}>
            <CitySelector />
            <div className={styles.headerInfo}>
              <Text className={styles.headerInfoText}>Питання</Text>
              <Divider
                orientation="vertical"
                h={16}
                size={1}
                color="rgba(255, 255, 255, 0.3)"
                style={{ alignSelf: "center" }}
              />
              <Text className={styles.headerInfoText}>Повернення</Text>
            </div>
          </div>
        </Container>
      </div>

      <div className={styles.headerBottom}>
        <Container>
          <div className={styles.headerBottomContent}>
            <button
              type="button"
              className={styles.menuButton}
              onClick={open}
              aria-label="Відкрити меню"
            >
              <Menu size={26} strokeWidth={1.75} />
            </button>
            <NavigationDrawer opened={menuOpened} onClose={close} />

            <Link to="/" className={styles.headerLogo}>
              <Image src={logo} alt="Marketly" className={styles.logoImage} />
              <Text c="black" fw={600} className={styles.logoText}>
                Marketly
              </Text>
            </Link>

            <Button
              variant="outline"
              className={styles.catalogButton}
              onClick={() => navigate("/catalog")}
              leftSection={LayoutGrid}
              iconProps={{ size: 20 }}
              hideTextOnMobile
            >
              <Text
                fz={15}
                fw={600}
                inherit
                className={styles.catalogButtonText}
              >
                Каталог
              </Text>
            </Button>

            <SearchInput />

            <div className={styles.headerActions}>
              {isAdmin && (
                <Link to="/admin" className={styles.iconContainer}>
                  <ShieldCheck className="icon iconLarge" />
                  <Text className={styles.iconContainerText}>Адмінка</Text>
                </Link>
              )}

              {ACTION_ITEMS.map((item) => {
                const Icon = item.icon;
                const isFavorite = item.id === "favorites";
                const iconFill =
                  isFavorite && isFavoritesActive ? "currentColor" : "none";

                return (
                  <Link
                    key={item.id}
                    to={item.to!}
                    className={styles.iconContainer}
                  >
                    <Icon className="icon iconLarge" fill={iconFill} />
                    <Text className={styles.iconContainerText}>
                      {item.label}
                    </Text>
                  </Link>
                );
              })}

              <div onClick={openCart} className={styles.iconContainer}>
                <span className={styles.iconWrapper}>
                  <ShoppingCart className="icon iconLarge" />
                  {basketBadge}
                </span>
                <Text className={styles.iconContainerText}>Кошик</Text>
              </div>
            </div>

            <button
              type="button"
              className={styles.mobileCartButton}
              onClick={openCart}
              aria-label="Кошик"
            >
              <span className={styles.iconWrapper}>
                <ShoppingCart size={24} />
                {basketBadge}
              </span>
            </button>

            <CartDrawer opened={cartOpened} onClose={closeCart} />
          </div>
        </Container>
      </div>
    </header>
  );
}
