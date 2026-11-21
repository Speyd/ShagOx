import type {
  Advertisement,
  AttributeValue,
  VariantAttribute,
} from "@/shared/lib/types/advertisements";

export type CompareRow = {
  key: string;
  label: string;
  values: string[];
  isDifferent: boolean;
};

export type CompareSection = { title: string; rows: CompareRow[] };

const EMPTY = "—";

export const formatPrice = (value: number, symbol = "₴") =>
  `${value.toLocaleString("uk-UA")} ${symbol}`;

export const getCategoryLabel = (category: Advertisement["category"]) =>
  category?.label ?? "";

function formatValue(type: string, value: AttributeValue): string {
  if (type === "Boolean") return value.value === "True" ? "Так" : "Ні";
  return value.label || value.value || value.code;
}

function attributeText(attr: VariantAttribute): string {
  return attr.values.map((v) => formatValue(attr.attribute.type, v)).filter(Boolean).join(", ");
}

function productAttributes(item: Advertisement) {
  const map = new Map<string, { label: string; text: string }>();
  for (const attr of Array.isArray(item.attributes) ? item.attributes : []) {
    map.set(attr.attribute.key, { label: attr.attribute.label || attr.attribute.key, text: attributeText(attr) });
  }
  return map;
}

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
  if (prices.length === 0) {
    const price = item.price ?? 0;
    return { min: price, max: price };
  }
  return { min: Math.min(...prices), max: Math.max(...prices) };
}

export function getPreviousPrice(item: Advertisement, price: number) {
  const variants = item.variants ?? [];
  if (variants.length === 0) return item.previousPrice ?? 0;
  return variants.find((v) => v.price === price)?.previousPrice ?? 0;
}

export function isAdvertisementInStock(item: Advertisement) {
  const variants = item.variants ?? [];
  return variants.length === 0 || variants.some((v) => v.stock > 0);
}

function row(key: string, label: string, values: string[]): CompareRow {
  return { key, label, values, isDifferent: new Set(values).size > 1 };
}

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
      isAdvertisementInStock(item) ? "В наявності" : "Немає в наявності",
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
