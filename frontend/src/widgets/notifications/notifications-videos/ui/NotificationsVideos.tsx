import { Text } from "@mantine/core";
import styles from "./NotificationsVideos.module.css";

export default function NotificationsVideos() {
  return (
    <section className={styles.notificationsVideos}>
      <div className={styles.notificationsEmpty}>
        <Text fz={24} fw={500} c="var(--color-primary)">
          Немає нових сповіщень
        </Text>
      </div>
    </section>
  );
}
