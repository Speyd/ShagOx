import { X } from "lucide-react";
import type { CatalogFilters } from "@/features/catalog";
import { formatRangeValue } from "./format";
import styles from "../CatalogFilters.module.css";

type Chip = { id: string; label: string; remove: () => void };

const formatPrice = (value: number) => value.toLocaleString("uk-UA");

function buildChips(filters: CatalogFilters): Chip[] {
  const { groups, state } = filters;
  const chips: Chip[] = [];

  if (state.price) {
    const [from, to] = state.price;
    chips.push({ id: "price", label: `${formatPrice(from)} – ${formatPrice(to)} ₴`, remove: () => filters.setPrice(null) });
  }

  for (const group of groups) {
    if (group.kind === "options") {
      for (const value of state.options[group.key] ?? []) {
        const option = group.options.find((o) => o.value === value);
        chips.push({
          id: `${group.key}:${value}`,
          label: option?.label ?? value,
          remove: () => filters.setOptions(group.key, state.options[group.key].filter((v) => v !== value)),
        });
      }
    } else if (state.ranges[group.key]) {
      const [from, to] = state.ranges[group.key];
      chips.push({
        id: group.key,
        label: `${group.label}: ${formatRangeValue(from, group.unit)} – ${formatRangeValue(to, group.unit)}`,
        remove: () => filters.setRange(group.key, null),
      });
    }
  }

  return chips;
}

export function ActiveFilters({ filters }: { filters: CatalogFilters }) {
  const chips = buildChips(filters);
  if (chips.length === 0) return null;

  return (
    <div className={styles.chips}>
      {chips.map((chip) => (
        <button key={chip.id} type="button" className={styles.chip} onClick={chip.remove}>
          {chip.label}
          <X size={12} />
        </button>
      ))}
    </div>
  );
}
