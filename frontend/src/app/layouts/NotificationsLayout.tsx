import Container from "@/shared/ui/container";
import { Text } from "@mantine/core";
import { Outlet } from "react-router-dom";
import styles from "./NotificationsLayout.module.css";
import NotificationSidebar from "@/widgets/notifications/notification-sidebar/ui/NotificationSidebar";

export default function NotificationsLayout() {
  return (
    <section className={styles.notificationsLayout}>
      <Container>
        <div className={styles.notificationsContainer}>
          <div className={styles.notificationsHeader}>
            <Text fz={40} fw={700}>
              Центр сповіщень
            </Text>
            <Text fz={14} fw={400} className={styles.description}>
              У вас немає нових сповіщень.
            </Text>
          </div>

          <div className={styles.notificationsContent}>
            <aside>
              <NotificationSidebar />
            </aside>
            <main>
              <Outlet />
            </main>
          </div>
        </div>
      </Container>
    </section>
  );
}
