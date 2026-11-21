import { Text,  Image } from "@mantine/core";
import styles from "./EmptyState.module.css";
import Button from "../../buttons/button";

export interface EmptyStateProps {
  title?: string;
  description?: string;
  image?: string;
  label?: string;
  buttonLabel?: string;
  onButtonClick?: () => void;
  variant?: "card" | "dashed";
}

export function EmptyState({
  title,
  description,
  image,
  label = "Порожній стан",
  buttonLabel,
  onButtonClick,
  variant = "card",
}: EmptyStateProps) {
  return (
    <section className={`${styles.container} ${styles[variant]}`}>
      {title && <Text className={styles.title}>{title}</Text>}

      {description && (
        <Text c="dimmed" className={styles.description}>
          {description}
        </Text>
      )}

      {buttonLabel && onButtonClick && (
        <Button onClick={onButtonClick} className={styles.button}>
          {buttonLabel}
        </Button>
      )}

      {image && (
        <Image
          src={image}
          alt={label}
          w={230}
          h={200}
          fit="contain"
          className={styles.image}
        />
      )}
    </section>
  );
}
