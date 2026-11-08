import type { ReactNode } from "react";
import { Text } from "@mantine/core";
import styles from "./InfoTile.module.css";

type InfoTileProps = {
  icon: ReactNode;
  label: string;
  value: ReactNode;
  mono?: boolean;
};

export default function InfoTile({ icon, label, value, mono }: InfoTileProps) {
  return (
    <div className={styles.tile}>
      <span className={styles.icon}>{icon}</span>
      <div className={styles.body}>
        <Text size="xs" c="dimmed">
          {label}
        </Text>
        <Text fw={700} c="var(--color-base)" ff={mono ? "monospace" : undefined} truncate>
          {value}
        </Text>
      </div>
    </div>
  );
}
