import CreateAdvertisementForm from "@/features/create-advertisement";
import styles from "./CreateAdvertisementPage.module.css";

export default function CreateAdvertisementPage() {
  return (
    <div className={styles.createAdvertisementPage}>
      <div className={styles.title}>
        <h2>Створити оголошення</h2>
        <p>Заповніть всі поля для створення оголошення</p>
      </div>

      <CreateAdvertisementForm />
    </div>
  );
}
