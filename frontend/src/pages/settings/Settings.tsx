import Container from "@/shared/ui/layout/container";
import {
  ContactsSection,
  DeleteAccountSection,
  PasswordSection,
  SettingsSidebar,
  TwoFactorSection,
} from "@/widgets/settings";
import styles from "./Settings.module.css";
import SessionsSection from "@/widgets/settings/ui/SessionsSection";

export function Settings() {
  return (
    <section className={styles.page}>
      <Container>
        <div className={styles.container}>
          <header className={styles.header}>
            <h1 className={styles.title}>Безпека та конфіденційність</h1>
            <p className={styles.description}>
              Керуйте налаштуваннями безпеки вашого облікового запису Marketly,
              щоб забезпечити надійний захист ваших даних.
            </p>
          </header>

          <div className={styles.layout}>
            <div className={styles.aside}>
              <SettingsSidebar />
            </div>

            <div className={styles.content}>
              <PasswordSection />
              <ContactsSection />
              <TwoFactorSection />
              <SessionsSection />
              <DeleteAccountSection />
            </div>
          </div>
        </div>
      </Container>
    </section>
  );
}
