import type { Ref, TextareaHTMLAttributes } from "react";
import clsx from "clsx";
import styles from "./Form.module.css";

type TextAreaFieldProps = TextareaHTMLAttributes<HTMLTextAreaElement> & {
  invalid?: boolean;
  ref?: Ref<HTMLTextAreaElement>;
};

export default function TextAreaField({
  invalid,
  className,
  rows = 4,
  ...props
}: TextAreaFieldProps) {
  return (
    <textarea
      rows={rows}
      className={clsx(
        styles.control,
        styles.textarea,
        invalid && styles.invalid,
        className,
      )}
      aria-invalid={invalid}
      {...props}
    />
  );
}
