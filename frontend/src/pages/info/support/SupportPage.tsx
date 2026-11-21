import styles from "./SupportPage.module.css";
import SupportForm from "@/widgets/info/support-form";

export default function SupportPage() {
  return (
    <section className={styles.supportPage}>
      <div className={styles.supportPageContent}>
        <SupportForm />
      </div>
    </section>
  );
}
