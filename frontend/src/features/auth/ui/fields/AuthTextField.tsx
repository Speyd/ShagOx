import type { InputHTMLAttributes, ReactNode } from "react";
import type { LucideIcon } from "lucide-react";
import Input from "@/shared/ui/data-display/inputs/input";
import styles from "./AuthFields.module.css";

export type AuthTextFieldProps = InputHTMLAttributes<HTMLInputElement> & {
  icon: LucideIcon;
  label?: string;
  error?: string;
  /** Extra control rendered inside the input on the right (e.g. eye toggle). */
  endSlot?: ReactNode;
};

/**
 * Input with a leading icon, optional label and validation message.
 * Shared by login, registration, password recovery and password change.
 * Works with react-hook-form: `<AuthTextField {...register("email")} />`
 * (React 19 passes `ref` as a regular prop).
 */
export default function AuthTextField({
  icon: Icon,
  label,
  error,
  endSlot,
  id,
  name,
  ...inputProps
}: AuthTextFieldProps) {
  const inputId = id ?? (label ? `auth-${name}` : undefined);
  const errorId = error && inputId ? `${inputId}-error` : undefined;

  return (
    <div className={styles.field}>
      {label && (
        <label className={styles.label} htmlFor={inputId}>
          {label}
        </label>
      )}

      <div className={`${styles.control} ${error ? styles.invalid : ""}`}>
        <Icon className={styles.icon} aria-hidden />
        <Input
          id={inputId}
          name={name}
          aria-invalid={!!error}
          aria-describedby={errorId}
          {...inputProps}
        />
        {endSlot}
      </div>

      {error && (
        <p id={errorId} className={styles.error}>
          {error}
        </p>
      )}
    </div>
  );
}
