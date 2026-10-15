import { useState, useEffect, useRef } from "react";
import { TextInput, Loader } from "@mantine/core";
import { useDebouncedValue } from "@mantine/hooks";
import styles from "./SearchInput.module.css";
import { Search, Image as ImageIcon } from "lucide-react";
import { useNavigate, useSearchParams, Link } from "react-router-dom";
import { useQuery } from "@tanstack/react-query";
import { getCatalogAdvertisements } from "@/entities/advertisement/api/api";

export default function SearchInput() {
  const navigate = useNavigate();
  const [searchParams] = useSearchParams();
  const queryFromUrl = searchParams.get("search") ?? "";

  const [value, setValue] = useState(queryFromUrl);
  const [suggestionsOpen, setSuggestionsOpen] = useState(false);
  const containerRef = useRef<HTMLDivElement>(null);

  const [debouncedSearch] = useDebouncedValue(value.trim(), 300);

  useEffect(() => {
    // OK
    setValue(queryFromUrl);
  }, [queryFromUrl]);

  // 2. Оптимізований запит підказок
  const { data: suggestionsData, isFetching } = useQuery({
    queryKey: ["search-suggestions", debouncedSearch],
    queryFn: () =>
      getCatalogAdvertisements({
        search: debouncedSearch,
        page: 1,
        pageSize: 5,
      }),
    enabled: debouncedSearch.length >= 2,
    staleTime: 60_000, 
  });

  useEffect(() => {
    const handleOutsideClick = (event: MouseEvent) => {
      if (
        containerRef.current &&
        !containerRef.current.contains(event.target as Node)
      ) {
        setSuggestionsOpen(false);
      }
    };

    const handleKeyDown = (event: KeyboardEvent) => {
      if (event.key === "Escape") {
        setSuggestionsOpen(false);
      }
    };

    document.addEventListener("mousedown", handleOutsideClick);
    document.addEventListener("keydown", handleKeyDown);
    return () => {
      document.removeEventListener("mousedown", handleOutsideClick);
      document.removeEventListener("keydown", handleKeyDown);
    };
  }, []);

  const handleSubmit = (e?: React.FormEvent) => {
    if (e) e.preventDefault();
    const trimmed = value.trim();
    setSuggestionsOpen(false);

    if (trimmed) {
      navigate(`/catalog?search=${encodeURIComponent(trimmed)}`);
    } else {
      navigate("/catalog");
    }
  };

  const suggestions = suggestionsData?.items ?? [];

  return (
    <div ref={containerRef} className={styles.searchContainer}>
      <form onSubmit={handleSubmit} className={styles.searchForm}>
        <TextInput
          placeholder="Пошук товарів, відео, брендів..."
          value={value}
          onFocus={() => setSuggestionsOpen(true)}
          onChange={(e) => {
            setValue(e.currentTarget.value);
            setSuggestionsOpen(true);
          }}
          rightSection={
            <button
              type="submit"
              className={styles.searchButton}
              aria-label="Пошук"
            >
              <Search size={18} />
            </button>
          }
          size="lg"
          className={styles.searchInput}
        />
      </form>

      {suggestionsOpen && debouncedSearch.length >= 2 && (
        <div className={styles.suggestions} role="listbox">
          {isFetching ? (
            <div className={styles.suggestionMessage}>
              <Loader size="xs" mr={8} /> Пошук...
            </div>
          ) : suggestions.length > 0 ? (
            suggestions.map((suggestion) => {
              const imageUrl =
                "images" in suggestion
                  ? suggestion.images?.[0]?.url
                  : undefined;

              return (
                <Link
                  key={suggestion.id}
                  to={`/advertisement/${suggestion.id}`}
                  className={styles.suggestion}
                  onClick={() => setSuggestionsOpen(false)}
                >
                  {imageUrl ? (
                    <img
                      src={imageUrl}
                      alt=""
                      className={styles.suggestionImage}
                    />
                  ) : (
                    <div className={styles.suggestionPlaceholder}>
                      <ImageIcon size={16} />
                    </div>
                  )}
                  <div className={styles.suggestionInfo}>
                    <span className={styles.suggestionTitle}>
                      {suggestion.title}
                    </span>
                  </div>
                </Link>
              );
            })
          ) : (
            <div className={styles.suggestionMessage}>Нічого не знайдено</div>
          )}
        </div>
      )}
    </div>
  );
}