import { UpdateAdvertisementForm } from "@/features/update-advertisement";
import styles from "./UpdateAdvertisementPage.module.css";

export default function UpdateAdvertisementPage() {
  const advertisementId = Number(window.location.pathname.split("/").pop());
  return (
    <div className={styles.updateAdvertisementPage}>
      <div className={styles.title}>
        <h2>Змінити оголошення</h2>
        <p>Заповніть всі поля для зміни оголошення</p>
      </div>
      <UpdateAdvertisementForm advertisementId={advertisementId} />
    </div>
  );
}
