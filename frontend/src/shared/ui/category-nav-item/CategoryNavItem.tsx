import styles from "./CategoryNavItem.module.css";

export default function CategoryNavItem({
  active,
  label,
  onClick,
}: {
  active: boolean;
  label: string;
  onClick: () => void;
}) {
  return (
    <button
      type="button"
      className={`${styles.categoryItem} ${active ? styles.categoryItemActive : ""}`}
      onClick={onClick}
    >
      {label}
    </button>
  );
}
