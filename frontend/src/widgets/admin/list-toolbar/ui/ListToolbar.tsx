import type { ReactNode } from "react";
import { SegmentedControl } from "@mantine/core";
import { Search } from "lucide-react";
import type { EntityStatus } from "@/shared/api/crud";
import styles from "./ListToolbar.module.css";

const VISIBILITY_OPTIONS: { value: EntityStatus; label: string }[] = [
  { value: "ActiveAndHidden", label: "Усі" },
  { value: "Active", label: "Активні" },
  { value: "Hidden", label: "Приховані" },
];

type ListToolbarProps = {
  search?: string;
  onSearchChange?: (value: string) => void;
  searchPlaceholder?: string;
  status?: EntityStatus;
  onStatusChange?: (value: EntityStatus) => void;
  children?: ReactNode;
};

export default function ListToolbar({
  search,
  onSearchChange,
  searchPlaceholder = "Пошук...",
  status,
  onStatusChange,
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

      {status && onStatusChange && (
        <SegmentedControl
          value={status}
          onChange={(value) => onStatusChange(value as EntityStatus)}
          data={VISIBILITY_OPTIONS}
          radius="md"
          className={styles.segmented}
        />
      )}
    </div>
  );
}
