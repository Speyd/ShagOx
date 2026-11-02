import { Text } from "@mantine/core";
import styles from "./ReferenceHeader.module.css";

export default function ReferenceHeader() {
  return (
    <section className={styles.referenceHeader}>
      <Text fz={40} fw={700} color="var(--text-base)">
        Довідка та підтримка
      </Text>
      <Text
        fz={15}
        fw={400}
        color="var(--text-base)"
        className={styles.description}
      >
        Знайдіть відповіді на поширені запитання або зв'яжіться з нашою службою
        підтримки
        <br />
        для вирішення будь-яких питань.
      </Text>
    </section>
  );
}
