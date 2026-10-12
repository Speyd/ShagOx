import styles from "./SupportPage.module.css";
import SupportForm from "@/widgets/support";

export default function SupportPage() {
  return (
    <section className={styles.supportPage}>
      <div className={styles.supportPageContent}>
        <SupportForm />
      </div>
    </section>
  );
}
