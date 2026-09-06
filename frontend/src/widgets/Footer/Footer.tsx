import Container from "@/shared/ui/container";
import styles from "./Footer.module.css";
import { Divider, Text } from "@mantine/core";
import { Link } from "react-router-dom";
import { Headset } from "lucide-react";

const footerLinks = [
  { label: "Про нас", to: "/about" },
  { label: "Доставка", to: "/delivery" },
  { label: "Гарантія", to: "/warranty" },
  { label: "Повернення", to: "/returns" },
  { label: "Конфіденційність", to: "/privacy" },
];

export default function Footer() {
  return (
    <footer className={styles.footer}>
      <Container>
        <div className={styles.content}>
          <Text fz={35} fw={600}>
            Marketly
          </Text>
          <nav className={styles.footerLinks} aria-label="Навігація футера">
            {footerLinks.map(({ label, to }) => (
              <Link key={to} to={to} className={styles.footerLink}>
                {label}
              </Link>
            ))}
          </nav>
          <div className={styles.footerInfo}>
            <Divider
              orientation="vertical"
              h={85}
              color="var(--color-primary)"
            />
            <div className={styles.support}>
              <Headset size={30} />
              <div className={styles.supportText}>
                <Text>Підтримка</Text>
                <a
                  href="mailto:support@marketly.ua"
                  className={styles.supportEmail}
                >
                  support@marketly.ua
                </a>
              </div>
            </div>
            <Divider
              orientation="vertical"
              h={85}
              color="var(--color-primary)"
            />
            <div className={styles.copyright}>
              <Text c="#32639D" fz={14}>
                © 2024 Marketly
              </Text>
              <Text c="#32639D">Усі права захищені</Text>
            </div>
          </div>
        </div>
      </Container>
    </footer>
  );
}
