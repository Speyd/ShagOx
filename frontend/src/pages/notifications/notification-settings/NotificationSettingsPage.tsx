import {
  CommunicationChannels,
  NotificationTypes,
} from "@/widgets/notifications";
import styles from "./NotificationSettingsPage.module.css";
import Container from "@/shared/ui/container";
import { Text } from "@mantine/core";

export default function NotificationSettingsPage() {
  return (
    <section className={styles.notificationSettingsPage}>
      <Container>
        <div className={styles.notificationSettingsContainer}>
          <div className={styles.notificationSettingsHeader}>
            <Text fz={40} fw={700}>
              Сповіщення та канали зв'язку
            </Text>
            <Text fz={14} fw={400} className={styles.description}>
              Керуйте налаштуваннями сповіщень, щоб отримувати лише найважливішу{" "}
              <br />
              інформацію зручним для вас способом.
            </Text>
          </div>
          <div className={styles.notificationSettingsContent}>
            <NotificationTypes />
            <CommunicationChannels />
          </div>
        </div>
      </Container>
    </section>
  );
}
