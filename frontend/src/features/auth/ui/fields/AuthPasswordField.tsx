import { useState } from "react";
import { Eye, EyeOff, LockIcon, type LucideIcon } from "lucide-react";
import AuthTextField, { type AuthTextFieldProps } from "./AuthTextField";
import styles from "./AuthFields.module.css";

type Props = Omit<AuthTextFieldProps, "icon" | "type" | "endSlot"> & {
  icon?: LucideIcon;
  /**
   * Controlled visibility — pass together with `onVisibleChange` to share one
   * toggle between "password" and "repeat password" fields.
   */
  visible?: boolean;
  onVisibleChange?: (visible: boolean) => void;
  /** Hide the eye button (e.g. on a "repeat password" field). */
  hideToggle?: boolean;
};

export default function AuthPasswordField({
  icon = LockIcon,
  visible,
  onVisibleChange,
  hideToggle = false,
  ...props
}: Props) {
  const [innerVisible, setInnerVisible] = useState(false);
  const isVisible = visible ?? innerVisible;

  const toggle = () => {
    const next = !isVisible;
    setInnerVisible(next);
    onVisibleChange?.(next);
  };

  return (
    <AuthTextField
      {...props}
      icon={icon}
      type={isVisible ? "text" : "password"}
      endSlot={
        hideToggle ? undefined : (
          <button
            type="button"
            className={styles.eyeButton}
            onClick={toggle}
            tabIndex={-1}
            aria-label={isVisible ? "Приховати пароль" : "Показати пароль"}
          >
            {isVisible ? (
              <Eye className={styles.eyeIcon} />
            ) : (
              <EyeOff className={styles.eyeIcon} />
            )}
          </button>
        )
      }
    />
  );
}
