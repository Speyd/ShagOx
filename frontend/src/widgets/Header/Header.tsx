import Container from "@/shared/ui/Container";
import styles from "./Header.module.css";
import Button from "@/shared/ui/Button";

export default function Header() {
  return (
    <header className={styles.header}>
      <Container>
        <div className={styles.content}>
          <h2>Header</h2>
          <Button>Login</Button>
        </div>
      </Container>
    </header>
  );
}
