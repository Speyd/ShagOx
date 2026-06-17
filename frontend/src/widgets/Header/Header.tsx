import Container from "@/shared/ui/Container";
import styles from "./Header.module.css";
import Button from "@/shared/ui/Button";
import { Link, useNavigate } from "react-router-dom";

export default function Header() {
  const navigate = useNavigate();

  return (
    <header className={styles.header}>
      <Container>
        <div className={styles.content}>
          <Link to="/" className={styles.logo}>
            ShagOx
          </Link>

          <Button onClick={() => navigate("/login")}>Login</Button>
        </div>
      </Container>
    </header>
  );
}
