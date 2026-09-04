import Container from "@/shared/ui/container";
import styles from "./Header.module.css";
import Button from "@/shared/ui/button";
import { Link, useLocation, useNavigate } from "react-router-dom";
import { useAuthStore } from "@/features/auth/store/useAuthStore";
import { Heart, Shield } from "lucide-react";
import { useLogout } from "@/features/auth/model/hooks/useLogout";
import { hasRole } from "@/shared/lib/auth";

export default function Header() {
  const navigate = useNavigate();

  const user = useAuthStore((state) => state.user);
  const isAdmin = hasRole(user, "Admin");

  const logoutStore = useAuthStore((state) => state.logout);

  const logoutMutation = useLogout();

  const location = useLocation();
  const isFavoritePage = location.pathname === "/favorite";
  const isAdminPage = location.pathname === "/admin";

  const handleLogout = () => {
    logoutMutation.mutate(undefined, {
      onSuccess: () => {
        logoutStore();
        navigate("/", { replace: true });
      },
    });
  };

  return (
    <header className={styles.header}>
      <Container>
        <div className={styles.content}>
          <Link to="/" className={styles.logo}>
            ShagOx
          </Link>

          {user && <p className={styles.username}>{user.name}</p>}

          {isAdmin && <p className={styles.username}>{user?.roles[0].name}</p>}

          <div className={styles.buttons}>
            <Button onClick={() => navigate("/create-advertisement")}>
              Додати оголошення
            </Button>
            <Button onClick={() => navigate("/login")}>Увійти</Button>
            <Button onClick={() => handleLogout()}>Вийти</Button>
            <Link to="/favorite" className={styles.favoriteButton}>
              <Heart
                size={35}
                fill={isFavoritePage ? "#ef4444" : "none"}
                color={isFavoritePage ? "#ef4444" : "currentColor"}
              />
            </Link>
            {isAdmin && (
              <Link to="/admin" className={styles.favoriteButton}>
                <Shield
                  size={35}
                  fill={isAdminPage ? "#2563eb" : "none"}
                  color={isAdminPage ? "#2563eb" : "currentColor"}
                />
              </Link>
            )}
          </div>
        </div>
      </Container>
    </header>
  );
}
