import { useMemo, type CSSProperties } from "react";
import clsx from "clsx";
import type { Advertisement } from "@/shared/lib/types/advertisements";
import { buildCompareSections } from "../lib/buildCompareSections";
import { AddCompareCard } from "./AddCompareCard";
import { CompareCard } from "./CompareCard";
import styles from "./CompareBoard.module.css";

type CompareBoardProps = {
  items: Advertisement[];
  onlyDifferences: boolean;
  /** Посилання для картки «Додати товар»; null — ліміт досягнуто */
  addHref: string | null;
  onRemove: (id: number) => void;
};

/**
 * Картки товарів і таблиця характеристик під ними. Обидві частини
 * використовують одну сітку, тож значення стоять точно під своїм товаром.
 */
export function CompareBoard({ items, onlyDifferences, addHref, onRemove }: CompareBoardProps) {
  const sections = useMemo(() => {
    const all = buildCompareSections(items);
    if (!onlyDifferences) return all;
    return all
      .map((s) => ({ ...s, rows: s.rows.filter((r) => r.isDifferent) }))
      .filter((s) => s.rows.length > 0);
  }, [items, onlyDifferences]);

  const columns = items.length + (addHref ? 1 : 0);
  const gridStyle = { "--columns": columns } as CSSProperties;
  const highlight = items.length > 1;

  return (
    <div className={styles.scroll}>
      <div className={styles.board} style={gridStyle}>
        <div className={styles.grid}>
          {items.map((item) => (
            <CompareCard key={item.id} advertisement={item} onRemove={onRemove} />
          ))}
          {addHref && <AddCompareCard to={addHref} />}
        </div>

        {sections.length === 0 && (
          <p className={styles.noDiff}>Товари мають однакові характеристики.</p>
        )}

        {sections.map((section) => (
          <section key={section.title} className={styles.section}>
            <h3 className={styles.sectionTitle}>{section.title}</h3>

            {section.rows.map((row) => (
              <div
                key={row.key}
                className={clsx(styles.row, highlight && row.isDifferent && styles.rowDifferent)}
              >
                <div className={styles.rowLabel}>{row.label}</div>
                <div className={styles.grid}>
                  {row.values.map((value, i) => (
                    <div key={items[i].id} className={styles.cell}>
                      {value}
                    </div>
                  ))}
                </div>
              </div>
            ))}
          </section>
        ))}
      </div>
    </div>
  );
}
