import { Link } from "react-router-dom";
import { Plus } from "lucide-react";
import styles from "./CompareBoard.module.css";

/** Пунктирна картка, що веде до каталогу категорії. */
export function AddCompareCard({ to }: { to: string }) {
  return (
    <Link to={to} className={styles.addCard}>
      <span className={styles.addIcon}>
        <Plus size={22} />
      </span>
      <span className={styles.addTitle}>Додати товар</span>
      <span className={styles.addHint}>
        Додайте ще товар
        <br />
        для порівняння
      </span>
    </Link>
  );
}
