import type { VariantRow } from "../model/types";

export const MAX_VARIANTS = 50;

export function getVariantKey(options: Record<string, string>): string {
  return Object.entries(options)
    .map(([key, value]) => `${key}=${value}`)
    .sort()
    .join("|");
}

export function createVariantRow(
  options: Record<string, string> = {},
  template?: Pick<VariantRow, "price" | "previousPrice" | "stock">,
): VariantRow {
  return {
    key: getVariantKey(options),
    options,
    price: template?.price ?? "",
    previousPrice: template?.previousPrice ?? "",
    stock: template?.stock ?? 1,
  };
}

/**
 * Будує всі комбінації обраних значень (декартів добуток).
 * Уже заповнені рядки зберігаються, нові отримують ціну й кількість найближчого існуючого.
 * Якщо значення не обрані, повертає один рядок без атрибутів (товар без варіантів).
 */
export function buildVariantRows(
  variantOptions: Record<string, string[] | undefined>,
  attributeKeys: string[],
  currentRows: VariantRow[],
): VariantRow[] {
  const combinations = attributeKeys.reduce<Record<string, string>[]>(
    (acc, key) => {
      const values = variantOptions[key] ?? [];
      if (values.length === 0) return acc;

      return acc.flatMap((combo) =>
        values.map((value) => ({ ...combo, [key]: value })),
      );
    },
    [{}],
  );

  const rowsByKey = new Map(currentRows.map((row) => [row.key, row]));

  // Новий рядок успадковує ціну "батьківського" варіанта (color=red → color=red|size=M)
  const findTemplate = (options: Record<string, string>) =>
    currentRows.find((row) =>
      Object.entries(row.options).every(([key, value]) => options[key] === value),
    ) ?? currentRows[0];

  return combinations.map(
    (options) =>
      rowsByKey.get(getVariantKey(options)) ??
      createVariantRow(options, findTemplate(options)),
  );
}
