import Container from "@/shared/ui/Container";
import styles from "./Header.module.css";
import Button from "@/shared/ui/Button";
import { Link, useNavigate } from "react-router-dom";
import { useLogout } from "@/features/auth/hooks/useLogout";
import { useAuthStore } from "@/features/auth/store/useAuthStore";

export default function Header() {
  const navigate = useNavigate();

  const user = useAuthStore((state) => state.user);
  const logoutStore = useAuthStore((state) => state.logout);

  const logoutMutation = useLogout();

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

          <div className={styles.buttons}>
            <Button onClick={() => navigate("/create-advertisement")}>
              Додати оголошення
            </Button>
            <Button onClick={() => navigate("/login")}>Увійти</Button>
            <Button onClick={() => handleLogout()}>Вийти</Button>
          </div>
        </div>
      </Container>
    </header>
  );
}
