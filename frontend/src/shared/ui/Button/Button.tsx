import type { ButtonHTMLAttributes } from "react";
import styles from "./Button.module.css";
import clsx from "clsx";

type ButtonProps = ButtonHTMLAttributes<HTMLButtonElement>;

export default function Button({ className, ...props }: ButtonProps) {
  return <button className={clsx(styles.button, className)} {...props} />;
}
