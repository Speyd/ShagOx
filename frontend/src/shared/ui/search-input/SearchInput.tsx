import { TextInput } from "@mantine/core";
import styles from "./SearchInput.module.css";
import { Search } from "lucide-react";

export default function SearchInput() {
  return (
    <TextInput
      placeholder="Пошук товарів, відео, брендів..."
      rightSection={<Search size={18} />}
      size="lg"
      className={styles.searchInput}
    />
  );
}
