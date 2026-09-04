import { forwardRef } from "react";
import type { TextareaHTMLAttributes } from "react";
import styles from "./TextArea.module.css";

type TextareaProps = TextareaHTMLAttributes<HTMLTextAreaElement>;

const TextArea = forwardRef<HTMLTextAreaElement, TextareaProps>(
  (props, ref) => {
    return <textarea className={styles.textarea} {...props} ref={ref} />;
  },
);

TextArea.displayName = "TextArea";

export default TextArea;
