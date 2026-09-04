import type { ButtonHTMLAttributes } from "react";
import styles from "./IconButton.module.css";
import clsx from "clsx";

type IconButtonProps = ButtonHTMLAttributes<HTMLButtonElement>;

export default function IconButton({ className, ...props }: IconButtonProps) {
  return <button className={clsx(styles.button, className)} {...props} />;
}
