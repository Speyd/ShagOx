import { Text } from "@mantine/core";
import styles from "./NotificationsComments.module.css";

export default function NotificationsComments() {
  return (
    <section className={styles.notificationsComments}>
      <div className={styles.notificationsEmpty}>
        <Text fz={24} fw={500} c="var(--color-primary)">
          Немає нових сповіщень
        </Text>
      </div>
    </section>
  );
}
