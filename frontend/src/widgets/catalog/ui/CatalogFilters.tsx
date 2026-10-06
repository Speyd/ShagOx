import { useState, useCallback } from "react";
import styles from "./CatalogFilters.module.css";
import { SlidersHorizontal, ChevronDown, Check, RotateCcw } from "lucide-react";
import { useGetCategories } from "@/entities/advertisement/model/hooks/useGetCategories";

type CatalogFiltersProps = {
  selectedCategories: number[];
  onCategoriesChange: (categories: number[]) => void;
  inStock: boolean | undefined;
  onInStockChange: (inStock: boolean | undefined) => void;
  selectedMemory?: string[];
  onMemoryChange?: (memory: string[]) => void;
  selectedColors?: string[];
  onColorsChange?: (colors: string[]) => void;
  minPrice: number | undefined;
  maxPrice: number | undefined;
  onMinPriceChange: (val: number | undefined) => void;
  onMaxPriceChange: (val: number | undefined) => void;
  onClearFilters: () => void;
  mobile?: boolean;
};

const COLOR_OPTIONS = [
  { name: "Сріблястий", value: "White", bg: "#f2f2f7", border: true },
  { name: "Космічний чорний", value: "Black", bg: "#1f2022" },
  { name: "Синій титан", value: "Blue", bg: "#1e3a5f" },
  { name: "Натуральний титан", value: "Natural", bg: "#9a9895" },
  { name: "Золотий / Пустельний", value: "Gold", bg: "#e2d0ba" },
  { name: "Темно-фіолетовий", value: "Purple", bg: "#4a3b5a" },
  { name: "Червоний", value: "Red", bg: "#dc2626" },
];

const MEMORY_OPTIONS = ["128 ГБ", "256 ГБ", "512 ГБ", "1 ТБ"];

function FilterSection({
  title,
  defaultOpen = true,
  children,
}: {
  title: string;
  defaultOpen?: boolean;
  children: React.ReactNode;
}) {
  const [open, setOpen] = useState(defaultOpen);

  return (
    <div className={styles.filterSection}>
      <div className={styles.filterHeader} onClick={() => setOpen(!open)}>
        <span className={styles.filterTitle}>{title}</span>
        <ChevronDown
          size={16}
          className={`${styles.chevron} ${open ? styles.chevronOpen : ""}`}
        />
      </div>
      {open && <div className={styles.filterBody}>{children}</div>}
    </div>
  );
}

function CheckboxItem({
  checked,
  label,
  onChange,
}: {
  checked: boolean;
  label: string;
  onChange: () => void;
}) {
  return (
    <label className={styles.checkboxItem} onClick={onChange}>
      <div
        className={`${styles.checkbox} ${checked ? styles.checkboxChecked : ""}`}
      >
        {checked && <Check size={12} strokeWidth={3} />}
      </div>
      <span className={styles.checkboxLabel}>{label}</span>
    </label>
  );
}

export default function CatalogFilters({
  selectedCategories,
  onCategoriesChange,
  inStock,
  onInStockChange,
  selectedMemory = [],
  onMemoryChange,
  selectedColors = [],
  onColorsChange,
  minPrice,
  maxPrice,
  onMinPriceChange,
  onMaxPriceChange,
  onClearFilters,
  mobile = false,
}: CatalogFiltersProps) {
  const { data: categories = [] } = useGetCategories();
  const [showAllCategories, setShowAllCategories] = useState(false);
  const [showAllColors, setShowAllColors] = useState(false);

  const visibleCategories = showAllCategories
    ? categories
    : categories.slice(0, 5);
  const visibleColors = showAllColors
    ? COLOR_OPTIONS
    : COLOR_OPTIONS.slice(0, 5);

  const toggleCategory = useCallback(
    (id: number) => {
      if (selectedCategories.includes(id)) {
        onCategoriesChange(selectedCategories.filter((c) => c !== id));
      } else {
        onCategoriesChange([...selectedCategories, id]);
      }
    },
    [selectedCategories, onCategoriesChange],
  );

  const toggleMemory = (mem: string) => {
    if (!onMemoryChange) return;
    if (selectedMemory.includes(mem)) {
      onMemoryChange(selectedMemory.filter((m) => m !== mem));
    } else {
      onMemoryChange([...selectedMemory, mem]);
    }
  };

  const toggleColor = (colorVal: string) => {
    if (!onColorsChange) return;
    if (selectedColors.includes(colorVal)) {
      onColorsChange(selectedColors.filter((c) => c !== colorVal));
    } else {
      onColorsChange([...selectedColors, colorVal]);
    }
  };

  return (
    <aside
      className={`${styles.sidebar} ${mobile ? styles.mobileSidebar : ""}`}
    >
      <div className={styles.filterCard}>
        <div className={styles.sidebarHeader}>
          <SlidersHorizontal size={18} />
          <span className={styles.sidebarHeaderText}>Фільтри</span>
        </div>

        <FilterSection title="Категорія">
          <CheckboxItem
            checked={selectedCategories.length === 0}
            label="Всі"
            onChange={() => onCategoriesChange([])}
          />
          {visibleCategories.map((cat) => (
            <CheckboxItem
              key={cat.id}
              checked={selectedCategories.includes(cat.id)}
              label={cat.name}
              onChange={() => toggleCategory(cat.id)}
            />
          ))}
          {categories.length > 5 && (
            <button
              type="button"
              className={styles.showMore}
              onClick={() => setShowAllCategories(!showAllCategories)}
            >
              {showAllCategories ? "Сховати" : "Показати ще"}
            </button>
          )}
        </FilterSection>

        <FilterSection title="Наявність">
          <CheckboxItem
            checked={inStock === true}
            label="В наявності"
            onChange={() =>
              onInStockChange(inStock === true ? undefined : true)
            }
          />
          <CheckboxItem
            checked={inStock === false}
            label="Немає в наявності"
            onChange={() =>
              onInStockChange(inStock === false ? undefined : false)
            }
          />
        </FilterSection>

        <FilterSection title="Пам'ять">
          {MEMORY_OPTIONS.map((mem) => (
            <CheckboxItem
              key={mem}
              checked={selectedMemory.includes(mem)}
              label={mem}
              onChange={() => toggleMemory(mem)}
            />
          ))}
        </FilterSection>

        <FilterSection title="Колір">
          <div className={styles.colorSwatches}>
            {visibleColors.map((color) => {
              const active = selectedColors.includes(color.value);
              return (
                <button
                  key={color.value}
                  type="button"
                  title={color.name}
                  className={`${styles.colorSwatch} ${
                    active ? styles.colorSwatchActive : ""
                  }`}
                  style={{
                    backgroundColor: color.bg,
                    borderColor: color.border ? "#cbd5e1" : "transparent",
                  }}
                  onClick={() => toggleColor(color.value)}
                  aria-label={color.name}
                />
              );
            })}
          </div>
          {COLOR_OPTIONS.length > 5 && (
            <button
              type="button"
              className={styles.showMore}
              onClick={() => setShowAllColors(!showAllColors)}
            >
              {showAllColors ? "Сховати" : "Показати ще"}
            </button>
          )}
        </FilterSection>

        <FilterSection title="Ціна">
          <div className={styles.priceInputs}>
            <div className={styles.priceInputWrapper}>
              <input
                type="number"
                className={styles.priceInput}
                placeholder="Від"
                value={minPrice ?? ""}
                onChange={(e) =>
                  onMinPriceChange(
                    e.target.value ? Number(e.target.value) : undefined,
                  )
                }
              />
              <span className={styles.currencySign}>₴</span>
            </div>
            <span className={styles.priceDash}>—</span>
            <div className={styles.priceInputWrapper}>
              <input
                type="number"
                className={styles.priceInput}
                placeholder="До"
                value={maxPrice ?? ""}
                onChange={(e) =>
                  onMaxPriceChange(
                    e.target.value ? Number(e.target.value) : undefined,
                  )
                }
              />
              <span className={styles.currencySign}>₴</span>
            </div>
          </div>
        </FilterSection>
      </div>

      <button
        type="button"
        className={styles.clearButton}
        onClick={onClearFilters}
      >
        <RotateCcw size={15} />
        Очистити фільтри
      </button>
    </aside>
  );
}
