import { Text } from "@mantine/core";
import styles from "./NotificationsGeneral.module.css";

export default function NotificationsGeneral() {
  return (
    <section className={styles.notificationsGeneral}>
      <div className={styles.notificationsEmpty}>
        <Text fz={24} fw={500} c="var(--color-primary)">
          Немає нових сповіщень
        </Text>
      </div>
    </section>
  );
}
