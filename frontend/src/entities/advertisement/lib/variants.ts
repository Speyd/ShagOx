import { getColorHex } from "@/shared/lib/colors";
import type {
  AdvertisementVariant,
  VariantAttribute,
} from "@/shared/lib/types/advertisements";

export type VariantOption = {
  code: string;
  label: string;
  hex?: string;
};

export type VariantOptionGroup = {
  key: string;
  label: string;
  isColor: boolean;
  options: VariantOption[];
};

export type VariantSelection = Record<string, string>;

const COLOR_KEY = "color";
const EXCLUDED_KEYS = ["brand"];

function splitUnit(label: string): [string, string] {
  const match = label.match(/^(.*?)\s*\(([^)]+)\)$/);
  return match ? [match[1], match[2]] : [label, ""];
}

function optionCode(attr: VariantAttribute): string {
  const value = attr.values[0];
  if (!value) return "";
  return value.code || value.value || value.label;
}

function optionLabel(attr: VariantAttribute): string {
  const value = attr.values[0];
  if (!value) return "";
  if (value.label) return value.label;

  const [, unit] = splitUnit(attr.attribute.label ?? "");
  const isNumber = attr.attribute.type === "Integer" || attr.attribute.type === "Decimal";
  return isNumber && unit ? `${value.value} ${unit}` : value.value || value.code;
}

export function getVariantSelection(variant?: AdvertisementVariant): VariantSelection {
  const selection: VariantSelection = {};
  for (const attr of variant?.attributes ?? []) {
    if (!EXCLUDED_KEYS.includes(attr.attribute.key)) {
      selection[attr.attribute.key] = optionCode(attr);
    }
  }
  return selection;
}

export function getVariantOptionGroups(variants: AdvertisementVariant[]): VariantOptionGroup[] {
  const groups = new Map<string, VariantOptionGroup>();

  for (const variant of variants) {
    for (const attr of variant.attributes) {
      const { key } = attr.attribute;
      if (EXCLUDED_KEYS.includes(key)) continue;

      const code = optionCode(attr);
      if (!code) continue;

      const group = groups.get(key) ?? {
        key,
        label: splitUnit(attr.attribute.label || key)[0],
        isColor: key === COLOR_KEY,
        options: [],
      };

      if (!group.options.some((o) => o.code === code)) {
        group.options.push({
          code,
          label: optionLabel(attr),
          hex: group.isColor ? getColorHex(code) : undefined,
        });
      }
      groups.set(key, group);
    }
  }

  return [...groups.values()].filter((g) => g.options.length > 1 || g.isColor);
}

function matchesCount(variant: AdvertisementVariant, selection: VariantSelection) {
  const own = getVariantSelection(variant);
  return Object.entries(selection).filter(([k, v]) => own[k] === v).length;
}

export function isOptionCombinable(
  variants: AdvertisementVariant[],
  selection: VariantSelection,
  key: string,
  code: string,
): boolean {
  const target = { ...selection, [key]: code };
  return variants.some((v) => matchesCount(v, target) === Object.keys(target).length);
}

export function findVariantForOption(
  variants: AdvertisementVariant[],
  selection: VariantSelection,
  key: string,
  code: string,
): AdvertisementVariant | undefined {
  const target = { ...selection, [key]: code };
  const candidates = variants.filter((v) => getVariantSelection(v)[key] === code);

  return candidates.reduce<AdvertisementVariant | undefined>((best, variant) => {
    if (!best) return variant;
    const score = matchesCount(variant, target) + (variant.stock > 0 ? 0.5 : 0);
    const bestScore = matchesCount(best, target) + (best.stock > 0 ? 0.5 : 0);
    return score > bestScore ? variant : best;
  }, undefined);
}

export function getDefaultVariant(variants: AdvertisementVariant[]) {
  return variants.find((v) => v.stock > 0) ?? variants[0];
}
