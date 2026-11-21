import type {
  AttributeFacet,
  AttributeInfo,
  CategoryAttributeValueResponse,
} from "@/entities/advertisement/model/types";

export type FilterOption = { value: string; label: string; count: number };

export type OptionsFilterGroup = {
  kind: "options";
  key: string;
  label: string;
  options: FilterOption[];
};

export type RangeFilterGroup = {
  kind: "range";
  key: string;
  label: string;
  unit: string;
  values: number[];
};

export type FilterGroup = OptionsFilterGroup | RangeFilterGroup;

export type FilterState = {
  options: Record<string, string[]>;
  ranges: Record<string, [number, number]>;
  price: [number, number] | null;
};

export const EMPTY_FILTERS: FilterState = { options: {}, ranges: {}, price: null };

const MIN_VALUES_FOR_SLIDER = 5;

const isNumeric = (attribute: AttributeInfo) =>
  attribute.type === "Integer" || attribute.type === "Decimal";

function splitUnit(label: string): [string, string] {
  const match = label.match(/^(.*?)\s*\(([^)]+)\)$/);
  return match ? [match[1], match[2]] : [label, ""];
}

function optionLabel(item: CategoryAttributeValueResponse, raw: string, unit: string) {
  const { attribute, values } = item;
  if (attribute.type === "Boolean") return raw === "true" ? "Так" : "Ні";
  if (attribute.type === "Select") return values?.find((v) => String(v.id) === raw)?.label ?? raw;
  if (isNumeric(attribute)) return unit ? `${raw} ${unit}` : raw;
  return String(JSON.parse(raw));
}

function sortOptions(attribute: AttributeInfo, options: FilterOption[]) {
  if (isNumeric(attribute)) return options.sort((a, b) => Number(a.value) - Number(b.value));
  if (attribute.type === "Boolean") return options.sort((a) => (a.value === "true" ? -1 : 1));
  return options.sort((a, b) => b.count - a.count || a.label.localeCompare(b.label, "uk"));
}

export function buildFilterGroups(
  items: CategoryAttributeValueResponse[],
  facets: AttributeFacet[],
): FilterGroup[] {
  const facetsByKey = new Map<string, AttributeFacet[]>();
  for (const facet of facets) {
    facetsByKey.set(facet.key, [...(facetsByKey.get(facet.key) ?? []), facet]);
  }

  const groups = items.flatMap<FilterGroup>((item) => {
    const { attribute } = item;
    const facet = facetsByKey.get(attribute.key) ?? [];
    if (facet.length < 2) return [];

    const [label, unit] = splitUnit(attribute.label || attribute.key);

    if (isNumeric(attribute) && facet.length >= MIN_VALUES_FOR_SLIDER) {
      const values = facet.map((f) => Number(f.value)).sort((a, b) => a - b);
      return [{ kind: "range", key: attribute.key, label, unit, values }];
    }

    const options = facet.map((f) => ({
      value: f.value,
      label: optionLabel(item, f.value, unit),
      count: f.count,
    }));
    return [{ kind: "options", key: attribute.key, label, options: sortOptions(attribute, options) }];
  });

  return groups.sort((a, b) => Number(b.key === "brand") - Number(a.key === "brand"));
}

export function toSearchParams(state: FilterState, groups: FilterGroup[]) {
  const attributes: Record<string, unknown[]> = {};

  for (const group of groups) {
    if (group.kind === "options" && state.options[group.key]?.length) {
      attributes[group.key] = state.options[group.key].map((v) => JSON.parse(v));
    }
    if (group.kind === "range" && state.ranges[group.key]) {
      const [min, max] = state.ranges[group.key];
      attributes[group.key] = group.values.filter((v) => v >= min && v <= max);
    }
  }

  return {
    attributes,
    minPrice: state.price?.[0],
    maxPrice: state.price?.[1],
  };
}

export function countActiveFilters(state: FilterState) {
  const options = Object.values(state.options).reduce((sum, v) => sum + v.length, 0);
  return options + Object.keys(state.ranges).length + (state.price ? 1 : 0);
}
