import Container from "@/shared/ui/container";
import styles from "./ComparisonPage.module.css";
import { Text } from "@mantine/core";

export default function ComparisonPage() {
  return (
    <section className={styles.comparisonPage}>
      <Container>
        <div className={styles.comparisonPageContent}>
          <div className={styles.header}>
            <Text fz={40} fw={700}>
              Порівняння товарів
            </Text>
            <Text fz={14} fw={400} className={styles.description}>
              Оберіть найкращу модель для себе
            </Text>
          </div>
        </div>
      </Container>
    </section>
  );
}
