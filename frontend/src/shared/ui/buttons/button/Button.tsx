import type { ButtonHTMLAttributes, ReactNode } from "react";
import styles from "./Button.module.css";
import clsx from "clsx";

type ButtonProps = ButtonHTMLAttributes<HTMLButtonElement> & {
  loading?: boolean;
  variant?: "default" | "outline" | "light" | "ghost" | "danger";
  size?: "sm" | "md";
  leftIcon?: ReactNode;
};

export default function Button({
  children,
  className,
  disabled,
  loading = false,
  variant = "default",
  size = "md",
  leftIcon,
  ...props
}: ButtonProps) {
  return (
    <button
      className={clsx(styles.button, styles[variant], styles[size], className)}
      disabled={disabled || loading}
      aria-busy={loading}
      {...props}
    >
      {loading ? (
        <span className={styles.spinner} aria-hidden="true" />
      ) : (
        <>
          {leftIcon}
          {children}
        </>
      )}
    </button>
  );
}
