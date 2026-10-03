import { useState, useEffect } from "react";
import { TextInput } from "@mantine/core";
import styles from "./SearchInput.module.css";
import { Search } from "lucide-react";
import { useNavigate, useSearchParams } from "react-router-dom";

export default function SearchInput() {
  const navigate = useNavigate();
  const [searchParams] = useSearchParams();
  const queryFromUrl = searchParams.get("search") ?? "";
  const [value, setValue] = useState(queryFromUrl);

  useEffect(() => {
    // Keep the input in sync when navigation changes the query.
    // eslint-disable-next-line react-hooks/set-state-in-effect
    setValue(queryFromUrl);
  }, [queryFromUrl]);

  const handleSubmit = (e?: React.FormEvent) => {
    if (e) e.preventDefault();
    const trimmed = value.trim();
    if (trimmed) {
      navigate(`/catalog?search=${encodeURIComponent(trimmed)}`);
    } else {
      navigate("/catalog");
    }
  };

  return (
    <form onSubmit={handleSubmit} style={{ flex: 1, display: "flex" }}>
      <TextInput
        placeholder="Пошук товарів, відео, брендів..."
        value={value}
        onChange={(e) => setValue(e.currentTarget.value)}
        rightSection={
          <button
            type="submit"
            style={{
              background: "none",
              border: "none",
              padding: 0,
              cursor: "pointer",
              display: "flex",
              alignItems: "center",
              justifyContent: "center",
              color: "inherit",
            }}
            aria-label="Пошук"
          >
            <Search size={18} />
          </button>
        }
        size="lg"
        className={styles.searchInput}
      />
    </form>
  );
}
