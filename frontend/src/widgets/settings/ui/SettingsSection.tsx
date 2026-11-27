import type { ReactNode } from "react";
import type { LucideIcon } from "lucide-react";
import clsx from "clsx";
import type { SettingsSectionId } from "../model/sections";
import styles from "./Settings.module.css";

type Props = {
  id: SettingsSectionId;
  title: string;
  description?: ReactNode;
  icon?: LucideIcon;
  badge?: ReactNode;
  aside?: ReactNode;
  danger?: boolean;
  children?: ReactNode;
};

export default function SettingsSection({
  id,
  title,
  description,
  icon: Icon,
  badge,
  aside,
  danger = false,
  children,
}: Props) {
  return (
    <section
      id={id}
      className={clsx(styles.section, danger && styles.sectionDanger)}
      aria-labelledby={`${id}-title`}
    >
      <header className={styles.sectionHeader}>
        <div className={styles.sectionHeading}>
          <div className={styles.sectionTitleRow}>
            <h2 id={`${id}-title`} className={styles.sectionTitle}>
              {title}
            </h2>
            {badge}
          </div>
          {description && (
            <p className={styles.sectionDescription}>{description}</p>
          )}
        </div>

        {aside ??
          (Icon && (
            <Icon
              className={clsx(
                styles.sectionIcon,
                danger && styles.sectionIconDanger,
              )}
              aria-hidden
            />
          ))}
      </header>

      {children}
    </section>
  );
}

type BadgeProps = {
  tone: "success" | "danger" | "neutral" | "warning";
  children: ReactNode;
};

export function StatusBadge({ tone, children }: BadgeProps) {
  return <span className={clsx(styles.badge, styles[tone])}>{children}</span>;
}
