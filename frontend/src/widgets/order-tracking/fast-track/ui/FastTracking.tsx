import { Text } from "@mantine/core";
import styles from "./FastTracking.module.css";
import Input from "@/shared/ui/input";
import Button from "@/shared/ui/button";

export default function FastTracking() {
  return (
    <section className={styles.fastTracking}>
      <Text fz={24} fw={600}>
        Миттєве відстеження
      </Text>
      <Text fz={15} fw={400} className={styles.description}>
        Введіть номер вашого замовлення нижче, щоб швидко перевірити його статус
        без авторизації.
      </Text>
      <div className={styles.inputContainer}>
        <Input placeholder="Напр. МКТ-878123" className={styles.input} />
        <Button>Знайти</Button>
      </div>
    </section>
  );
}
