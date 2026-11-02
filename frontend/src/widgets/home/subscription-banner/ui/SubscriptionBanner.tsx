import Container from "@/shared/ui/layout/container";
import styles from "./SubscriptionBanner.module.css";
import { Text } from "@mantine/core";
import { SubscriptionBannerSearch } from "./SubscriptionBannerSearch";

export function SubscriptionBanner() {
  return (
    <Container>
      <section className={styles.subscriptionBanner}>
        <div className={styles.content}>
          <div className={styles.text}>
            <Text className={styles.title}>Весняний розпродаж техніки!</Text>
            <Text className={styles.description}>
              Підпишіться на новини та отримуйте знижки
            </Text>
          </div>
          <SubscriptionBannerSearch />
        </div>
      </section>
    </Container>
  );
}
