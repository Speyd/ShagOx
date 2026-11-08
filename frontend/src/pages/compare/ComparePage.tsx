import { useMemo, useState } from "react";
import { Link } from "react-router-dom";
import { Button, Loader, SegmentedControl } from "@mantine/core";
import { Scale } from "lucide-react";
import clsx from "clsx";
import Container from "@/shared/ui/layout/container";
import {
  MAX_COMPARE_PER_CATEGORY,
  useCompareAdvertisements,
  useCompareStore,
} from "@/entities/compare";
import { CompareBoard, getCategoryLabel } from "@/widgets/compare";
import styles from "./ComparePage.module.css";

type ViewMode = "all" | "diff";

export default function ComparePage() {
  const items = useCompareStore((state) => state.items);
  const remove = useCompareStore((state) => state.remove);
  const clearCategory = useCompareStore((state) => state.clearCategory);

  const { items: advertisements, isLoading } = useCompareAdvertisements(
    useMemo(() => items.map((i) => i.id), [items]),
  );

  const [selectedCategoryId, setSelectedCategoryId] = useState<number | null>(null);
  const [mode, setMode] = useState<ViewMode>("all");

  /** Вкладки категорій у порядку додавання товарів. */
  const categories = useMemo(() => {
    const map = new Map<number, { id: number; code: string; label: string; count: number }>();
    for (const ad of advertisements) {
      const entry = map.get(ad.category.id);
      if (entry) entry.count++;
      else map.set(ad.category.id, { id: ad.category.id, code: ad.category.code, label: getCategoryLabel(ad.category), count: 1 });
    }
    return [...map.values()];
  }, [advertisements]);

  // якщо обрану категорію очистили — показуємо першу доступну
  const active = categories.find((c) => c.id === selectedCategoryId) ?? categories[0];
  const activeItems = advertisements.filter((ad) => ad.category.id === active?.id);
  const canAdd = activeItems.length < MAX_COMPARE_PER_CATEGORY;

  if (isLoading && advertisements.length === 0) {
    return (
      <section className={styles.page}>
        <div className={styles.loader}>
          <Loader />
        </div>
      </section>
    );
  }

  return (
    <section className={styles.page}>
      <Container>
        <header className={styles.header}>
          <div>
            <h1 className={styles.title}>Порівняння товарів</h1>
            <p className={styles.subtitle}>Оберіть найкращу модель для себе</p>
            {active && (
              <Button
                className={styles.clearButton}
                bg="var(--color-base)"
                radius="md"
                onClick={() => clearCategory(active.id)}
              >
                Очистити список
              </Button>
            )}
          </div>

          {activeItems.length > 1 && (
            <SegmentedControl
              value={mode}
              onChange={(value) => setMode(value as ViewMode)}
              data={[
                { value: "all", label: "Усі характеристики" },
                { value: "diff", label: "Тільки відмінності" },
              ]}
            />
          )}
        </header>

        {categories.length > 1 && (
          <nav className={styles.tabs}>
            {categories.map((category) => (
              <button
                key={category.id}
                type="button"
                className={clsx(styles.tab, category.id === active?.id && styles.tabActive)}
                onClick={() => setSelectedCategoryId(category.id)}
              >
                {category.label}
                <span className={styles.tabCount}>{category.count}</span>
              </button>
            ))}
          </nav>
        )}

        {active ? (
          <CompareBoard
            items={activeItems}
            onlyDifferences={mode === "diff"}
            addHref={canAdd ? `/catalog/category/${active.code}` : null}
            onRemove={remove}
          />
        ) : (
          <div className={styles.empty}>
            <Scale size={48} strokeWidth={1.5} />
            <h2>Список порівняння порожній</h2>
            <p>
              Натисніть «Порівняти» на сторінці товару, щоб додати його сюди.
              <br />
              Порівнювати можна до {MAX_COMPARE_PER_CATEGORY} товарів однієї категорії.
            </p>
            <Button component={Link} to="/catalog" bg="var(--color-base)" radius="md">
              Перейти до каталогу
            </Button>
          </div>
        )}
      </Container>
    </section>
  );
}
