import type { ReactNode } from "react";
import styles from "../CreateAdvertisementForm.module.css";

type FormCardProps = {
  title: string;
  subtitle?: string;
  icon?: ReactNode;
  required?: boolean;
  aside?: ReactNode;
  children: ReactNode;
};

export default function FormCard({
  title,
  subtitle,
  icon,
  required,
  aside,
  children,
}: FormCardProps) {
  return (
    <section className={styles.card}>
      <header className={styles.cardHeader}>
        <div>
          <div className={styles.cardTitleRow}>
            {icon}
            <h3 className={styles.cardTitle}>
              {title}
              {required && <span className={styles.requiredStar}>*</span>}
            </h3>
          </div>
          {subtitle && <p className={styles.cardSubtitle}>{subtitle}</p>}
        </div>
        {aside}
      </header>

      {children}
    </section>
  );
}
