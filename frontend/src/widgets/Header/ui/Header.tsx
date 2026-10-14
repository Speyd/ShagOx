import Container from "@/shared/ui/container";
import styles from "./Header.module.css";
import { Bell, Heart, Menu, Scale, ShoppingBag, User } from "lucide-react";
import { Divider, Image, Text } from "@mantine/core";
import logo from "@/shared/assets/icons/logo.png";
import SearchInput from "@/shared/ui/search-input/SearchInput";
import { useDisclosure } from "@mantine/hooks";
import { Link, useLocation } from "react-router-dom";
import CitySelector from "@/shared/ui/city-selector";
import NavigationDrawer from "./NavigationDrawer";
import CartDrawer from "./BasketDrawer";

interface ActionItem {
  id: string;
  label: string;
  to?: string;
  icon: React.ComponentType<{ className?: string; fill?: string }>;
  isCart?: boolean;
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
  { id: "cart", label: "Кошик", icon: ShoppingBag, isCart: true },
];

export default function Header() {
  const [menuOpened, { open, close }] = useDisclosure(false);
  const [cartOpened, { open: openCart, close: closeCart }] =
    useDisclosure(false);

  const location = useLocation();
  const isFavoritesActive = location.pathname.startsWith("/profile/favorites");

  return (
    <header className={styles.header}>
      <div className={styles.headerTop}>
        <Container>
          <div className={styles.headerTopContent}>
            <CitySelector />
            <div className={styles.headerInfo}>
              <Text>Питання</Text>
              <Divider
                orientation="vertical"
                h={19}
                size={2}
                style={{ alignSelf: "center" }}
              />
              <Text>Повернення</Text>
            </div>
          </div>
        </Container>
      </div>

      <div className={styles.headerBottom}>
        <Container>
          <div className={styles.headerBottomContent}>
            <Menu
              size={50}
              strokeWidth={1.5}
              className="icon iconLarge"
              onClick={open}
            />
            <NavigationDrawer opened={menuOpened} onClose={close} />

            <Link to="/" className={styles.headerLogo}>
              <Image src={logo} alt="ShagOx" w={55} />
              <Text c="black" fw={600} fz={30}>
                Marketly
              </Text>
            </Link>

            <SearchInput />

            <div className={styles.headerActions}>
              {ACTION_ITEMS.map((item) => {
                const Icon = item.icon;
                const isFavorite = item.id === "favorites";
                const iconFill =
                  isFavorite && isFavoritesActive ? "currentColor" : "none";

                if (item.isCart) {
                  return (
                    <div
                      key={item.id}
                      className={styles.iconContainer}
                      onClick={openCart}
                    >
                      <Icon className="icon iconLarge" />
                      <Text className={styles.iconContainerText}>
                        {item.label}
                      </Text>
                    </div>
                  );
                }

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
            </div>

            <Link to="/profile" className={styles.iconContainer}>
              <User className="icon iconLarge" />
              <Text className={styles.iconContainerText}>Профіль</Text>
            </Link>

            <CartDrawer opened={cartOpened} onClose={closeCart} />
          </div>
        </Container>
      </div>
    </header>
  );
}
