import type { ButtonHTMLAttributes } from "react";
import styles from "./Button.module.css";
import clsx from "clsx";
import type { LucideIcon } from "lucide-react";

type ButtonProps = ButtonHTMLAttributes<HTMLButtonElement> & {
  hideTextOnMobile?: boolean;
  loading?: boolean;
  variant?: "default" | "outline" | "light" | "ghost" | "danger";
  size?: "sm" | "md";
  leftSection?: LucideIcon;
  rightSection?: LucideIcon;
  iconProps?: { size?: number; color?: string; className?: string };
};

export default function Button({
  hideTextOnMobile = false,
  children,
  className,
  disabled,
  loading = false,
  variant = "default",
  size = "md",
  leftSection: LeftIcon,
  rightSection: RightIcon,
  iconProps,
  ...props
}: ButtonProps) {
  return (
    <button
      className={clsx(
        styles.button,
        styles[variant],
        styles[size],
        hideTextOnMobile && styles.hideTextOnMobile,
        className,
      )}
      disabled={disabled || loading}
      aria-busy={loading}
      {...props}
    >
      {loading ? (
        <span className={styles.spinner} aria-hidden="true" />
      ) : (
        <div className={styles.content}>
          {LeftIcon && <LeftIcon {...iconProps} />}
          {children && <span className={styles.children}>{children}</span>}
          {RightIcon && <RightIcon {...iconProps} />}
        </div>
      )}
    </button>
  );
}
