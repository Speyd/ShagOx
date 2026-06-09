import Container from "@/shared/ui/Container";
import styles from "./Footer.module.css";

export default function Footer() {
  return (
    <footer className={styles.footer}>
      <Container>
        <div className={styles.content}>
          <p>© 2026 ShagOx</p>
        </div>
      </Container>
    </footer>
  );
}
