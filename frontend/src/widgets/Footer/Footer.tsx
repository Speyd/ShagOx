import Container from "@/shared/ui/container";
import styles from "./Footer.module.css";
import { Divider, Text } from "@mantine/core";
import { Link } from "react-router-dom";
import { Headset } from "lucide-react";

export default function Footer() {
  return (
    <footer className={styles.footer}>
      <Container>
        <div className={styles.content}>
          <Text fz={35} fw={700}>
            Marketly
          </Text>
          <nav className={styles.footerLinks}>
            <Link to="/about" className={styles.footerLink}>
              Про нас
            </Link>
            <Link to="/delivery" className={styles.footerLink}>
              Доставка
            </Link>
            <Link to="/warranty" className={styles.footerLink}>
              Гарантія
            </Link>
            <Link to="/returns" className={styles.footerLink}>
              Повернення
            </Link>
            <Link to="/privacy" className={styles.footerLink}>
              Конфіденційність
            </Link>
          </nav>
          <div className={styles.footerSupportContainer}>
            <Divider
              orientation="vertical"
              h={85}
  
              color="#32639D"
              style={{ alignSelf: "center" }}
            />
            <div className={styles.support}>
              <Headset  size={35} />
              <div className={styles.supportText}>
                <Text>Підтримка</Text>
                <Text c="#32639D">Support @marketly.ua</Text>
              </div>
            </div>
            <Divider
              orientation="vertical"
              h={85}
              color="#32639D"
              style={{ alignSelf: "center" }}
            />
            <div>
              <Text c="#32639D">© 2024 Marketly</Text>
              <Text c="#32639D">Усі права захищені</Text>
            </div>
          </div>
        </div>
      </Container>
    </footer>
  );
}
