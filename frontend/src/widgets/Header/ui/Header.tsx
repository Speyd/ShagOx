import Container from "@/shared/ui/container";
import styles from "./Header.module.css";
import {
  Bell,
  Heart,
  Menu,
  Scale,
  ShoppingBag,
  User,
} from "lucide-react";
import { Divider, Image, Text } from "@mantine/core";
import logo from "@/shared/assets/icons/logo.png";
import SearchInput from "@/shared/ui/search-input/SearchInput";
import { useDisclosure } from "@mantine/hooks";
import NavigationDrawer from "./NavigationDrawer";
import CartDrawer from "./CartDrawer";
import { Link } from "react-router-dom";
import CitySelector from "@/shared/ui/city-selector";

export default function Header() {
  const [menuOpened, { open, close }] = useDisclosure(false);
  const [cartOpened, { open: openCart, close: closeCart }] =
    useDisclosure(false);

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
              <Link to="/compare" className={styles.iconContainer}>
                <Scale className="icon iconLarge" />
                <Text className={styles.iconContainerText}>Порівняння</Text>
              </Link>

              <Link to="/favorite" className={styles.iconContainer}>
                <Heart className="icon iconLarge" />
                <Text className={styles.iconContainerText}>Обране</Text>
              </Link>

              <Link to="/notifications" className={styles.iconContainer}>
                <Bell className="icon iconLarge" />
                <Text className={styles.iconContainerText}>Сповіщення</Text>
              </Link>

              <div className={styles.iconContainer} onClick={openCart}>
                <ShoppingBag className="icon iconLarge" />
                <Text className={styles.iconContainerText}>Кошик</Text>
              </div>
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
