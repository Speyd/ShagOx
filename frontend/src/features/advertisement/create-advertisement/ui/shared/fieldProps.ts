import styles from "../CreateAdvertisementForm.module.css";

/** Спільні пропси для всіх полів форми (розмір та стиль підпису). */
export const fieldProps = {
  size: "md",
  classNames: { label: styles.label },
} as const;
