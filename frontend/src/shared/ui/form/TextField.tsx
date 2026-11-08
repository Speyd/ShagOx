import type { InputHTMLAttributes, Ref } from "react";
import clsx from "clsx";
import styles from "./Form.module.css";

type TextFieldProps = InputHTMLAttributes<HTMLInputElement> & {
  invalid?: boolean;
  ref?: Ref<HTMLInputElement>;
};

export default function TextField({
  invalid,
  className,
  ...props
}: TextFieldProps) {
  return (
    <input
      className={clsx(styles.control, invalid && styles.invalid, className)}
      aria-invalid={invalid}
      {...props}
    />
  );
}
