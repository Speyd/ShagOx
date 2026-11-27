import type { ReactNode } from "react";
import { Search } from "lucide-react";
import styles from "./ListToolbar.module.css";

type ListToolbarProps = {
  search?: string;
  onSearchChange?: (value: string) => void;
  searchPlaceholder?: string;
  children?: ReactNode;
};

export default function ListToolbar({
  search,
  onSearchChange,
  searchPlaceholder = "Пошук...",
  children,
}: ListToolbarProps) {
  return (
    <div className={styles.toolbar}>
      {onSearchChange && (
        <label className={styles.search}>
          <Search size={16} className={styles.searchIcon} />
          <input
            type="search"
            value={search}
            onChange={(event) => onSearchChange(event.currentTarget.value)}
            placeholder={searchPlaceholder}
            className={styles.searchInput}
          />
        </label>
      )}

      {children}
    </div>
  );
}
