import {
  formatAttributeValue,
  type Advertisement,
} from "@/shared/lib/types/advertisements";

export type FilterMode = "all" | "diff" | "main";

export interface CompareRow {
  key: string;
  values: Record<number, string>;
  isDifferent: boolean;
}

export function prepareCompareTable(
  advertisements: Advertisement[],
  filterMode: FilterMode = "all",
): CompareRow[] {
  if (!advertisements.length) return [];

  const allKeysSet = new Set<string>();
  advertisements.forEach((ad) => {
    if (ad.attributes) {
      Object.keys(ad.attributes).forEach((key) => allKeysSet.add(key));
    }
  });

  const rows: CompareRow[] = [];

  allKeysSet.forEach((key) => {
    const values: Record<number, string> = {};
    const uniqueValues = new Set<string>();

    advertisements.forEach((ad) => {
      const rawValue = ad.attributes?.[key];

      const formatted = rawValue != null ? formatAttributeValue(rawValue) : "—";

      values[ad.id] = formatted;
      uniqueValues.add(formatted);
    });

    const isDifferent = uniqueValues.size > 1;

    rows.push({
      key,
      values,
      isDifferent,
    });
  });

  return rows.filter((row) => {
    if (filterMode === "diff") {
      return row.isDifferent;
    }
    if (filterMode === "main") {
      const mainKeys = ["price", "brand", "condition", "color"];
      return mainKeys.includes(row.key.toLowerCase());
    }
    return true;
  });
}
