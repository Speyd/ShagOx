import type { ButtonHTMLAttributes } from "react";
import styles from "./IconButton.module.css";

type IconButtonProps = ButtonHTMLAttributes<HTMLButtonElement>;

export default function IconButton(props: IconButtonProps) {
  return <button className={styles.button} {...props} />;
}
