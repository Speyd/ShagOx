import Container from "@/shared/ui/Container";
import styles from "./Header.module.css";
import Button from "@/shared/ui/Button";
import Input from "@/shared/ui/Input";

export default function Header() {
  return (
    <header className={styles.header}>
      <Container>
        <div className={styles.content}>
          <h2>Header</h2>
          <Input placeholder="Email" />
          <Button>Login</Button>
        </div>
      </Container>
    </header>
  );
}
