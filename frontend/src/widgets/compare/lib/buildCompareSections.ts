import type {
  Advertisement,
  AttributeValue,
  VariantAttribute,
} from "@/shared/lib/types/advertisements";

export type CompareRow = {
  key: string;
  label: string;
  /** Значення в порядку товарів */
  values: string[];
  isDifferent: boolean;
};

export type CompareSection = { title: string; rows: CompareRow[] };

const EMPTY = "—";

export const formatPrice = (value: number, symbol = "₴") =>
  `${value.toLocaleString("uk-UA")} ${symbol}`;

/** API повертає `label`, хоча тип описує `lable`. */
export const getCategoryLabel = (category: Advertisement["category"]) =>
  (category as { label?: string }).label ?? category.lable ?? "";

function formatValue(type: string, value: AttributeValue): string {
  if (type === "Boolean") return value.value === "True" ? "Так" : "Ні";
  return value.label || value.value || value.code;
}

function attributeText(attr: VariantAttribute): string {
  return attr.values.map((v) => formatValue(attr.attribute.type, v)).filter(Boolean).join(", ");
}

/** Ключ → { назва, текст } для атрибутів товару. */
function productAttributes(item: Advertisement) {
  const map = new Map<string, { label: string; text: string }>();
  for (const attr of Array.isArray(item.attributes) ? item.attributes : []) {
    map.set(attr.attribute.key, { label: attr.attribute.label || attr.attribute.key, text: attributeText(attr) });
  }
  return map;
}

/** Ключ → { назва, усі значення по варіантах } для варіантних атрибутів. */
function variantAttributes(item: Advertisement) {
  const map = new Map<string, { label: string; values: Set<string> }>();
  for (const variant of item.variants ?? []) {
    for (const attr of variant.attributes) {
      const entry = map.get(attr.attribute.key) ?? { label: attr.attribute.label || attr.attribute.key, values: new Set() };
      const text = attributeText(attr);
      if (text) entry.values.add(text);
      map.set(attr.attribute.key, entry);
    }
  }
  return map;
}

export function getPriceRange(item: Advertisement) {
  const prices = (item.variants ?? []).map((v) => v.price);
  return { min: Math.min(...prices), max: Math.max(...prices) };
}

function row(key: string, label: string, values: string[]): CompareRow {
  return { key, label, values, isDifferent: new Set(values).size > 1 };
}

/** Рядки за порядком першої появи ключа серед товарів. */
function rowsFromMaps(
  maps: Map<string, { label: string }>[],
  text: (index: number, key: string) => string | undefined,
  skip: string[] = [],
) {
  const keys = new Map<string, string>();
  maps.forEach((m) => m.forEach((v, k) => !skip.includes(k) && !keys.has(k) && keys.set(k, v.label)));
  return [...keys].map(([key, label]) => row(key, label, maps.map((_, i) => text(i, key) || EMPTY)));
}

export function buildCompareSections(items: Advertisement[]): CompareSection[] {
  const product = items.map(productAttributes);
  const variants = items.map(variantAttributes);

  const main = [
    row("price", "Ціна", items.map((item) => {
      const { min, max } = getPriceRange(item);
      const symbol = item.currency?.symbol;
      return min === max ? formatPrice(min, symbol) : `від ${formatPrice(min, symbol)}`;
    })),
    row("stock", "Наявність", items.map((item) =>
      (item.variants ?? []).some((v) => v.stock > 0) ? "В наявності" : "Немає в наявності",
    )),
    row("brand", "Бренд", product.map((m) => m.get("brand")?.text || EMPTY)),
    row("seller", "Продавець", items.map((item) =>
      [item.seller?.firstName, item.seller?.lastName].filter(Boolean).join(" ") || item.seller?.userName || EMPTY,
    )),
  ];

  const characteristics = rowsFromMaps(product, (i, key) => product[i].get(key)?.text, ["brand"]);
  const options = rowsFromMaps(variants, (i, key) => [...(variants[i].get(key)?.values ?? [])].join(", "));

  return [
    { title: "Основне", rows: main },
    { title: "Характеристики", rows: characteristics },
    { title: "Доступні варіанти", rows: options },
  ].filter((section) => section.rows.length > 0);
}
