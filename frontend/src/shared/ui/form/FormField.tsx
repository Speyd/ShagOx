import type { ReactNode } from "react";
import { Text } from "@mantine/core";
import clsx from "clsx";
import styles from "./Form.module.css";

type FormFieldProps = {
  label?: string;
  htmlFor?: string;
  required?: boolean;
  hint?: ReactNode;
  error?: string;
  className?: string;
  children: ReactNode;
};

export default function FormField({
  label,
  htmlFor,
  required,
  hint,
  error,
  className,
  children,
}: FormFieldProps) {
  return (
    <div className={clsx(styles.field, className)}>
      {label && (
        <label htmlFor={htmlFor} className={styles.label}>
          {label}
          {required && <span className={styles.required}>*</span>}
        </label>
      )}
      {children}
      {error ? (
        <Text size="xs" c="red.7">
          {error}
        </Text>
      ) : (
        hint && (
          <Text size="xs" c="dimmed">
            {hint}
          </Text>
        )
      )}
    </div>
  );
}
