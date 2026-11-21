import { Text } from "@mantine/core";
import styles from "./SupportForm.module.css";
import Input from "@/shared/ui/data-display/inputs/input";
import Button from "@/shared/ui/buttons/button";
import TextArea from "@/shared/ui/data-display/inputs/text-area";

export default function SupportForm() {
  return (
    <form className={styles.supportForm}>
      <div className={styles.formHeader}>
        <Text className={styles.title}>Зв'язок із підтримкою</Text>
        <Text className={styles.formDescription}>
          Заповніть форму нижче, і наша команда зв'яжеться з вами найближчим
          часом.
        </Text>
      </div>

      <div className={styles.formFields}>
        <div className={styles.inputContainer}>
          <label htmlFor="email">Ваш Email</label>
          <Input id="email" placeholder="example@email.com" />
        </div>
        <div className={styles.inputContainer}>
          <label htmlFor="message">Ваше повідомлення</label>
          <TextArea
            id="message"
            placeholder="Опишіть вашу проблему або запитання..."
          />
        </div>
      </div>

      <Button type="submit" className={styles.submitButton}>
        Продовжити
      </Button>

      <div className={styles.contactsContainer}>
        <div className={styles.contactItem}>
          <Text className={styles.textSecondary}>Гаряча лінія</Text>
          <Text className={styles.contactValue}>+380 12 345 67 89</Text>
        </div>
        <div className={styles.contactItem}>
          <Text className={styles.textSecondary}>Пряма пошта</Text>
          <Text className={styles.contactValue}>support@marketly.com</Text>
        </div>
      </div>

      <Text className={styles.privacyText}>
        Служба турботи Marketly працює щодня з 08:00 до 21:00. Середній час
        відповіді — до 15 хвилин.
      </Text>
    </form>
  );
}
