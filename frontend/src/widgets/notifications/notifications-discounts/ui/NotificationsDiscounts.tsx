import { Text } from "@mantine/core";
import styles from "./NotificationsDiscounts.module.css";

export default function NotificationsDiscounts() {
  return (
    <section className={styles.notificationsDiscounts}>
      <div className={styles.notificationsEmpty}>
        <Text fz={24} fw={500} c="var(--color-primary)">
          Немає нових сповіщень
        </Text>
      </div>
    </section>
  );
}
