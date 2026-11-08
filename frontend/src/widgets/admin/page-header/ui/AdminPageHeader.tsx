import type { ReactNode } from "react";
import { Link } from "react-router-dom";
import { Text } from "@mantine/core";
import { ArrowLeft } from "lucide-react";
import styles from "./AdminPageHeader.module.css";

type AdminPageHeaderProps = {
  title: string;
  description?: ReactNode;
  backTo?: string;
  backLabel?: string;
  actions?: ReactNode;
};

export default function AdminPageHeader({
  title,
  description,
  backTo,
  backLabel = "Назад",
  actions,
}: AdminPageHeaderProps) {
  return (
    <header className={styles.header}>
      {backTo && (
        <Link to={backTo} className={styles.back}>
          <ArrowLeft size={16} />
          {backLabel}
        </Link>
      )}
      <div className={styles.row}>
        <div className={styles.titles}>
          <Text component="h1" className={styles.title}>
            {title}
          </Text>
          {description && (
            <Text c="dimmed" size="sm">
              {description}
            </Text>
          )}
        </div>
        {actions && <div className={styles.actions}>{actions}</div>}
      </div>
    </header>
  );
}
