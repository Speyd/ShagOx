import { Text } from "@mantine/core";
import styles from "./SupportForm.module.css";
import Input from "@/shared/ui/input";
import Button from "@/shared/ui/button";
import TextArea from "@/shared/ui/text-area";

export default function SupportForm() {
  return (
    <form className={styles.supportForm}>
      <div className={styles.formHeader}>
        <Text fz={32} fw={700}>
          Зв'язок із підтримкою
        </Text>
        <Text fz={14} fw={400} className={styles.formDescription}>
          Заповніть форму нижче, і наша команда зв'яжеться з вами найближчими
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
          <Text fz={12} fw={400} className={styles.textSecondary}>
            Гаряча лінія
          </Text>
          <Text fz={14} fw={400}>
            +380 12 345 67 89
          </Text>
        </div>
        <div className={styles.contactItem}>
          <Text fz={12} fw={400} className={styles.textSecondary}>
            Пряма пошта
          </Text>
          <Text fz={14} fw={400}>
            support@marketly.com
          </Text>
        </div>
      </div>

      <Text fz={13} fw={400} className={styles.privacyText}>
        Служба турботи Marketly працює щодня з 08:00 до 21:00. Середній час
        відповіді — до 15 хвилин.
      </Text>
    </form>
  );
}
