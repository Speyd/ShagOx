import { Input, Text } from "@mantine/core";
import styles from "./CheckoutDetailsForm.module.css";

export default function CheckoutDetailsForm() {
  return (
    <div className={styles.checkoutDetailsForm}>
      <Text fw={600} fz={24}>
        Контактні дані
      </Text>
      <div className={styles.inputWrapper}>
        <label>Ім'я</label>
        <Input />
      </div>
      <div className={styles.inputWrapper}>
        <label>Прізвище</label>
        <Input />
      </div>
      <div className={styles.inputWrapper}>
        <label>Електронна пошта</label>
        <Input />
      </div>
      <div className={styles.inputWrapper}>
        <label>Номер телефону</label>
        <Input />
      </div>
      <Text fw={600} fz={24}>
        Деталі доставки
      </Text>
    </div>
  );
}
