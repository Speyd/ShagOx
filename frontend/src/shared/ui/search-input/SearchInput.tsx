import {
  useState,
  useEffect,
  useRef,
  useMemo,
  type FormEvent,
  type MouseEvent,
} from "react";
import { TextInput, Loader, Text } from "@mantine/core";
import { useDebouncedValue } from "@mantine/hooks";
import styles from "./SearchInput.module.css";
import {
  Search,
  X,
  History,
  TrendingUp,
  Tag,
  ArrowRight,
  Package,
} from "lucide-react";
import { useNavigate, useSearchParams, Link } from "react-router-dom";
import { useQuery } from "@tanstack/react-query";
import { getCatalogAdvertisements } from "@/entities/advertisement/api/api";
import { useGetCategories } from "@/entities/advertisement/model/hooks/useGetCategories";
import { getAdvertisementPrice } from "@/shared/lib/types/advertisements";

const SEARCH_HISTORY_KEY = "marketly_search_history";
const MAX_HISTORY_ITEMS = 6;

function loadSearchHistory(): string[] {
  try {
    const raw = localStorage.getItem(SEARCH_HISTORY_KEY);
    if (!raw) return [];
    const parsed = JSON.parse(raw);
    return Array.isArray(parsed) ? parsed.slice(0, MAX_HISTORY_ITEMS) : [];
  } catch {
    return [];
  }
}

function saveSearchHistory(history: string[]) {
  try {
    localStorage.setItem(
      SEARCH_HISTORY_KEY,
      JSON.stringify(history.slice(0, MAX_HISTORY_ITEMS)),
    );
  } catch {
    // Ignore storage quota errors
  }
}

export default function SearchInput() {
  const navigate = useNavigate();
  const [searchParams] = useSearchParams();
  const queryFromUrl = searchParams.get("search") ?? "";

  const [value, setValue] = useState(queryFromUrl);
  const [isOpen, setIsOpen] = useState(false);
  const [history, setHistory] = useState<string[]>(loadSearchHistory);

  const containerRef = useRef<HTMLDivElement>(null);
  const inputRef = useRef<HTMLInputElement>(null);

  const [debouncedSearch] = useDebouncedValue(value.trim(), 350);

  // Sync value when URL search param changes
  useEffect(() => {
    setValue(queryFromUrl);
  }, [queryFromUrl]);

  // Categories list for popular chips & category matching
  const { data: categories = [] } = useGetCategories();

  // Autocomplete preview search query
  const { data: suggestionsData, isFetching } = useQuery({
    queryKey: ["search-suggestions", debouncedSearch],
    queryFn: () =>
      getCatalogAdvertisements({
        search: debouncedSearch,
        page: 1,
        pageSize: 5,
      }),
    enabled: isOpen && debouncedSearch.length >= 2,
    staleTime: 30_000,
  });

  const suggestions = suggestionsData?.items ?? [];
  const totalFound = suggestionsData?.totalCount ?? suggestions.length;

  // Matching categories based on current input
  const matchingCategories = useMemo(() => {
    if (debouncedSearch.length < 2 || !categories.length) return [];
    const query = debouncedSearch.toLowerCase();
    return categories
      .filter((cat) => cat.label.toLowerCase().includes(query))
      .slice(0, 2);
  }, [categories, debouncedSearch]);

  // Popular categories for empty-state dropdown
  const popularCategories = useMemo(() => {
    return categories.slice(0, 6);
  }, [categories]);

  // Handle outside click & Escape key
  useEffect(() => {
    const handleOutsideClick = (event: globalThis.MouseEvent) => {
      if (
        containerRef.current &&
        !containerRef.current.contains(event.target as Node)
      ) {
        setIsOpen(false);
      }
    };

    const handleKeyDown = (event: KeyboardEvent) => {
      if (event.key === "Escape") {
        setIsOpen(false);
        inputRef.current?.blur();
      }
    };

    document.addEventListener("mousedown", handleOutsideClick);
    document.addEventListener("keydown", handleKeyDown);
    return () => {
      document.removeEventListener("mousedown", handleOutsideClick);
      document.removeEventListener("keydown", handleKeyDown);
    };
  }, []);

  const saveTermToHistory = (term: string) => {
    const clean = term.trim();
    if (clean.length < 2) return;
    setHistory((prev) => {
      const next = [
        clean,
        ...prev.filter((i) => i.toLowerCase() !== clean.toLowerCase()),
      ].slice(0, MAX_HISTORY_ITEMS);
      saveSearchHistory(next);
      return next;
    });
  };

  const removeHistoryItem = (e: MouseEvent, itemToRemove: string) => {
    e.stopPropagation();
    e.preventDefault();
    setHistory((prev) => {
      const next = prev.filter((i) => i !== itemToRemove);
      saveSearchHistory(next);
      return next;
    });
  };

  const clearAllHistory = (e: MouseEvent) => {
    e.stopPropagation();
    e.preventDefault();
    setHistory([]);
    saveSearchHistory([]);
  };

  const performSearch = (searchTerm: string) => {
    const trimmed = searchTerm.trim();
    setIsOpen(false);

    if (trimmed) {
      saveTermToHistory(trimmed);
      navigate(`/catalog?search=${encodeURIComponent(trimmed)}`);
    } else {
      navigate("/catalog");
    }
  };

  const handleSubmit = (e?: FormEvent) => {
    if (e) e.preventDefault();
    performSearch(value);
  };

  const handleClear = () => {
    setValue("");
    inputRef.current?.focus();
    setIsOpen(true);
  };

  const isQueryMode = debouncedSearch.length >= 2;

  return (
    <div ref={containerRef} className={styles.searchContainer}>
      <form onSubmit={handleSubmit} className={styles.searchForm}>
        <TextInput
          ref={inputRef}
          placeholder="Пошук товарів, брендів, категорій..."
          value={value}
          onFocus={() => setIsOpen(true)}
          onChange={(e) => {
            setValue(e.currentTarget.value);
            setIsOpen(true);
          }}
          rightSectionWidth={value ? 76 : 44}
          rightSection={
            <div className={styles.rightSection}>
              {value && (
                <button
                  type="button"
                  onClick={handleClear}
                  className={styles.clearButton}
                  aria-label="Очистити поле пошуку"
                  title="Очистити"
                >
                  <X size={16} />
                </button>
              )}
              <button
                type="submit"
                className={styles.searchButton}
                aria-label="Шукати"
                title="Шукати"
              >
                <Search size={18} />
              </button>
            </div>
          }
          size="lg"
          className={styles.searchInput}
          aria-expanded={isOpen}
          aria-autocomplete="list"
          role="combobox"
        />
      </form>

      {isOpen && (
        <div className={styles.dropdown} role="listbox">
          {/* STATE 1: Empty input or < 2 symbols -> Show Search History & Popular Categories */}
          {!isQueryMode && (
            <div className={styles.initialContent}>
              {history.length > 0 && (
                <div className={styles.section}>
                  <div className={styles.sectionHeader}>
                    <div className={styles.sectionTitle}>
                      <History size={15} />
                      <span>Історія пошуку</span>
                    </div>
                    <button
                      type="button"
                      onClick={clearAllHistory}
                      className={styles.clearAllButton}
                    >
                      Очистити все
                    </button>
                  </div>
                  <div className={styles.historyList}>
                    {history.map((term) => (
                      <div
                        key={term}
                        className={styles.historyItem}
                        onClick={() => {
                          setValue(term);
                          performSearch(term);
                        }}
                      >
                        <div className={styles.historyItemLeft}>
                          <History size={14} className={styles.historyIcon} />
                          <span className={styles.historyText}>{term}</span>
                        </div>
                        <button
                          type="button"
                          className={styles.removeHistoryButton}
                          onClick={(e) => removeHistoryItem(e, term)}
                          aria-label={`Видалити ${term} з історії`}
                          title="Видалити з історії"
                        >
                          <X size={14} />
                        </button>
                      </div>
                    ))}
                  </div>
                </div>
              )}

              {popularCategories.length > 0 && (
                <div className={styles.section}>
                  <div className={styles.sectionHeader}>
                    <div className={styles.sectionTitle}>
                      <TrendingUp size={15} />
                      <span>Популярні категорії</span>
                    </div>
                  </div>
                  <div className={styles.categoryChips}>
                    {popularCategories.map((cat) => (
                      <Link
                        key={cat.id}
                        to={`/catalog/category/${cat.code}`}
                        className={styles.categoryChip}
                        onClick={() => setIsOpen(false)}
                      >
                        {cat.label}
                      </Link>
                    ))}
                  </div>
                </div>
              )}
            </div>
          )}

          {/* STATE 2: Query mode (>= 2 symbols) -> Loading, Category matches, Products preview */}
          {isQueryMode && (
            <div className={styles.resultsContent}>
              {isFetching ? (
                <div className={styles.loadingWrapper}>
                  <Loader size="sm" />
                  <Text fz={14} c="dimmed" mt={8}>
                    Шукаємо «{debouncedSearch}»...
                  </Text>
                </div>
              ) : (
                <>
                  {/* Category Suggestion */}
                  {matchingCategories.length > 0 && (
                    <div className={styles.categorySuggestions}>
                      {matchingCategories.map((cat) => (
                        <Link
                          key={cat.id}
                          to={`/catalog/category/${cat.code}?search=${encodeURIComponent(
                            debouncedSearch,
                          )}`}
                          className={styles.categorySuggestionItem}
                          onClick={() => {
                            saveTermToHistory(debouncedSearch);
                            setIsOpen(false);
                          }}
                        >
                          <Tag size={16} className={styles.categoryIcon} />
                          <span>
                            Шукати «<strong>{debouncedSearch}</strong>» у категорії{" "}
                            <span className={styles.categoryHighlight}>
                              {cat.label}
                            </span>
                          </span>
                        </Link>
                      ))}
                    </div>
                  )}

                  {/* Quick Results (Products Preview) */}
                  {suggestions.length > 0 ? (
                    <div className={styles.previewSection}>
                      <div className={styles.previewHeader}>
                        <Text fz={12} fw={600} c="dimmed">
                          Товари
                        </Text>
                      </div>

                      <div className={styles.productsList}>
                        {suggestions.map((item) => {
                          const imageUrl = item.pictures?.[0]?.url;
                          const price = getAdvertisementPrice(item);
                          const inStock = (item.stock ?? 0) > 0;

                          return (
                            <Link
                              key={item.id}
                              to={`/advertisement/${item.id}`}
                              className={styles.productItem}
                              onClick={() => {
                                saveTermToHistory(debouncedSearch);
                                setIsOpen(false);
                              }}
                            >
                              {imageUrl ? (
                                <img
                                  src={imageUrl}
                                  alt={item.title}
                                  className={styles.productImage}
                                  onError={(e) => {
                                    e.currentTarget.style.display = "none";
                                  }}
                                />
                              ) : (
                                <div className={styles.productImagePlaceholder}>
                                  <Package size={20} />
                                </div>
                              )}

                              <div className={styles.productDetails}>
                                <span className={styles.productTitle}>
                                  {item.title}
                                </span>
                                <div className={styles.productMeta}>
                                  <span className={styles.productPrice}>
                                    {price.toLocaleString("uk-UA")} ₴
                                  </span>
                                  {inStock ? (
                                    <span className={styles.inStockBadge}>
                                      В наявності
                                    </span>
                                  ) : (
                                    <span className={styles.outOfStockBadge}>
                                      Закінчився
                                    </span>
                                  )}
                                </div>
                              </div>
                            </Link>
                          );
                        })}
                      </div>

                      {/* View All Results Button */}
                      <button
                        type="button"
                        onClick={handleSubmit}
                        className={styles.viewAllButton}
                      >
                        <span>
                          Усі результати за запитом «<strong>{debouncedSearch}</strong>»{" "}
                          {totalFound > 0 && `(${totalFound})`}
                        </span>
                        <ArrowRight size={16} />
                      </button>
                    </div>
                  ) : (
                    matchingCategories.length === 0 && (
                      <div className={styles.emptyResults}>
                        <Text fz={15} fw={600} c="var(--color-base)">
                          За запитом «{debouncedSearch}» нічого не знайдено
                        </Text>
                        <Text fz={13} c="dimmed" mt={4}>
                          Перевірте правильність написання або спробуйте більш
                          загальний запит
                        </Text>
                      </div>
                    )
                  )}
                </>
              )}
            </div>
          )}
        </div>
      )}
    </div>
  );
}
