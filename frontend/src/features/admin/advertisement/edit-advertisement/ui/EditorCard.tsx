import type { ReactNode } from "react";
import { Text } from "@mantine/core";
import styles from "./AdvertisementEditor.module.css";

type EditorCardProps = {
  title: string;
  subtitle?: string;
  icon?: ReactNode;
  aside?: ReactNode;
  children: ReactNode;
};

export default function EditorCard({ title, subtitle, icon, aside, children }: EditorCardProps) {
  return (
    <section className={styles.card}>
      <header className={styles.cardHeader}>
        <div>
          <div className={styles.cardTitle}>
            {icon && <span className={styles.cardIcon}>{icon}</span>}
            <Text fw={700} size="lg" c="var(--color-base)">
              {title}
            </Text>
          </div>
          {subtitle && (
            <Text size="sm" c="dimmed">
              {subtitle}
            </Text>
          )}
        </div>
        {aside}
      </header>
      {children}
    </section>
  );
}
